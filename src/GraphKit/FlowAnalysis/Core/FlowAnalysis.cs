using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using GraphKit.FlowAnalysis.Interprocedural;

namespace GraphKit.FlowAnalysis.Core
{
    public static class FlowAnalysis
    {
        public static void AnalyzeMethod(
            Compilation compilation,
            SemanticModel model,
            IMethodSymbol method,
            InterproceduralAnalysisConfiguration configuration,
            FlowCallsitePredicate shouldExpand,
            FlowDataFlowOperationVisitor rootVisitor)
        {
            _ = configuration;

            var decl = method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
            if (decl is null) return;
            var op = model.GetOperation(decl) ?? model.GetOperation(decl.Parent);
            if (op is null) return;

            // Entry traversal (manual interprocedural handled by callers using shouldExpand)
            rootVisitor.Visit(op);
        }
    }
}
