using System;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public sealed class FatherFlowEngine : IFlowEngine
{
    public string Build(
        GraphDocument document,
        FlowWorkspaceIndex? workspace,
        Func<GraphNode, bool>? controllerPredicate = null,
        string format = "md",
        int? maxDepth = null)
        => FlowBuilder.BuildFlows(document, controllerPredicate ?? (_ => true), workspace, maxDepth);
}
