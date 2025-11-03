using Microsoft.CodeAnalysis;
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

        public virtual void Visit(IOperation op)
        {
            switch (op)
            {
                case IInvocationOperation inv: VisitInvocation(inv); break;
                case IAssignmentOperation asg: VisitAssignment(asg); break;
                default:
                    foreach (var child in op.ChildOperations) Visit(child);
                    break;
            }
        }

        protected virtual void VisitAssignment(IAssignmentOperation op)
        {
            foreach (var child in op.ChildOperations) Visit(child);
        }

        protected virtual void VisitInvocation(IInvocationOperation op)
        {
            foreach (var arg in op.Arguments) Visit(arg.Value);
        }
    }
}
