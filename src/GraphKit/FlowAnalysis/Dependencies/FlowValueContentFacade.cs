using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies
{
    public sealed class FlowValueContentFacade
    {
        public FlowValueContentFacade(
            InterproceduralAnalysisConfiguration configuration,
            FlowCallsitePredicate pruningPredicate)
        {
            Configuration = configuration;
            PruningPredicate = pruningPredicate;
        }

        public InterproceduralAnalysisConfiguration Configuration { get; }

        public FlowCallsitePredicate PruningPredicate { get; }

        // Best-effort literal/concat/interpolation reconstruction.
        public string? TryGetStringValue(IOperation op) => null;
    }
}
