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
        var loader = new WorkspaceLoader(options.WorkspacePath);
        var projects = await loader.LoadAsync(cancellationToken);

        var analyzer = new ProjectAnalyzer(options.WorkspacePath);
        foreach (var project in projects)
        {
            await analyzer.AnalyzeProjectAsync(project, cancellationToken);
        }

        var document = analyzer.BuildDocument(AnalyzerVersion);

        var outputWriter = new GraphOutputWriter(options.WorkspacePath, options.OutputDirectory);
        await outputWriter.WriteAsync(document, AnalyzerVersion, cancellationToken);

        return document;
    }
}
