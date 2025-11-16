using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using GraphKit.FlowAnalysis.Dependencies;

namespace GraphKit.FlowAnalysis.Core
{
    public abstract class FlowDataFlowOperationVisitor
    {
        protected readonly Compilation Compilation;
        protected readonly SemanticModel Model;
        protected readonly FlowPointsToFacade PointsTo;
        protected readonly FlowValueContentFacade ValueContent;

        protected FlowDataFlowOperationVisitor(
            Compilation compilation, SemanticModel model,
            FlowPointsToFacade pointsTo, FlowValueContentFacade valueContent)
        {
            Compilation = compilation;
            Model = model;
            PointsTo = pointsTo;
            ValueContent = valueContent;
        }

        protected BasicBlock? CurrentBlock { get; private set; }

        public virtual void Visit(ControlFlowGraph? graph)
        {
            if (graph is null)
            {
                return;
            }

            var visited = new HashSet<int>();
            var worklist = new Queue<BasicBlock>();

            void Enqueue(BasicBlock? block)
            {
                if (block is null || !block.IsReachable)
                {
                    return;
                }

                if (visited.Add(block.Ordinal))
                {
                    worklist.Enqueue(block);
                }
            }

            Enqueue(graph.EntryPoint);

            foreach (var block in graph.Blocks)
            {
                Enqueue(block);
            }

            while (worklist.Count > 0)
            {
                var block = worklist.Dequeue();
                Visit(block);

                foreach (var branch in EnumerateSuccessors(block))
                {
                    foreach (var region in branch.LeavingRegions)
                    {
                        OnLeaveRegion(region);
                    }

                    foreach (var finallyRegion in branch.FinallyRegions)
                    {
                        OnEnterRegion(finallyRegion);
                    }

                    foreach (var region in branch.EnteringRegions)
                    {
                        OnEnterRegion(region);
                    }

                    OnBranch(branch, block.BranchValue);

                    Enqueue(branch.Destination);
                }
            }
        }

        private static IEnumerable<ControlFlowBranch> EnumerateSuccessors(BasicBlock block)
        {
            if (block.ConditionalSuccessor is { } conditional)
            {
                yield return conditional;
            }

            if (block.FallThroughSuccessor is { } fallthrough)
            {
                yield return fallthrough;
            }
        }

        public virtual void Visit(BasicBlock block)
        {
            var previous = CurrentBlock;
            CurrentBlock = block;

            try
            {
                foreach (var operation in block.Operations)
                {
                    Visit(operation);
                }

                if (block.BranchValue is { } branchOperation)
                {
                    Visit(branchOperation);
                }
            }
            finally
            {
                CurrentBlock = previous;
            }
        }

        public virtual void Visit(IOperation op)
        {
            switch (op)
            {
                case IInvocationOperation invocation:
                    VisitInvocation(invocation);
                    break;
                case ISimpleAssignmentOperation simpleAssignment:
                    OnAssignment(simpleAssignment);
                    break;
                case IAssignmentOperation assignment:
                    VisitAssignment(assignment);
                    break;
                case IReturnOperation returnOperation:
                    OnReturn(returnOperation);
                    break;
                case IConditionalOperation conditional:
                    OnConditional(conditional);
                    break;
                default:
                    foreach (var child in op.ChildOperations)
                    {
                        Visit(child);
                    }

                    return;
            }

            foreach (var child in op.ChildOperations)
            {
                Visit(child);
            }
        }

        protected virtual void VisitAssignment(IAssignmentOperation op)
        {
            foreach (var child in op.ChildOperations)
            {
                Visit(child);
            }
        }

        protected virtual void VisitInvocation(IInvocationOperation op)
        {
            foreach (var argument in op.Arguments)
            {
                Visit(argument.Value);
            }
        }

        // Optional hooks for derived visitors
        protected virtual void OnReturn(IReturnOperation op) { }
        protected virtual void OnAssignment(ISimpleAssignmentOperation op) { }
        protected virtual void OnConditional(IConditionalOperation op) { }
        protected virtual void OnBranch(ControlFlowBranch branch, IOperation? condition) { }
        protected virtual void OnEnterRegion(ControlFlowRegion region) { }
        protected virtual void OnLeaveRegion(ControlFlowRegion region) { }
    }
}
