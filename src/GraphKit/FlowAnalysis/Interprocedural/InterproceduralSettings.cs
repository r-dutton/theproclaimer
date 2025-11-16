using System;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace GraphKit.FlowAnalysis.Interprocedural;

public readonly struct InterproceduralSettings : IEquatable<InterproceduralSettings>
{
    public InterproceduralSettings(
        InterproceduralAnalysisKind kind,
        int maxCallChainLength,
        int maxLambdaOrLocalFunctionDepth)
    {
        Kind = kind;
        MaxCallChainLength = maxCallChainLength;
        MaxLambdaOrLocalFunctionDepth = maxLambdaOrLocalFunctionDepth;
    }

    public InterproceduralAnalysisKind Kind { get; }

    public int MaxCallChainLength { get; }

    public int MaxLambdaOrLocalFunctionDepth { get; }

    public bool Equals(InterproceduralSettings other)
        => Kind == other.Kind
            && MaxCallChainLength == other.MaxCallChainLength
            && MaxLambdaOrLocalFunctionDepth == other.MaxLambdaOrLocalFunctionDepth;

    public override bool Equals(object? obj)
        => obj is InterproceduralSettings other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine((int)Kind, MaxCallChainLength, MaxLambdaOrLocalFunctionDepth);
}
