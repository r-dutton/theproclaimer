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
        private readonly Stack<NestedFlowScope> _nestedFlows = new();

        protected FlowDataFlowOperationVisitor(
            Compilation compilation, SemanticModel model,
            FlowPointsToFacade pointsTo, FlowValueContentFacade valueContent)
        {
            Compilation = compilation;
            Model = model;
            PointsTo = pointsTo;
            ValueContent = valueContent;
        }

        public virtual void Visit(ControlFlowGraph graph)
        {
            foreach (var block in graph.Blocks)
            {
                Visit(block);
            }
        }

        public virtual void Visit(BasicBlock block)
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

        public NestedFlowScope? CurrentNestedFlow => _nestedFlows.Count == 0
            ? null
            : _nestedFlows.Peek();

        public virtual void Visit(IOperation op)
        {
            switch (op)
            {
                case IAnonymousFunctionOperation anonymousFunction:
                    VisitAnonymousFunction(anonymousFunction);
                    return;
                case ILocalFunctionOperation localFunction:
                    VisitLocalFunction(localFunction);
                    return;
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

        protected virtual void VisitAnonymousFunction(IAnonymousFunctionOperation op)
        {
            VisitNestedFlow(op, ControlFlowGraph.GetAnonymousFunctionControlFlowGraph(op));
        }

        protected virtual void VisitLocalFunction(ILocalFunctionOperation op)
        {
            VisitNestedFlow(op, ControlFlowGraph.GetLocalFunctionControlFlowGraph(op));
        }

        // Optional hooks for derived visitors
        protected virtual void OnReturn(IReturnOperation op) { }
        protected virtual void OnAssignment(ISimpleAssignmentOperation op) { }
        protected virtual void OnConditional(IConditionalOperation op) { }
        protected virtual void OnNestedFlowEntered(in NestedFlowScope scope) { }
        protected virtual void OnNestedFlowExited(in NestedFlowScope scope) { }

        protected virtual IInvocationOperation? FindEnclosingCallsite(IOperation operation)
        {
            if (operation is null)
            {
                return null;
            }

            var parent = operation.Parent;
            while (parent is not null)
            {
                if (parent is IInvocationOperation invocation)
                {
                    return invocation;
                }

                parent = parent.Parent;
            }

            return null;
        }

        private void VisitNestedFlow(IOperation owner, ControlFlowGraph? nestedGraph)
        {
            if (nestedGraph is null)
            {
                foreach (var child in owner.ChildOperations)
                {
                    Visit(child);
                }

                return;
            }

            var scope = new NestedFlowScope(owner, FindEnclosingCallsite(owner));
            _nestedFlows.Push(scope);
            OnNestedFlowEntered(scope);

            try
            {
                Visit(nestedGraph);
            }
            finally
            {
                OnNestedFlowExited(scope);
                _nestedFlows.Pop();
            }
        }

        protected readonly record struct NestedFlowScope(
            IOperation Operation,
            IInvocationOperation? Callsite);
    }
}
