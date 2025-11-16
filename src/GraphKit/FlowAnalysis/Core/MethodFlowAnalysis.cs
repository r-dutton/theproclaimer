using Analyzer.Utilities.FlowAnalysis.Analysis.TaintedDataAnalysis;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using CopyAnalysisResult = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.DataFlowAnalysisResult<Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis.CopyBlockAnalysisResult, Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis.CopyAbstractValue>;
using ValueContentAnalysisResult = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.DataFlowAnalysisResult<Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentBlockAnalysisResult, Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentAbstractValue>;

namespace GraphKit.FlowAnalysis.Core;

public sealed class MethodFlowAnalysis
{
    public MethodFlowAnalysis(MethodFlowContext context, InterproceduralSettings settings)
    {
        Context = context;
        Settings = settings;
    }

    public MethodFlowContext Context { get; }

    public InterproceduralSettings Settings { get; }

    public PointsToAnalysisResult? PointsToAnalysis { get; internal set; }

    public ValueContentAnalysisResult? ValueContentAnalysis { get; internal set; }

    public CopyAnalysisResult? CopyAnalysis { get; internal set; }

    public TaintedDataAnalysisResult? TaintedDataAnalysis { get; internal set; }

    public bool PointsToComputed { get; internal set; }

    public bool ValueContentComputed { get; internal set; }

    public bool CopyAnalysisComputed { get; internal set; }

    public bool TaintedDataComputed { get; internal set; }
}
