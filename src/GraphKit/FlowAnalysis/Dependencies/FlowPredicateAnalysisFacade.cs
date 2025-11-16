using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowPredicateAnalysisFacade
{
    private readonly FlowPointsToFacade _pointsTo;

    public FlowPredicateAnalysisFacade(FlowPointsToFacade pointsTo)
    {
        _pointsTo = pointsTo;
    }

    public PredicateValueKind GetPredicateKind(IOperation operation)
    {
        if (operation is null)
        {
            return PredicateValueKind.Unknown;
        }

        var analysis = _pointsTo.TryGetAnalysisResult(operation);
        return analysis?.GetPredicateKind(operation) ?? PredicateValueKind.Unknown;
    }

    public bool IsAlwaysTrue(IOperation operation)
        => GetPredicateKind(operation) == PredicateValueKind.AlwaysTrue;

    public bool IsAlwaysFalse(IOperation operation)
        => GetPredicateKind(operation) == PredicateValueKind.AlwaysFalse;
}
