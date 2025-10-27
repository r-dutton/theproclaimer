using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public interface IFlowEngine
{
    string Build(GraphDocument document, FlowWorkspaceIndex? workspace, string format = "md", int? maxDepth = null);
}
