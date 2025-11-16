using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using ValueContentAnalysisResult = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.DataFlowAnalysisResult<Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentBlockAnalysisResult, Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentAbstractValue>;

namespace GraphKit.FlowAnalysis.Core;

public sealed class MethodFlowAnalysis
{
    public MethodFlowAnalysis(MethodFlowContext context)
    {
        Context = context;
    }

    public MethodFlowContext Context { get; }

    public PointsToAnalysisResult? PointsToAnalysis { get; internal set; }

    public ValueContentAnalysisResult? ValueContentAnalysis { get; internal set; }

    public bool PointsToComputed { get; internal set; }

    public InterproceduralSettings? PointsToSettings { get; internal set; }

    public FlowCallsitePredicate? PointsToPruningPredicate { get; internal set; }

    public bool PointsToIncludesCopyAnalysis { get; internal set; }

    public bool ValueContentComputed { get; internal set; }

    public bool ValueContentIncludesCopyAnalysis { get; internal set; }
}
