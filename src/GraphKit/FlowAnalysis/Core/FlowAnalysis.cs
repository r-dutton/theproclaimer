using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using GraphKit.FlowAnalysis.Interprocedural;

namespace GraphKit.FlowAnalysis.Core
{
    public static class FlowAnalysis
    {
        public static void AnalyzeMethod(
            Compilation compilation,
            SemanticModel model,
            IMethodSymbol method,
            FlowInterproceduralConfig ipc,
            FlowCallsitePredicate shouldExpand,
            FlowDataFlowOperationVisitor rootVisitor)
        {
            var decl = method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
            if (decl is null) return;

            var op = decl switch
            {
                BaseMethodDeclarationSyntax methodDecl => GetBodyOperation(model, methodDecl),
                AccessorDeclarationSyntax accessorDecl => GetAccessorOperation(model, accessorDecl),
                LocalFunctionStatementSyntax localFunction => GetLocalFunctionOperation(model, localFunction),
                _ => model.GetOperation(decl)
            };
            if (op is null) return;

            // Entry traversal (manual interprocedural handled by callers using shouldExpand)
            rootVisitor.Visit(op);

            static IOperation? GetBodyOperation(SemanticModel model, BaseMethodDeclarationSyntax methodDecl)
            {
                if (methodDecl.Body is { } block)
                {
                    return model.GetOperation(block);
                }

                if (methodDecl.ExpressionBody?.Expression is { } expression)
                {
                    return model.GetOperation(expression);
                }

                return model.GetOperation(methodDecl);
            }

            static IOperation? GetAccessorOperation(SemanticModel model, AccessorDeclarationSyntax accessorDecl)
            {
                if (accessorDecl.Body is { } block)
                {
                    return model.GetOperation(block);
                }

                if (accessorDecl.ExpressionBody?.Expression is { } expression)
                {
                    return model.GetOperation(expression);
                }

                return model.GetOperation(accessorDecl);
            }

            static IOperation? GetLocalFunctionOperation(SemanticModel model, LocalFunctionStatementSyntax localFunction)
            {
                if (localFunction.Body is { } block)
                {
                    return model.GetOperation(block);
                }

                if (localFunction.ExpressionBody?.Expression is { } expression)
                {
                    return model.GetOperation(expression);
                }

                return model.GetOperation(localFunction);
            }
        }
    }
}
