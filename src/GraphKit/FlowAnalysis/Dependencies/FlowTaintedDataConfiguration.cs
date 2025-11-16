using System.Collections.Immutable;
using Analyzer.Utilities.FlowAnalysis.Analysis.TaintedDataAnalysis;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed record FlowTaintedDataConfiguration(
    ImmutableArray<SourceInfo> Sources,
    ImmutableArray<SanitizerInfo> Sanitizers,
    ImmutableArray<SinkInfo> Sinks)
{
    public static FlowTaintedDataConfiguration Empty { get; } = new(
        ImmutableArray<SourceInfo>.Empty,
        ImmutableArray<SanitizerInfo>.Empty,
        ImmutableArray<SinkInfo>.Empty);

    public bool IsEmpty => Sources.IsDefaultOrEmpty && Sanitizers.IsDefaultOrEmpty && Sinks.IsDefaultOrEmpty;
}
