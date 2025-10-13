using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzeMappingProfile(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        var profileName = classDeclaration.Identifier.Text;
        var profileFqdn = string.IsNullOrWhiteSpace(namespaceName) ? profileName : $"{namespaceName}.{profileName}";
        var profileSymbolId = $"T:{profileFqdn}";
        var profileFile = GetRelativePath(tree.FilePath);
        var profileSpan = ToGraphSpan(tree, classDeclaration);

        var profileId = StableId.For("mapping.automapper.profile", profileFqdn, project.AssemblyName, profileSymbolId);
        _nodes[profileId] = new GraphNode
        {
            Id = profileId,
            Type = "mapping.automapper.profile",
            Name = profileName,
            Fqdn = profileFqdn,
            Assembly = project.AssemblyName,
            Project = project.RelativeDirectory,
            FilePath = profileFile,
            Span = profileSpan,
            SymbolId = profileSymbolId,
            Tags = new[] { "mapping" }
        };

        foreach (var invocation in classDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            var generic = invocation.Expression switch
            {
                MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "CreateMap" }, Name: GenericNameSyntax g } => g,
                MemberAccessExpressionSyntax { Name: GenericNameSyntax g } member when member.Expression is ThisExpressionSyntax && g.Identifier.Text == "CreateMap" => g,
                GenericNameSyntax g when g.Identifier.Text == "CreateMap" => g,
                _ => null
            };

            if (generic is null)
            {
                continue;
            }

            if (generic.TypeArgumentList.Arguments.Count == 2)
            {
                var source = generic.TypeArgumentList.Arguments[0].ToString();
                var destination = generic.TypeArgumentList.Arguments[1].ToString();
                var line = GetLineNumber(tree, invocation);
                var mapFqdn = $"{profileFqdn}.CreateMap<{source},{destination}>";
                var mapSymbolId = $"M:{mapFqdn}";
                var mapSpan = new GraphSpan { StartLine = line, EndLine = line };
                var mapId = StableId.For("mapping.automapper.map", mapFqdn, project.AssemblyName, mapSymbolId);

                _nodes[mapId] = new GraphNode
                {
                    Id = mapId,
                    Type = "mapping.automapper.map",
                    Name = $"{source}->{destination}",
                    Fqdn = mapFqdn,
                    Assembly = project.AssemblyName,
                    Project = project.RelativeDirectory,
                    FilePath = profileFile,
                    Span = mapSpan,
                    SymbolId = mapSymbolId,
                    Tags = new[] { "mapping" },
                    Props = new Dictionary<string, object>
                    {
                        ["source_type"] = source,
                        ["destination_type"] = destination
                    }
                };

                _edges.Add(new GraphEdge
                {
                    From = mapId,
                    To = profileId,
                    Kind = "generated_from",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "automapper.create_map",
                        Location = new GraphLocation { File = profileFile, Line = line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["source_type"] = source,
                        ["destination_type"] = destination
                    },
                    Evidence = CreateEvidence(profileFile, line)
                });

                _mappings.Add(new MappingInfo(mapId, profileFile, mapSpan, profileFqdn, $"{source}->{destination}", source, destination));
            }
        }
    }

    private void EmitMappings()
    {
        foreach (var mapping in _mappings)
        {
            if (!TryResolveNodeReference(mapping.SourceType, out var sourceNode) || !TryResolveNodeReference(mapping.DestinationType, out var destinationNode))
            {
                continue;
            }

            _edges.Add(new GraphEdge
            {
                From = sourceNode.Id,
                To = destinationNode.Id,
                Kind = "converts_to",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "automapper.for_member",
                    Location = new GraphLocation { File = mapping.FilePath, Line = mapping.Span.StartLine }
                },
                Props = new Dictionary<string, object>
                {
                    ["profile"] = mapping.ProfileFqdn,
                    ["map"] = mapping.MapName
                },
                Evidence = CreateEvidence(mapping.FilePath, mapping.Span)
            });

            _edges.Add(new GraphEdge
            {
                From = mapping.MapId,
                To = destinationNode.Id,
                Kind = "maps_to",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "automapper.create_map",
                    Location = new GraphLocation { File = mapping.FilePath, Line = mapping.Span.StartLine }
                },
                Props = new Dictionary<string, object>
                {
                    ["source_type"] = mapping.SourceType,
                    ["destination_type"] = mapping.DestinationType
                },
                Evidence = CreateEvidence(mapping.FilePath, mapping.Span)
            });
        }
    }
}
