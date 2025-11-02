using System.Collections.Generic;
using System.IO;
using GraphKit.Analyzers;
using GraphKit.Facts;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Outputs.Facts;
using GraphKit.Workspace;

namespace GraphKit;

public sealed class GraphGenerator
{
    private const string AnalyzerVersion = "0.2.1";

    public async Task<GraphDocument> GenerateAsync(GraphGenerationOptions options, CancellationToken cancellationToken = default)
    {
        var loader = new WorkspaceLoader(options.WorkspacePath, options.Solutions);
        var projects = await loader.LoadAsync(cancellationToken); // process all solutions/projects without filtering
    Console.WriteLine($"[graph] Loaded {projects.Count} projects. Memory={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        var fingerprints = await ProjectFingerprint.ComputeAsync(projects, options.WorkspacePath, cancellationToken);
        var cacheManager = new WorkspaceCacheManager(options.WorkspacePath, options.OutputDirectory);
        var outputWriter = new GraphOutputWriter(options.WorkspacePath, options.OutputDirectory);
        var cachedDocument = await cacheManager.TryLoadCachedDocumentAsync(AnalyzerVersion, fingerprints, cancellationToken);
        if (cachedDocument is not null)
        {
            Console.WriteLine("[graph] Cache hit. Skipping analysis and reusing existing graph.");
            await outputWriter.WriteAsync(cachedDocument, AnalyzerVersion, cancellationToken);
            return cachedDocument;
        }

        var factWriter = new FactWriter();
        var analyzer = new ProjectAnalyzer(options.WorkspacePath, factWriter);
        await Parallel.ForEachAsync(projects, cancellationToken, async (project, ct) =>
        {
            await analyzer.AnalyzeProjectAsync(project, cancellationToken);
            Console.WriteLine($"[graph] Analyzed project {project.AssemblyName} ({project.SourceFiles.Count} files). Nodes={analyzer.NodeCount} Edges={analyzer.EdgeCount} Mem={GC.GetTotalMemory(false) / 1024 / 1024:F1}MB");
        });

        var document = analyzer.BuildDocument(AnalyzerVersion);
    Console.WriteLine($"[graph] Built document. Nodes={document.Nodes.Count} Edges={document.Edges.Count} Mem={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        PopulateFactsFromGraph(document, factWriter);

        var factBag = FactsPipeline.Finalize(factWriter);
        var factsOutputDirectory = Path.GetFullPath(Path.Combine(options.WorkspacePath, options.OutputDirectory));
        var factsPath = Path.Combine(factsOutputDirectory, "facts.json");
        Console.WriteLine($"[facts] Collected Nodes={factBag.Nodes.Count} Edges={factBag.Edges.Count}");
        FactsJsonWriter.Write(factBag, factsPath);

        await outputWriter.WriteAsync(document, AnalyzerVersion, cancellationToken);
        await cacheManager.SaveAsync(AnalyzerVersion, document, fingerprints, cancellationToken);

        return document;
    }

    private static string MapConfidence(double value)
        => value >= 0.9 ? "High" : value >= 0.6 ? "Medium" : "Low";

    private static string MapProvenance(string source) => source switch
    {
        "static" => "Static",
        "synthetic" => "Heuristic",
        "runtime" => "Interprocedural",
        _ => "Unknown"
    };

    private static void PopulateFactsFromGraph(GraphDocument document, FactWriter facts)
    {
        foreach (var node in document.Nodes)
        {
            var props = new Dictionary<string, object?>
            {
                ["name"] = node.Name,
                ["fqdn"] = node.Fqdn,
                ["assembly"] = node.Assembly,
                ["project"] = node.Project,
                ["file_path"] = node.FilePath,
                ["symbol_id"] = node.SymbolId
            };

            if (node.Tags is { Count: > 0 })
            {
                props["tags"] = node.Tags.ToArray();
            }

            if (node.Span is { } span)
            {
                props["span"] = new Dictionary<string, object?>
                {
                    ["start_line"] = span.StartLine,
                    ["end_line"] = span.EndLine
                };
            }

            if (node.Props is { Count: > 0 })
            {
                foreach (var kv in node.Props)
                {
                    props[kv.Key] = kv.Value;
                }
            }

            facts.AddNode(new NodeFact(node.Id, node.Type, props));
        }

        foreach (var edge in document.Edges)
        {
            var props = new Dictionary<string, object?>
            {
                ["provenance"] = MapProvenance(edge.Source),
                ["confidence"] = MapConfidence(edge.Confidence)
            };

            if (edge.Transform?.Location is { } loc)
            {
                props["file"] = loc.File;
                props["line"] = loc.Line;
            }

            if (edge.Props is { Count: > 0 })
            {
                foreach (var kv in edge.Props)
                {
                    props[kv.Key] = kv.Value;
                }
            }

            facts.AddEdge(new EdgeFact(edge.From, edge.To, edge.Kind, props));
        }
    }
}
