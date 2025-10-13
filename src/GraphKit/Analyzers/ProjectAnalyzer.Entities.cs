using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzeDbContext(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        foreach (var property in classDeclaration.Members.OfType<PropertyDeclarationSyntax>())
        {
            if (property.Type is GenericNameSyntax { Identifier.Text: "DbSet" } dbSet)
            {
                var entityType = dbSet.TypeArgumentList.Arguments.First().ToString();
                if (_entities.TryGetValue(entityType, out var entity))
                {
                    entity.DbSetProperties.Add(new DbSetInfo(property.Identifier.Text, GetLineNumber(tree, property)));
                }
            }
        }
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
}
