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
    private void AnalyzeRepository(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        var repository = new RepositoryInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, fieldLookup);

        if (classDeclaration.BaseList is { Types.Count: > 0 })
        {
            foreach (var baseType in classDeclaration.BaseList.Types)
            {
                var baseTypeName = baseType.Type.ToString();
                if (string.IsNullOrWhiteSpace(baseTypeName))
                {
                    continue;
                }

                if (baseTypeName.StartsWith("IControlledRepository<", StringComparison.Ordinal))
                {
                    var serviceType = QualifyTypeName(baseTypeName);
                    if (string.IsNullOrWhiteSpace(serviceType))
                    {
                        serviceType = baseTypeName;
                    }

                    RegisterServiceRegistration(
                        serviceType,
                        fqdn,
                        "Scoped (inferred)",
                        filePath,
                        span,
                        project);
                }
            }
        }

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(p => p.Identifier.Text, p => p.Type?.ToString(), StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var local in method.DescendantNodes().OfType<LocalDeclarationStatementSyntax>())
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer?.Value is ObjectCreationExpressionSyntax creation)
                    {
                        resolvedType = creation.Type.ToString();
                    }

                    localVariables[variable.Identifier.Text] = resolvedType;
                }
            }

            foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax identifier } access &&
                    fieldLookup.TryGetValue(identifier.Identifier.Text.TrimStart('_'), out var descriptor))
                {
                    var resolvedType = ResolveImplementationType(descriptor.Type) ?? descriptor.Type;
                    if (IsConfigurationType(resolvedType) || IsConfigurationType(descriptor.Type))
                    {
                        if (TryCaptureConfigurationUsage(access, invocation, resolvedType ?? descriptor.Type, tree) is { } configurationUsage)
                        {
                            repository.ConfigurationUsages.Add(configurationUsage);
                        }

                        continue;
                    }

                    if (descriptor.Type.Contains("IMapper", StringComparison.Ordinal))
                    {
                        if (access.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                        {
                            var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                            var sourceExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                                ? resolved
                                : null;
                            var line = GetLineNumber(tree, invocation);
                            repository.MapperCalls.Add(new RepositoryMapperCall(sourceType, destination, line));
                        }
                    }
                    else
                    {
                        var cacheType = resolvedType;
                        if (IsCacheService(cacheType) || IsCacheService(descriptor.Type))
                        {
                            var resolvedCacheType = IsCacheService(cacheType) ? cacheType : descriptor.Type;
                            if (TryCaptureCacheInvocation(access, invocation, resolvedCacheType, tree) is { } cacheInvocation)
                            {
                                repository.CacheInvocations.Add(cacheInvocation);
                            }

                            continue;
                        }

                        if (TryResolveOptionsType(cacheType) is { } optionsType)
                        {
                            var line = GetLineNumber(tree, access);
                            repository.OptionsUsages.Add(new OptionsUsage(optionsType, line));
                            continue;
                        }
                    }
                }
                if (invocation.Expression is MemberAccessExpressionSyntax extensionAccess)
                {
                    if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectTo" } projectTo)
                    {
                        var destination = projectTo.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            repository.MapperCalls.Add(new RepositoryMapperCall(sourceType, destination, line));
                        }
                    }
                    else if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                    {
                        var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            repository.MapperCalls.Add(new RepositoryMapperCall(sourceType, destination, line));
                        }
                    }
                }
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: MemberAccessExpressionSyntax inner, Name.Identifier.Text: var methodName } &&
                    inner.Expression is IdentifierNameSyntax dbIdentifier &&
                    fieldLookup.TryGetValue(dbIdentifier.Identifier.Text.TrimStart('_'), out var dbType) &&
                    dbType.Type.Contains("DbContext", StringComparison.Ordinal))
                {
                    var dbSet = inner.Name.Identifier.Text;
                    var line = GetLineNumber(tree, invocation);
                    var operation = DetermineRepositoryOperation(methodName);
                    repository.DbAccesses.Add(new RepositoryDbAccess(dbSet, methodName, line, operation));
                }
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax contextIdentifier, Name.Identifier.Text: var contextMethod } &&
                    fieldLookup.TryGetValue(contextIdentifier.Identifier.Text.TrimStart('_'), out var dbContextType) &&
                    dbContextType.Type.Contains("DbContext", StringComparison.Ordinal))
                {
                    var line = GetLineNumber(tree, invocation);
                    var operation = DetermineRepositoryOperation(contextMethod);
                    repository.DbAccesses.Add(new RepositoryDbAccess(contextIdentifier.Identifier.Text, contextMethod, line, operation));
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
                    repository.ConfigurationUsages.Add(configurationUsage);
                }
            }
        }

        _repositories[fqdn] = repository;
    }

    private void EmitRepositories()
    {
        foreach (var repository in _repositories.Values)
        {
            var id = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.repository",
                Name = repository.Name,
                Fqdn = repository.Fqdn,
                Assembly = repository.Assembly,
                Project = repository.Project,
                FilePath = repository.FilePath,
                Span = repository.Span,
                SymbolId = repository.SymbolId,
                Tags = new[] { "app", "data" }
            };

            foreach (var mapping in repository.MapperCalls)
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
                        Location = new GraphLocation { File = repository.FilePath, Line = mapping.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(repository.FilePath, mapping.Line)
                });
            }

            foreach (var access in repository.DbAccesses)
            {
                if (string.IsNullOrWhiteSpace(access.Member))
                {
                    continue;
                }

                var entity = _entities.Values.FirstOrDefault(e => e.DbSetProperties.Any(p => p.Name.Equals(access.Member, StringComparison.Ordinal)))
                    ?? _entities.Values.FirstOrDefault(e => access.Member.StartsWith(e.Name, StringComparison.Ordinal));

                if (entity is null)
                {
                    continue;
                }

                var entityId = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
                var edgeKind = access.Operation switch
                {
                    "insert" => "inserts_into",
                    "update" => "updates",
                    "delete" => "deletes_from",
                    "upsert" => "upserts",
                    "write" => "writes_to",
                    _ => "queries"
                };
                var transformType = edgeKind switch
                {
                    "inserts_into" => "ef.insert",
                    "updates" => "ef.update",
                    "deletes_from" => "ef.delete",
                    "upserts" => "ef.upsert",
                    "writes_to" => "ef.write",
                    _ => "ef.query"
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = entityId,
                    Kind = edgeKind,
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = transformType,
                        Location = new GraphLocation { File = repository.FilePath, Line = access.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["operation"] = access.Operation
                    },
                    Evidence = CreateEvidence(repository.FilePath, access.Line)
                });

                if (_tables.TryGetValue(entity.TableName, out var table))
                {
                    var tableId = StableId.For("db.table", table.Name, entity.Assembly, entity.SymbolId);
                    _edges.Add(new GraphEdge
                    {
                        From = entityId,
                        To = tableId,
                        Kind = edgeKind,
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = transformType,
                            Location = new GraphLocation { File = repository.FilePath, Line = access.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["operation"] = access.Operation
                        },
                        Evidence = CreateEvidence(repository.FilePath, access.Line)
                    });
                }
            }

            foreach (var cache in repository.CacheInvocations)
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
                        Location = new GraphLocation { File = repository.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(repository.FilePath, cache.Line)
                });
            }

            foreach (var optionsUsage in repository.OptionsUsages)
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
                        Location = new GraphLocation { File = repository.FilePath, Line = optionsUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(repository.FilePath, optionsUsage.Line)
                });
            }

            EmitConfigurationEdges(id, repository.ConfigurationUsages);
        }
    }

}
