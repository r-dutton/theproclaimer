using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Analysis;

public sealed class GraphPipeline
{
    public GraphDocument Build(GraphPipelineOptions options)
    {
        var loader = new WorkspaceLoader();
        var workspace = loader.Load(options.WorkspaceRoot);
        var analyzer = new WorkspaceAnalyzer(options.WorkspaceRoot);
        return analyzer.Analyze(workspace);
    }
}

public sealed class GraphPipelineOptions
{
    public string WorkspaceRoot { get; init; } = string.Empty;
}
