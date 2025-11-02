using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace GraphKit.FlowAnalysis.Core
{
    public sealed class FlowAnalysisData
    {
        public ImmutableDictionary<ISymbol, FlowAbstractValue> Values { get; }
        public FlowAnalysisData(ImmutableDictionary<ISymbol, FlowAbstractValue> values)
            => Values = values;
        public static FlowAnalysisData Empty { get; } =
            new(ImmutableDictionary<ISymbol, FlowAbstractValue>.Empty);
    }
}
