using System.Linq;
using GraphKit.Analyzers;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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

    [Fact]
    public void GetOrCreateMethodAnalysis_IsCachedPerSettings()
    {
        var (compilation, methodSymbol) = CreateCompilationAndMethod();
        var shallow = new InterproceduralSettings(InterproceduralAnalysisKind.ContextInsensitive, 0, 0);

        var analysis1 = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, shallow);
        var analysis2 = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, shallow);

        Assert.Same(analysis1, analysis2);
        Assert.Equal(shallow, analysis1.Settings);
    }

    [Fact]
    public void GetOrCreateMethodAnalysis_RecomputesForDifferentSettings()
    {
        var (compilation, methodSymbol) = CreateCompilationAndMethod();
        var shallow = new InterproceduralSettings(InterproceduralAnalysisKind.ContextInsensitive, 0, 0);
        var deep = new InterproceduralSettings(InterproceduralAnalysisKind.ContextSensitive, 8, 4);

        var shallowAnalysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, shallow);
        var deepAnalysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, deep);

        Assert.NotSame(shallowAnalysis, deepAnalysis);
        Assert.Same(shallowAnalysis.Context, deepAnalysis.Context);
        Assert.Equal(shallow, shallowAnalysis.Settings);
        Assert.Equal(deep, deepAnalysis.Settings);
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
