using System.Collections.Generic;
using System.Text;
using GraphKit.Graph;

using static GraphKit.Outputs.Utilities;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    public static void AppendControllerFlow(StringBuilder builder, FlowRenderState state, string controllerName, IReadOnlyList<GraphNode> actions)
    {
        if (!string.IsNullOrWhiteSpace(controllerName))
        {
            builder.AppendLine($"## {controllerName}");
            builder.AppendLine();
        }

        var primaryAction = actions.Count > 0 ? actions[0] : null;
        var impact = new ImpactAccumulator(GetAssemblyRoot(primaryAction?.Assembly ?? string.Empty));
        state.PushImpact(impact);
        try
        {
            for (var i = 0; i < actions.Count; i++)
            {
                state.ResetPerFlowState();
                AppendEndpointFlow(builder, state, actions[i], indent: 0);
                if (i < actions.Count - 1)
                {
                    builder.AppendLine();
                }
            }
            AppendImpactSummary(builder, impact);
        }
        finally
        {
            state.PopImpact();
        }
    }
}
