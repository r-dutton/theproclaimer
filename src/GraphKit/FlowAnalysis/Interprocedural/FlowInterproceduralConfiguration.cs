using System;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace GraphKit.FlowAnalysis.Interprocedural;

public sealed record FlowInterproceduralConfiguration(
    InterproceduralAnalysisKind AnalysisKind,
    uint MaxInterproceduralCallChainLength,
    uint MaxInterproceduralLambdaOrLocalFunctionCallChainLength)
{
    public static FlowInterproceduralConfiguration Create(
        InterproceduralAnalysisKind analysisKind,
        int maxCallChainLength,
        int maxLambdaOrLocalFunctionDepth)
    {
        var normalizedCallChain = Math.Max(0, maxCallChainLength);
        var normalizedLambdaDepth = Math.Max(0, maxLambdaOrLocalFunctionDepth);

        return new FlowInterproceduralConfiguration(
            analysisKind,
            (uint)normalizedCallChain,
            (uint)normalizedLambdaDepth);
    }
}
