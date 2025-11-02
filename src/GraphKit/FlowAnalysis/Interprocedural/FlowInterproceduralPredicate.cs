using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Interprocedural
{
    public delegate bool FlowCallsitePredicate(IInvocationOperation invocation);
}
