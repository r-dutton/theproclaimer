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
    private void AnalyzeDbContext(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var context = new DbContextInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);

        foreach (var property in classDeclaration.Members.OfType<PropertyDeclarationSyntax>())
        {
            if (property.Type is not GenericNameSyntax { Identifier.Text: "DbSet" } dbSet || dbSet.TypeArgumentList.Arguments.Count == 0)
            {
                continue;
            }

            var entityType = dbSet.TypeArgumentList.Arguments[0].ToString();
            var line = GetLineNumber(tree, property);
            var propertyName = property.Identifier.Text;

            context.DbSets.Add(new DbContextDbSet(entityType, propertyName, line));

            if (TryFindEntityByTypeName(entityType) is { } entity)
            {
                AddDbSetToEntity(entity, propertyName, line);
            }
        }

        _dbContexts[fqdn] = context;
    }

    private void AnalyzeEntity(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);
        var tableAttribute = classDeclaration.AttributeLists.SelectMany(list => list.Attributes)
            .FirstOrDefault(attr => attr.Name.ToString().Contains("Table", StringComparison.Ordinal));
        var tableName = tableAttribute?.ArgumentList?.Arguments.FirstOrDefault()?.ToString().Trim('"') ?? className + "s";

        var entity = new EntityInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, tableName);
        _entities[fqdn] = entity;
        _tables[tableName] = new TableInfo(tableName, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId);

        foreach (var context in _dbContexts.Values)
        {
            foreach (var dbSet in context.DbSets.Where(set => EntityMatchesType(entity, set.EntityType)))
            {
                AddDbSetToEntity(entity, dbSet.PropertyName, dbSet.Line);
            }
        }
    }

    private void EmitEntities()
    {
        foreach (var entity in _entities.Values)
        {
            var id = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "ef.entity",
                Name = entity.Name,
                Fqdn = entity.Fqdn,
                Assembly = entity.Assembly,
                Project = entity.Project,
                FilePath = entity.FilePath,
                Span = entity.Span,
                SymbolId = entity.SymbolId,
                Tags = new[] { "data" },
                Props = new Dictionary<string, object>
                {
                    ["table"] = entity.TableName
                }
            };

            if (_tables.TryGetValue(entity.TableName, out var table))
            {
                var tableId = StableId.For("db.table", table.Name, entity.Assembly, entity.SymbolId);
                _nodes[tableId] = new GraphNode
                {
                    Id = tableId,
                    Type = "db.table",
                    Name = table.Name,
                    Fqdn = table.Name,
                    Assembly = entity.Assembly,
                    Project = entity.Project,
                    FilePath = table.FilePath,
                    Span = entity.Span,
                    SymbolId = entity.SymbolId,
                    Tags = new[] { "db" }
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = tableId,
                    Kind = "reads_from",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ef.query",
                        MethodSpan = entity.Span
                    },
                    Evidence = CreateEvidence(entity.FilePath, entity.Span)
                });
            }
        }

        foreach (var handler in _handlers.Values)
        {
            var handlerId = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            foreach (var access in handler.DbContextAccesses)
            {
                var entity = _entities.Values.FirstOrDefault(e => e.Name == access.Member || access.Member.StartsWith(e.Name, StringComparison.Ordinal));
                if (entity is null)
                {
                    continue;
                }

                var entityId = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = handlerId,
                    To = entityId,
                    Kind = "queries",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ef.query",
                        Location = new GraphLocation { File = handler.FilePath, Line = access.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, access.Line)
                });
            }
        }
    }

    private void EmitDbContexts()
    {
        foreach (var context in _dbContexts.Values)
        {
            var id = StableId.For("ef.db_context", context.Fqdn, context.Assembly, context.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "ef.db_context",
                Name = context.Name,
                Fqdn = context.Fqdn,
                Assembly = context.Assembly,
                Project = context.Project,
                FilePath = context.FilePath,
                Span = context.Span,
                SymbolId = context.SymbolId,
                Tags = new[] { "data" }
            };

            foreach (var dbSet in context.DbSets)
            {
                var entity = FindEntityForDbSet(dbSet.EntityType);
                if (entity is null)
                {
                    continue;
                }

                var entityId = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
                var props = new Dictionary<string, object>
                {
                    ["dbset"] = dbSet.PropertyName
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = entityId,
                    Kind = "manages",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ef.dbset",
                        Location = new GraphLocation { File = context.FilePath, Line = dbSet.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(context.FilePath, dbSet.Line)
                });
            }
        }
    }

    private static bool EntityMatchesType(EntityInfo entity, string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        return string.Equals(entity.Fqdn, typeName, StringComparison.OrdinalIgnoreCase)
            || string.Equals(entity.Name, typeName, StringComparison.OrdinalIgnoreCase)
            || string.Equals(entity.Name, typeName.Split('.').Last(), StringComparison.OrdinalIgnoreCase);
    }

    private EntityInfo? TryFindEntityByTypeName(string typeName)
    {
        if (_entities.TryGetValue(typeName, out var entity))
        {
            return entity;
        }

        var simple = typeName.Split('.').Last();
        return _entities.Values.FirstOrDefault(e =>
            string.Equals(e.Fqdn, typeName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(e.Name, simple, StringComparison.OrdinalIgnoreCase));
    }

    private EntityInfo? FindEntityForDbSet(string entityType)
        => TryFindEntityByTypeName(entityType);

    private static void AddDbSetToEntity(EntityInfo entity, string propertyName, int line)
    {
        if (entity.DbSetProperties.Any(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        entity.DbSetProperties.Add(new DbSetInfo(propertyName, line));
    }
}
