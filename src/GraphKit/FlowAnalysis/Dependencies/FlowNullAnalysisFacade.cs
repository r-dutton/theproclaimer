using System;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowNullAnalysisFacade
{
    private readonly FlowPointsToFacade _pointsTo;

    public FlowNullAnalysisFacade(FlowPointsToFacade pointsTo)
    {
        _pointsTo = pointsTo ?? throw new ArgumentNullException(nameof(pointsTo));
    }

    public bool TryGetNullState(IOperation operation, out NullAbstractValue nullState)
    {
        nullState = NullAbstractValue.Invalid;
        if (operation is null)
        {
            return false;
        }

        return _pointsTo.TryGetAbstractValue(operation, out var abstractValue) &&
               (nullState = abstractValue.NullState) != NullAbstractValue.Invalid;
    }

    public bool IsDefinitelyNull(IOperation operation)
        => TryGetNullState(operation, out var state) && state == NullAbstractValue.Null;

    public bool IsDefinitelyNonNull(IOperation operation)
        => TryGetNullState(operation, out var state) && state == NullAbstractValue.NotNull;

    public bool IsMaybeNull(IOperation operation)
        => TryGetNullState(operation, out var state) && state == NullAbstractValue.MaybeNull;
}
