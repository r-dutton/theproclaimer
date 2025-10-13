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
    private void AnalyzeBackgroundService(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var info = new BackgroundServiceInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);
        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var descriptor in fieldTypes.Values)
        {
            info.ServiceUsages.Add(new ServiceUsage(descriptor.Type, descriptor.Line));
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

                var typeName = ResolveImplementationType(descriptor.Type) ?? descriptor.Type;
                var invocation = memberAccess.Parent as InvocationExpressionSyntax;
                if (IsConfigurationType(typeName) || IsConfigurationType(descriptor.Type))
                {
                    if (invocation is not null && TryCaptureConfigurationUsage(memberAccess, invocation, typeName ?? descriptor.Type, tree) is { } configurationUsage)
                    {
                        info.ConfigurationUsages.Add(configurationUsage);
                    }

                    continue;
                }

                if (IsCacheService(typeName) || IsCacheService(descriptor.Type))
                {
                    var cacheType = IsCacheService(typeName) ? typeName : descriptor.Type;
                    if (TryCaptureCacheInvocation(memberAccess, invocation, cacheType, tree) is { } cacheInvocation)
                    {
                        info.CacheInvocations.Add(cacheInvocation);
                    }

                    continue;
                }

                if (TryResolveOptionsType(typeName) is { } optionsType)
                {
                    var line = GetLineNumber(tree, memberAccess);
                    info.OptionsUsages.Add(new OptionsUsage(optionsType, line));
                }

                if (typeName.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var line = GetLineNumber(tree, memberAccess);
                    info.RepositoryCalls.Add(new BackgroundServiceRepositoryCall(typeName, memberAccess.Name.Identifier.Text, line));
                }
            }

            foreach (var elementAccess in method.DescendantNodes().OfType<ElementAccessExpressionSyntax>())
            {
                if (elementAccess.Expression is not IdentifierNameSyntax identifier)
                {
                    continue;
                }

                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (!fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    continue;
                }

                var resolvedType = ResolveImplementationType(descriptor.Type) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree) is { } configurationUsage)
                {
                    info.ConfigurationUsages.Add(configurationUsage);
                }
            }
        }

        _backgroundServices[fqdn] = info;
    }

    private void EmitBackgroundServices()
    {
        foreach (var service in _backgroundServices.Values)
        {
            var id = StableId.For("app.background_service", service.Fqdn, service.Assembly, service.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.background_service",
                Name = service.Name,
                Fqdn = service.Fqdn,
                Assembly = service.Assembly,
                Project = service.Project,
                FilePath = service.FilePath,
                Span = service.Span,
                SymbolId = service.SymbolId,
                Tags = new[] { "app" }
            };

            foreach (var usage in service.ServiceUsages
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

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = serviceId!,
                    Kind = "uses_service",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ioc.resolve",
                        Location = new GraphLocation { File = service.FilePath, Line = service.Span.StartLine }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, service.Span)
                });
            }

            foreach (var cache in service.CacheInvocations)
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
                    From = id,
                    To = cacheId,
                    Kind = "uses_cache",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "cache.operation",
                        Location = new GraphLocation { File = service.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, cache.Line)
                });
            }

            foreach (var optionsUsage in service.OptionsUsages)
            {
                var optionsId = EnsureOptionsNode(optionsUsage.OptionsType);
                if (optionsId is null)
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["options_type"] = optionsUsage.OptionsType
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = optionsId,
                    Kind = "uses_options",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "options.access",
                        Location = new GraphLocation { File = service.FilePath, Line = optionsUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, optionsUsage.Line)
                });
            }

            EmitConfigurationEdges(id, service.ConfigurationUsages);

            foreach (var call in service.RepositoryCalls)
            {
                if (!TryResolveNodeReference(call.RepositoryType, out var repository))
                {
                    continue;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "background_service",
                        Location = new GraphLocation { File = service.FilePath, Line = call.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = call.Method
                    },
                    Evidence = CreateEvidence(service.FilePath, call.Line)
                });
            }
        }
    }
}
