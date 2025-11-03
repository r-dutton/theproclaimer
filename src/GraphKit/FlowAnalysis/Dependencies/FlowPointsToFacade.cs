using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace GraphKit.FlowAnalysis.Dependencies
{
    // Placeholder to integrate Roslyn-analyzers PointsToAnalysis in the future.
    public sealed class FlowPointsToFacade
    {
        public FlowPointsToFacade(
            InterproceduralAnalysisConfiguration configuration,
            FlowCallsitePredicate pruningPredicate)
        {
            Configuration = configuration;
            PruningPredicate = pruningPredicate;
        }

        public InterproceduralAnalysisConfiguration Configuration { get; }

        public FlowCallsitePredicate PruningPredicate { get; }
    }
}
