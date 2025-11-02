using System;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public interface IFlowEngine
{
    string Build(
        GraphDocument document,
        FlowWorkspaceIndex? workspace,
        Func<GraphNode, bool>? controllerPredicate = null,
        string format = "md",
        int? maxDepth = null);
}
