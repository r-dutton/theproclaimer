using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzePipelineBehavior(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var behaviorInterface = classDeclaration.BaseList?.Types
            .Select(t => MatchGenericInterface(t.Type, "IPipelineBehavior"))
            .FirstOrDefault(g => g is not null);

        if (behaviorInterface is null)
        {
            return;
        }

        var requestType = behaviorInterface.TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;
        var responseType = behaviorInterface.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void";

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var info = new PipelineBehaviorInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, requestType, responseType);
        CaptureBehaviorDependencies(classDeclaration, tree, fieldTypes, info.ServiceUsages, info.OptionsUsages, info.CacheInvocations);
        _pipelineBehaviors[fqdn] = info;
    }

    private void AnalyzeRequestProcessor(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        if (classDeclaration.BaseList is not { Types.Count: > 0 })
        {
            return;
        }

        GenericNameSyntax? processorInterface = null;
        var kind = string.Empty;

        foreach (var baseType in classDeclaration.BaseList.Types)
        {
            var generic = MatchGenericInterface(baseType.Type, "IRequestPreProcessor", "IRequestPostProcessor");
            if (generic is null)
            {
                continue;
            }

            processorInterface = generic;
            kind = generic.Identifier.Text switch
            {
                "IRequestPreProcessor" => "pre",
                "IRequestPostProcessor" => "post",
                _ => string.Empty
            };
            break;
        }

        if (processorInterface is null || string.IsNullOrWhiteSpace(kind))
        {
            return;
        }

        var requestType = processorInterface.TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;
        var responseType = kind == "post"
            ? processorInterface.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void"
            : "void";

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var info = new RequestProcessorInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, requestType, responseType, kind);
        CaptureBehaviorDependencies(classDeclaration, tree, fieldTypes, info.ServiceUsages, info.OptionsUsages, info.CacheInvocations);
        _requestProcessors[fqdn] = info;
    }

    private void CaptureBehaviorDependencies(
        ClassDeclarationSyntax classDeclaration,
        SyntaxTree tree,
        IReadOnlyDictionary<string, FieldDescriptor> fieldTypes,
        List<ServiceUsage> serviceUsages,
        List<OptionsUsage> optionsUsages,
        List<CacheInvocation> cacheInvocations)
    {
        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var descriptor in fieldTypes.Values)
        {
            serviceUsages.Add(new ServiceUsage(descriptor.Type, descriptor.Line));
        }

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            foreach (var memberAccess in method.DescendantNodes().OfType<MemberAccessExpressionSyntax>())
            {
                if (memberAccess.Expression is not IdentifierNameSyntax identifier)
                {
                    continue;
                }

                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (!fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    continue;
                }

                var typeName = descriptor.Type;
                var resolvedType = ResolveImplementationType(typeName) ?? typeName;

                if (IsCacheService(resolvedType) || IsCacheService(typeName))
                {
                    var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                    if (TryCaptureCacheInvocation(memberAccess, memberAccess.Parent as InvocationExpressionSyntax, cacheType, tree) is { } cacheInvocation)
                    {
                        cacheInvocations.Add(cacheInvocation);
                    }

                    continue;
                }

                var invocation = memberAccess.Parent as InvocationExpressionSyntax;
                var invocationNode = (SyntaxNode?)invocation ?? memberAccess;
                var line = GetLineNumber(tree, invocationNode);
                var methodName = GetMemberName(memberAccess.Name);

                if (TryResolveOptionsType(resolvedType) is { } resolvedOptionsType)
                {
                    optionsUsages.Add(new OptionsUsage(resolvedOptionsType, line));
                }
                else if (TryResolveOptionsType(typeName) is { } descriptorOptionsType)
                {
                    optionsUsages.Add(new OptionsUsage(descriptorOptionsType, line));
                }

                var serviceType = resolvedType ?? typeName;
                serviceUsages.Add(new ServiceUsage(serviceType, line, methodName));
            }
        }
    }

    private void EmitPipelineBehaviors()
    {
        foreach (var behavior in _pipelineBehaviors.Values)
        {
            var id = StableId.For("cqrs.pipeline_behavior", behavior.Fqdn, behavior.Assembly, behavior.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.pipeline_behavior",
                Name = behavior.Name,
                Fqdn = behavior.Fqdn,
                Assembly = behavior.Assembly,
                Project = behavior.Project,
                FilePath = behavior.FilePath,
                Span = behavior.Span,
                SymbolId = behavior.SymbolId,
                Tags = new[] { "app" }
            };

            if (FindRequestByType(behavior.RequestType) is { } request)
            {
                var requestId = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
                var props = new Dictionary<string, object>
                {
                    ["response_type"] = behavior.ResponseType
                };

                _edges.Add(new GraphEdge
                {
                    From = requestId,
                    To = id,
                    Kind = "processed_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.pipeline",
                        MethodSpan = behavior.Span
                    },
                    Props = props,
                    Evidence = CreateEvidence(behavior.FilePath, behavior.Span)
                });
            }

            EmitBehaviorServiceEdges(behavior.ServiceUsages, behavior.FilePath, id);
            EmitBehaviorOptionEdges(behavior.OptionsUsages, behavior.FilePath, id);
            EmitBehaviorCacheEdges(behavior.CacheInvocations, behavior.FilePath, id);
        }
    }

    private void EmitRequestProcessors()
    {
        foreach (var processor in _requestProcessors.Values)
        {
            var id = StableId.For("cqrs.request_processor", processor.Fqdn, processor.Assembly, processor.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.request_processor",
                Name = processor.Name,
                Fqdn = processor.Fqdn,
                Assembly = processor.Assembly,
                Project = processor.Project,
                FilePath = processor.FilePath,
                Span = processor.Span,
                SymbolId = processor.SymbolId,
                Tags = new[] { "app" },
                Props = new Dictionary<string, object>
                {
                    ["stage"] = processor.Kind
                }
            };

            if (FindRequestByType(processor.RequestType) is { } request)
            {
                var requestId = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
                var props = new Dictionary<string, object>
                {
                    ["stage"] = processor.Kind
                };

                if (!string.IsNullOrWhiteSpace(processor.ResponseType) && !processor.ResponseType.Equals("void", StringComparison.OrdinalIgnoreCase))
                {
                    props["response_type"] = processor.ResponseType;
                }

                _edges.Add(new GraphEdge
                {
                    From = requestId,
                    To = id,
                    Kind = "processed_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.request_processor",
                        MethodSpan = processor.Span
                    },
                    Props = props,
                    Evidence = CreateEvidence(processor.FilePath, processor.Span)
                });
            }

            EmitBehaviorServiceEdges(processor.ServiceUsages, processor.FilePath, id);
            EmitBehaviorOptionEdges(processor.OptionsUsages, processor.FilePath, id);
            EmitBehaviorCacheEdges(processor.CacheInvocations, processor.FilePath, id);
        }
    }

    private void EmitBehaviorServiceEdges(IEnumerable<ServiceUsage> usages, string sourceFile, string fromId)
    {
        foreach (var usage in usages
            .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.OrderBy(u => u.Line).First()))
        {
            if (!TryEnsureServiceNode(usage.ServiceType, out var serviceId, out var registration))
            {
                continue;
            }

            var props = new Dictionary<string, object>
            {
                ["service_type"] = usage.ServiceType
            };

            if (registration is not null)
            {
                props["lifetime"] = registration.Lifetime;
            }

            if (!string.IsNullOrWhiteSpace(usage.Method))
            {
                props["method"] = usage.Method!;
            }

            _edges.Add(new GraphEdge
            {
                From = fromId,
                To = serviceId!,
                Kind = "uses_service",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "ioc.resolve",
                    Location = new GraphLocation { File = sourceFile, Line = usage.Line }
                },
                Props = props,
                Evidence = CreateEvidence(sourceFile, usage.Line)
            });
        }
    }

    private void EmitBehaviorOptionEdges(IEnumerable<OptionsUsage> usages, string sourceFile, string fromId)
    {
        foreach (var usage in usages)
        {
            var optionsId = EnsureOptionsNode(usage.OptionsType);
            if (optionsId is null)
            {
                continue;
            }

            var props = new Dictionary<string, object>
            {
                ["options_type"] = usage.OptionsType
            };

            _edges.Add(new GraphEdge
            {
                From = fromId,
                To = optionsId,
                Kind = "uses_options",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "options.access",
                    Location = new GraphLocation { File = sourceFile, Line = usage.Line }
                },
                Props = props,
                Evidence = CreateEvidence(sourceFile, usage.Line)
            });
        }
    }

    private void EmitBehaviorCacheEdges(IEnumerable<CacheInvocation> caches, string sourceFile, string fromId)
    {
        foreach (var cache in caches)
        {
            var cacheId = EnsureCacheNode(cache.CacheType);
            var props = new Dictionary<string, object>
            {
                ["method"] = cache.Method,
                ["operation"] = cache.Operation
            };

            if (!string.IsNullOrWhiteSpace(cache.Key))
            {
                props["key"] = cache.Key!;
            }

            _edges.Add(new GraphEdge
            {
                From = fromId,
                To = cacheId,
                Kind = "uses_cache",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "cache.operation",
                    Location = new GraphLocation { File = sourceFile, Line = cache.Line }
                },
                Props = props,
                Evidence = CreateEvidence(sourceFile, cache.Line)
            });
        }
    }
}
