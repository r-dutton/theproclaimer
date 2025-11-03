using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using FlowAnalysisEngine = GraphKit.FlowAnalysis.Core.FlowAnalysis;

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

        var model = project.GetModel(tree);
        var compilation = project.Compilation;
        var callsitePredicate = ComposeInterproceduralPredicate(ShouldExpandForCqrsEfHttpMap);
        var pointsToFacade = CreatePointsToFacade(callsitePredicate);
        var valueContentFacade = CreateValueContentFacade(callsitePredicate);
        var registeredMappings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var invocation in Descendants<InvocationExpressionSyntax>(classDeclaration))
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
                RegisterMappingDefinition(project, profileFqdn, profileId, profileFile, profileSpan, source, destination, line, registeredMappings);
            }
        }

        foreach (var constructor in classDeclaration.Members.OfType<ConstructorDeclarationSyntax>())
        {
            IMethodSymbol? ctorSymbol = null;
            try
            {
                ctorSymbol = model.GetDeclaredSymbol(constructor) as IMethodSymbol;
            }
            catch (ArgumentException)
            {
                ctorSymbol = null;
            }

            if (ctorSymbol is not null && TryAcquireMethodAnalysis(ctorSymbol))
            {
                var visitor = new MappingOperationVisitor(this, model, project, profileFqdn, profileId, profileFile, profileSpan, registeredMappings, pointsToFacade, valueContentFacade, _facts);
                FlowAnalysisEngine.AnalyzeMethod(
                    compilation,
                    model,
                    ctorSymbol,
                    InterproceduralConfiguration,
                    callsitePredicate,
                    visitor);
            }
        }

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            IMethodSymbol? methodSymbol = null;
            try
            {
                methodSymbol = model.GetDeclaredSymbol(method) as IMethodSymbol;
            }
            catch (ArgumentException)
            {
                methodSymbol = null;
            }

            if (methodSymbol is not null && TryAcquireMethodAnalysis(methodSymbol))
            {
                var visitor = new MappingOperationVisitor(this, model, project, profileFqdn, profileId, profileFile, profileSpan, registeredMappings, pointsToFacade, valueContentFacade, _facts);
                FlowAnalysisEngine.AnalyzeMethod(
                    compilation,
                    model,
                    methodSymbol,
                    InterproceduralConfiguration,
                    callsitePredicate,
                    visitor);
            }
        }
    }

    private void EmitMappings()
    {
        foreach (var mapping in _mappings)
        {
            var preferredAssembly = GuessAssemblyName(mapping.ProfileFqdn);
            if (!TryResolveNodeReference(mapping.SourceType, out var sourceNode, preferredAssembly) ||
                !TryResolveNodeReference(mapping.DestinationType, out var destinationNode, preferredAssembly))
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

    private void RegisterMappingDefinition(
        ProjectInfo project,
        string profileFqdn,
        string profileId,
        string profileFile,
        GraphSpan profileSpan,
        string source,
        string destination,
        int line,
        HashSet<string> registeredMappings)
    {
        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(destination))
        {
            return;
        }

        var mappingKey = $"{source}->{destination}";
        if (!registeredMappings.Add(mappingKey))
        {
            return;
        }

        var mapFqdn = $"{profileFqdn}.CreateMap<{source},{destination}>";
        var mapSymbolId = $"M:{mapFqdn}";
        var mapSpan = new GraphSpan { StartLine = line, EndLine = line };
        var mapId = StableId.For("mapping.automapper.map", mapFqdn, project.AssemblyName, mapSymbolId);

        if (!_nodes.ContainsKey(mapId))
        {
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
        }

        var factProps = new Dictionary<string, object?>
        {
            ["source_type"] = source,
            ["destination_type"] = destination,
            ["profile_fqdn"] = profileFqdn,
            ["profile_id"] = profileId,
            ["file_path"] = profileFile,
            ["line"] = line
        };
        AddSource(factProps, profileFile, line);
        _facts.AddNode(new NodeFact(mapId, "mapping.automapper.map", factProps));

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

        _mappings.Add(new MappingInfo(mapId, profileFile, mapSpan, profileFqdn, mappingKey, source, destination));
    }
}
