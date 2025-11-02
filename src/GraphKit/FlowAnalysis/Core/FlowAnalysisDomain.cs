using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace GraphKit.FlowAnalysis.Core
{
    public sealed class FlowAnalysisDomain
    {
        private readonly FlowAbstractValueDomain _valueDomain = new();
        public FlowAnalysisData Merge(FlowAnalysisData a, FlowAnalysisData b)
        {
            var map = a.Values.ToBuilder();
            foreach (var kv in b.Values)
            {
                var merged = map.TryGetValue(kv.Key, out var prior)
                    ? _valueDomain.Merge(prior, kv.Value)
                    : kv.Value;
                map[kv.Key] = merged;
            }
            return new FlowAnalysisData(map.ToImmutable());
        }
    }
}
