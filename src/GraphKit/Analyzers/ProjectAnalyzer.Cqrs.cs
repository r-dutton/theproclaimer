using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzeHandler(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var generic = classDeclaration.BaseList?.Types
            .FirstOrDefault(t => t.Type is GenericNameSyntax { Identifier.Text: "IRequestHandler" } g)
            ?.Type as GenericNameSyntax;

        if (generic is null)
        {
            return;
        }

        var requestType = generic.TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;
        var responseType = generic.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void";

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var handlerInfo = new HandlerInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, requestType, responseType);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var descriptor in fieldTypes.Values)
        {
            handlerInfo.ServiceUsages.Add(new ServiceUsage(descriptor.Type, descriptor.Line));
        }

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(p => p.Identifier.Text, p => p.Type?.ToString(), StringComparer.OrdinalIgnoreCase);

            foreach (var memberAccess in method.DescendantNodes().OfType<MemberAccessExpressionSyntax>())
            {
                if (memberAccess.Expression is IdentifierNameSyntax identifier)
                {
                    var fieldName = identifier.Identifier.Text.TrimStart('_');
                    if (fieldLookup.TryGetValue(fieldName, out var descriptor))
                    {
                        var typeName = descriptor.Type;
                        var resolvedType = ResolveImplementationType(typeName) ?? typeName;
                        if (typeName.Contains("DbContext", StringComparison.Ordinal))
                        {
                            var accessedMember = memberAccess.Name.Identifier.Text;
                            var line = GetLineNumber(tree, memberAccess);
                            handlerInfo.DbContextAccesses.Add(new HandlerDbAccess(typeName, accessedMember, line));
                        }
                        else if (typeName.Contains("Publisher", StringComparison.Ordinal))
                        {
                            var line = GetLineNumber(tree, memberAccess);
                            handlerInfo.PublisherCalls.Add(new HandlerPublisherCall(typeName, memberAccess.Name.Identifier.Text, line));
                        }
                        else if (resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                        {
                            var line = GetLineNumber(tree, memberAccess);
                            handlerInfo.RepositoryCalls.Add(new HandlerRepositoryCall(resolvedType, memberAccess.Name.Identifier.Text, line));
                        }
                        else if (typeName.Contains("IMapper", StringComparison.Ordinal) && memberAccess.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                        {
                            var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                            var sourceExpression = memberAccess.Parent is InvocationExpressionSyntax invocation
                                ? invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString()
                                : null;
                            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                                ? resolved
                                : null;
                            var line = GetLineNumber(tree, memberAccess);
                            handlerInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                        }
                    }
                }
            }
        }

        _handlers[fqdn] = handlerInfo;
    }

    private void PromoteDerivedRequests()
    {
        foreach (var candidate in _derivedRequestCandidates)
        {
            if (_requests.ContainsKey(candidate.Fqdn))
            {
                continue;
            }

            if (_requests.TryGetValue(candidate.BaseType, out var _) ||
                _requests.Values.Any(r => r.Name.Equals(candidate.BaseType.Split('.').Last(), StringComparison.Ordinal)))
            {
                _requests[candidate.Fqdn] = new RequestInfo(candidate.Fqdn, candidate.Assembly, candidate.Project, candidate.FilePath, candidate.Span, candidate.SymbolId, candidate.Name);
            }
        }
    }

    private void EmitRequests()
    {
        foreach (var request in _requests.Values)
        {
            var id = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.request",
                Name = request.Name,
                Fqdn = request.Fqdn,
                Assembly = request.Assembly,
                Project = request.Project,
                FilePath = request.FilePath,
                Span = request.Span,
                SymbolId = request.SymbolId,
                Tags = new[] { "app" }
            };
        }
    }

    private void EmitHandlers()
    {
        foreach (var handler in _handlers.Values)
        {
            var id = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.handler",
                Name = handler.Name,
                Fqdn = handler.Fqdn,
                Assembly = handler.Assembly,
                Project = handler.Project,
                FilePath = handler.FilePath,
                Span = handler.Span,
                SymbolId = handler.SymbolId,
                Tags = new[] { "app" }
            };

            if (FindRequestByType(handler.RequestType) is { } request)
            {
                var requestId = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = requestId,
                    To = id,
                    Kind = "handled_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.handler",
                        MethodSpan = handler.Span
                    },
                    Evidence = CreateEvidence(handler.FilePath, handler.Span)
                });
            }

            foreach (var repositoryCall in handler.RepositoryCalls)
            {
                var targetType = ResolveImplementationType(repositoryCall.RepositoryType) ?? repositoryCall.RepositoryType;
                var repositoryName = targetType.Split('.').Last();

                if (_repositories.Values.FirstOrDefault(r => r.Name.Equals(repositoryName, StringComparison.Ordinal)) is { } repository)
                {
                    var repositoryId = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = repositoryId,
                        Kind = "calls",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "mediatr.handler",
                            Location = new GraphLocation { File = handler.FilePath, Line = repositoryCall.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["method"] = repositoryCall.Method
                        },
                        Evidence = CreateEvidence(handler.FilePath, repositoryCall.Line)
                    });
                }
            }

            foreach (var mapping in handler.MapperCalls)
            {
                if (string.IsNullOrWhiteSpace(mapping.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(mapping.DestinationType, out var destination))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["destination_type"] = mapping.DestinationType
                };

                if (!string.IsNullOrWhiteSpace(mapping.SourceType))
                {
                    props["source_type"] = mapping.SourceType!;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = destination.Id,
                    Kind = "maps_to",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "automapper.map",
                        Location = new GraphLocation { File = handler.FilePath, Line = mapping.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, mapping.Line)
                });
            }

            foreach (var usage in handler.ServiceUsages
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
                        Location = new GraphLocation { File = handler.FilePath, Line = usage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, usage.Line)
                });
            }
        }
    }
}
