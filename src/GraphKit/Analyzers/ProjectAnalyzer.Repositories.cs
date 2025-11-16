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
    /// <summary>
    /// Bind repositories discovered in the workspace to their canonical entity types using Roslyn symbols first,
    /// falling back to existing string-based heuristics only when necessary.
    /// </summary>
    private void BindRepositoryEntities(ProjectInfo project)
    {
        if (project?.Compilation is not { } compilation)
        {
            return;
        }

        foreach (var repository in _repositories.Values)
        {
            // Skip repositories that already have a bound entity type.
            if (!string.IsNullOrWhiteSpace(repository.EntityTypeFqdn))
            {
                continue;
            }

            // Try to resolve the repository symbol by its FQDN.
            var typeSymbol = compilation.GetTypeByMetadataName(repository.Fqdn);
            if (typeSymbol is null)
            {
                // Fall back to existing heuristics if we can't resolve the symbol.
                repository.EntityTypeFqdn = InferEntityTypeFromHeuristics(repository);
                continue;
            }

            // Generic repository implementations (e.g., IRepository<TEntity>).
            var entityFromGenerics = TryInferEntityFromGenericRepository(typeSymbol);
            if (!string.IsNullOrWhiteSpace(entityFromGenerics))
            {
                repository.EntityTypeFqdn = QualifyTypeName(entityFromGenerics!, repository.Assembly, repository.Project) ?? entityFromGenerics;
                continue;
            }

            // Reuse existing "ControlledEntities" as a strong hint if already populated.
            if (repository.ControlledEntities.Count == 1)
            {
                var single = repository.ControlledEntities.First();
                repository.EntityTypeFqdn = QualifyTypeName(single, repository.Assembly, repository.Project) ?? single;
                continue;
            }

            // Fall back to string-based heuristics derived from repository type name.
            repository.EntityTypeFqdn = InferEntityTypeFromHeuristics(repository);
        }
    }

    private static string? TryInferEntityFromGenericRepository(INamedTypeSymbol repositorySymbol)
    {
        // Direct generic repository types: Repository<TEntity> where TEntity is the first type argument.
        if (repositorySymbol.IsGenericType && repositorySymbol.TypeArguments.Length > 0)
        {
            var arg = repositorySymbol.TypeArguments[0];
            return arg.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        }

        // Interfaces implemented by the repository that might carry TEntity.
        foreach (var iface in repositorySymbol.AllInterfaces)
        {
            if (!iface.IsGenericType || iface.TypeArguments.Length == 0)
            {
                continue;
            }

            var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            if (ifaceName.Contains("Repository", StringComparison.OrdinalIgnoreCase))
            {
                var arg = iface.TypeArguments[0];
                return arg.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
            }
        }

        return null;
    }

    /// <summary>
    /// Use existing string-based helpers as a fallback when symbol-based inference fails.
    /// </summary>
    private string? InferEntityTypeFromHeuristics(RepositoryInfo repository)
    {
        // Try using the repository type itself first.
        var candidate = ExtractRepositoryEntityType(repository.Fqdn);
        if (!string.IsNullOrWhiteSpace(candidate))
        {
            var qualified = QualifyTypeName(candidate!, repository.Assembly, repository.Project);
            if (!string.IsNullOrWhiteSpace(qualified))
            {
                return qualified;
            }
        }

        // Try deriving from the simple repository name (e.g., BinderRepository -> Binder).
        var fromName = TryDeriveEntityTypeFromRepositoryName(repository.Fqdn);
        if (!string.IsNullOrWhiteSpace(fromName))
        {
            var qualified = QualifyTypeName(fromName!, repository.Assembly, repository.Project);
            if (!string.IsNullOrWhiteSpace(qualified))
            {
                return qualified;
            }
        }

        return null;
    }

    private void AnalyzeRepository(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        var repository = new RepositoryInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, fieldLookup);
        var model = project.GetModel(tree);
        var callsitePredicate = ComposeInterproceduralPredicate(ShouldExpandForCqrsEfHttpMap);
        var valueContent = CreateValueContentFacade(callsitePredicate);

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
                    else if (!serviceType.Contains('<', StringComparison.Ordinal))
                    {
                        serviceType = baseTypeName;
                    }

                    var entityType = SplitGenericArguments(baseTypeName).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(entityType))
                    {
                        var qualifiedEntity = QualifyTypeName(entityType);
                        repository.ControlledEntities.Add(!string.IsNullOrWhiteSpace(qualifiedEntity) ? qualifiedEntity : entityType);
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
            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(method))
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

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(method))
            {
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax identifier } access &&
                    fieldLookup.TryGetValue(identifier.Identifier.Text.TrimStart('_'), out var descriptor))
                {
                    var resolvedType = ResolveImplementationType(descriptor.Type, repository.Assembly, repository.Project) ?? descriptor.Type;
                    if (IsConfigurationType(resolvedType) || IsConfigurationType(descriptor.Type))
                    {
                        if (TryCaptureConfigurationUsage(access, invocation, resolvedType ?? descriptor.Type, tree, model, valueContent) is { } configurationUsage)
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
                        var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup, project.AssemblyName, project.RelativeDirectory);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            repository.MapperCalls.Add(new RepositoryMapperCall(sourceType, destination, line));
                        }
                    }
                    else if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                    {
                        var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup, project.AssemblyName, project.RelativeDirectory);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            repository.MapperCalls.Add(new RepositoryMapperCall(sourceType, destination, line));
                        }
                    }
                }
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: MemberAccessExpressionSyntax innerAccess, Name.Identifier.Text: var methodName } &&
                    innerAccess.Expression is IdentifierNameSyntax dbIdentifier &&
                    TryResolveDbContextType(dbIdentifier.Identifier.Text, fieldLookup, parameterTypes, localVariables, out _))
                {
                    var dbSet = innerAccess.Name.Identifier.Text;
                    var line = GetLineNumber(tree, invocation);
                    var operation = DetermineRepositoryOperation(methodName);
                    repository.DbAccesses.Add(new RepositoryDbAccess(dbSet, methodName, line, operation));
                }
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax contextIdentifier, Name.Identifier.Text: var contextMethod } &&
                    TryResolveDbContextType(contextIdentifier.Identifier.Text, fieldLookup, parameterTypes, localVariables, out _))
                {
                    var line = GetLineNumber(tree, invocation);
                    var operation = DetermineRepositoryOperation(contextMethod);
                    repository.DbAccesses.Add(new RepositoryDbAccess(contextIdentifier.Identifier.Text, contextMethod, line, operation));
                }
                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: InvocationExpressionSyntax innerInvocation, Name.Identifier.Text: var setMethod })
                {
                    var currentInvocation = innerInvocation;
                    while (currentInvocation.Expression is MemberAccessExpressionSyntax memberAccess)
                    {
                        if (memberAccess.Expression is IdentifierNameSyntax nameContextIdentifier &&
                            TryResolveDbContextType(nameContextIdentifier.Identifier.Text, fieldLookup, parameterTypes, localVariables, out _))
                        {
                            if (memberAccess.Name is GenericNameSyntax { Identifier.Text: "Set", TypeArgumentList.Arguments.Count: > 0 } generic)
                            {
                                var entityType = generic.TypeArgumentList.Arguments[0].ToString();
                                var line = GetLineNumber(tree, invocation);
                                var operation = DetermineRepositoryOperation(setMethod);
                                repository.DbAccesses.Add(new RepositoryDbAccess(entityType, setMethod, line, operation));
                            }
                            break;
                        }

                        if (memberAccess.Expression is InvocationExpressionSyntax nextInvocation)
                        {
                            currentInvocation = nextInvocation;
                            continue;
                        }

                        break;
                    }
                }
            }

            foreach (var elementAccess in Descendants<ElementAccessExpressionSyntax>(method))
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

                var resolvedType = ResolveImplementationType(descriptor.Type, repository.Assembly, repository.Project) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree, model, valueContent) is { } configurationUsage)
                {
                    repository.ConfigurationUsages.Add(configurationUsage);
                }
            }
        }

        _repositories[fqdn] = repository;
    }

    private static bool TryResolveDbContextType(
        string identifier,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        IReadOnlyDictionary<string, string?> parameterTypes,
        IReadOnlyDictionary<string, string> localVariables,
        out string? contextType)
    {
        static bool ContainsDbContext(string? type)
            => !string.IsNullOrWhiteSpace(type) && type!.Contains("DbContext", StringComparison.OrdinalIgnoreCase);

        var normalized = identifier.TrimStart('_');

        if (fieldLookup.TryGetValue(normalized, out var descriptor) && ContainsDbContext(descriptor.Type))
        {
            contextType = descriptor.Type;
            return true;
        }

        if (parameterTypes.TryGetValue(identifier, out var parameterType) && ContainsDbContext(parameterType))
        {
            contextType = parameterType;
            return true;
        }

        if (!string.Equals(identifier, normalized, StringComparison.Ordinal) &&
            parameterTypes.TryGetValue(normalized, out var trimmedParameterType) && ContainsDbContext(trimmedParameterType))
        {
            contextType = trimmedParameterType;
            return true;
        }

        if (localVariables.TryGetValue(identifier, out var localType) && ContainsDbContext(localType))
        {
            contextType = localType;
            return true;
        }

        if (!string.Equals(identifier, normalized, StringComparison.Ordinal) &&
            localVariables.TryGetValue(normalized, out var trimmedLocalType) && ContainsDbContext(trimmedLocalType))
        {
            contextType = trimmedLocalType;
            return true;
        }

        contextType = null;
        return false;
    }

    private void EmitRepositories()
    {
        foreach (var repository in _repositories.Values)
        {
            var id = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
            var tags = new List<string> { "app", "data", "repository" };
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
                Tags = tags.ToArray()
            };

            foreach (var mapping in repository.MapperCalls)
            {
                if (string.IsNullOrWhiteSpace(mapping.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(mapping.DestinationType, out var destination, repository.Assembly, repository.Project))
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
