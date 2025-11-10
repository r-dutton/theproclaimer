using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using GraphKit.Analyzers;
using GraphKit.FlowAnalysis.Interprocedural;
using Xunit;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;

namespace GraphKit.Tests.Flow;

public sealed class MethodFlowContextTests
{
    private static readonly MetadataReference CorlibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

    [Fact]
    public void GetOrCreateMethodContext_IsCachedPerCompilation()
    {
        var (compilation, methodSymbol) = CreateCompilationAndMethod();
        var context1 = FlowAnalysisCore.GetOrCreateMethodContext(compilation, methodSymbol);
        var context2 = FlowAnalysisCore.GetOrCreateMethodContext(compilation, methodSymbol);

        Assert.Same(context1, context2);
    }

    [Fact]
    public void GetOrCreateMethodAnalysis_ReusesContext()
    {
        var (compilation, methodSymbol) = CreateCompilationAndMethod();
        var settings = ProjectAnalyzer.ProjectAnalyzerConfiguration.Default.ToInterproceduralSettings();

        var analysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, settings);

        Assert.NotNull(analysis.Context);
        Assert.Same(analysis.Context, FlowAnalysisCore.GetOrCreateMethodContext(compilation, methodSymbol));
    }

    private static (Compilation Compilation, IMethodSymbol Method) CreateCompilationAndMethod()
    {
        const string source = """
            namespace Sample
            {
                public sealed class Calculator
                {
                    public int Add(int left, int right)
                    {
                        return left + right;
                    }
                }
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create(
            assemblyName: "Sample",
            syntaxTrees: new[] { syntaxTree },
            references: new[] { CorlibReference },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var semanticModel = compilation.GetSemanticModel(syntaxTree);
        var methodNode = syntaxTree.GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .First();

        var methodSymbol = (IMethodSymbol)semanticModel.GetDeclaredSymbol(methodNode)!;
        return (compilation, methodSymbol);
    }
}
