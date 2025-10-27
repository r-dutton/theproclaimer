using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public sealed class FatherFlowEngine : IFlowEngine
{
    public string Build(GraphDocument document, FlowWorkspaceIndex? workspace, string format = "md", int? maxDepth = null)
        => FlowBuilder.BuildFlows(document, FlowFilter.Passes, workspace, maxDepth);
}
