using System.Collections.Generic;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Core;

internal sealed class ControlFlowTraversalState
{
    private readonly HashSet<int> _guardedBlockOrdinals = new();
    private readonly HashSet<ControlFlowRegion> _exceptionRegions = new();

    public void OnBranch(ControlFlowBranch branch, IOperation? condition)
    {
        if (branch.Destination is null)
        {
            return;
        }

        switch (branch.Semantics)
        {
            case ControlFlowBranchSemantics.Return:
            case ControlFlowBranchSemantics.Throw:
            case ControlFlowBranchSemantics.Rethrow:
            case ControlFlowBranchSemantics.ProgramTermination:
            case ControlFlowBranchSemantics.StructuredExceptionHandling:
                _guardedBlockOrdinals.Add(branch.Destination.Ordinal);
                break;
        }
    }

    public void OnEnterRegion(ControlFlowRegion region)
    {
        if (IsExceptionRegion(region))
        {
            _exceptionRegions.Add(region);
        }
    }

    public void OnLeaveRegion(ControlFlowRegion region)
    {
        // Regions are intentionally kept once observed to reflect their semantics
        // even if control leaves them via other branches.
    }

    public bool ShouldSkip(BasicBlock? block)
    {
        return IsGuarded(block) || IsException(block);
    }

    public bool IsGuarded(BasicBlock? block)
    {
        return block is not null && _guardedBlockOrdinals.Contains(block.Ordinal);
    }

    public bool IsException(BasicBlock? block)
    {
        var region = block?.EnclosingRegion;
        while (region is not null)
        {
            if (_exceptionRegions.Contains(region) || IsExceptionRegion(region))
            {
                return true;
            }

            region = region.EnclosingRegion;
        }

        return false;
    }

    private static bool IsExceptionRegion(ControlFlowRegion region)
    {
        return region.Kind is ControlFlowRegionKind.Catch
            or ControlFlowRegionKind.Filter
            or ControlFlowRegionKind.FilterAndHandler
            or ControlFlowRegionKind.Finally;
    }
}
