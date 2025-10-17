using GraphKit.Analyzers;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Workspace;

namespace GraphKit;

public sealed class GraphGenerator
{
    private const string AnalyzerVersion = "0.1.0";

    public async Task<GraphDocument> GenerateAsync(GraphGenerationOptions options, CancellationToken cancellationToken = default)
    {
        var loader = new WorkspaceLoader(options.WorkspacePath, options.Solutions);
        var projects = await loader.LoadAsync(cancellationToken); // process all solutions/projects without filtering
    Console.WriteLine($"[graph] Loaded {projects.Count} projects. Memory={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        var analyzer = new ProjectAnalyzer(options.WorkspacePath);
        foreach (var project in projects)
        {
            await analyzer.AnalyzeProjectAsync(project, cancellationToken);
            Console.WriteLine($"[graph] Analyzed project {project.AssemblyName} ({project.SourceFiles.Count} files). Nodes={analyzer.NodeCount} Edges={analyzer.EdgeCount} Mem={GC.GetTotalMemory(false)/1024/1024:F1}MB");
            // Opportunistic GC hint (non-filtering, full fidelity retained)
            if ((project.SourceFiles?.Count ?? 0) > 250)
            {
                GC.Collect();
            }
        }

        var document = analyzer.BuildDocument(AnalyzerVersion);
    Console.WriteLine($"[graph] Built document. Nodes={document.Nodes.Count} Edges={document.Edges.Count} Mem={GC.GetTotalMemory(false)/1024/1024:F1}MB");

        var outputWriter = new GraphOutputWriter(options.WorkspacePath, options.OutputDirectory);
        await outputWriter.WriteAsync(document, AnalyzerVersion, cancellationToken);

        return document;
    }
}
