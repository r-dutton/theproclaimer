using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GraphKit.FlowAnalysis.Core;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.Operations;
using Xunit;

namespace GraphKit.Tests.FlowAnalysis;

public sealed class FlowDataFlowOperationVisitorTests
{
    [Fact]
    public void Visit_TraversesNestedLambdasAndLocalFunctions()
    {
        const string source = @"using System.Collections.Generic;
using System.Linq;

namespace Sample;

public static class LinqSample
{
    public static int Execute(int[] values)
    {
        const int threshold = 5;
        var results = values
            .Where(v => v > threshold)
            .Select((value, index) => Transform(value, index))
            .ToList();

        int Transform(int value, int index)
        {
            return values.Count(inner => inner == value) + index;
        }

        return results.Count;
    }
}";

        var tree = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create(
            assemblyName: "Sample",
            syntaxTrees: new[] { tree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location)
            },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var model = compilation.GetSemanticModel(tree);
        var methodSyntax = tree.GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Single(m => m.Identifier.Text == "Execute");

        var cfg = ControlFlowGraph.Create(methodSyntax, model, CancellationToken.None);
        Assert.NotNull(cfg);

        var settings = new InterproceduralSettings(
            InterproceduralAnalysisKind.ContextSensitive,
            maxCallChainLength: 5,
            maxLambdaOrLocalFunctionDepth: 5);
        FlowCallsitePredicate predicate = _ => true;
        var pointsTo = new FlowPointsToFacade(settings, predicate, FlowPointsToAnalysisOptions.Fast);
        var valueContent = new FlowValueContentFacade(settings, predicate, FlowPointsToAnalysisOptions.Fast);

        var visitor = new RecordingVisitor(compilation, model, pointsTo, valueContent);
        visitor.Visit(cfg!);

        Assert.Collection(visitor.EnteredFlows,
            flow =>
            {
                Assert.Equal(OperationKind.AnonymousFunction, flow.Operation.Kind);
                Assert.Equal("Where", flow.Callsite!.TargetMethod.Name);
            },
            flow =>
            {
                Assert.Equal(OperationKind.AnonymousFunction, flow.Operation.Kind);
                Assert.Equal("Select", flow.Callsite!.TargetMethod.Name);
            },
            flow =>
            {
                Assert.Equal(OperationKind.LocalFunction, flow.Operation.Kind);
                Assert.Null(flow.Callsite);
            },
            flow =>
            {
                Assert.Equal(OperationKind.AnonymousFunction, flow.Operation.Kind);
                Assert.Equal("Count", flow.Callsite!.TargetMethod.Name);
            });

        Assert.Equal(visitor.EnteredFlows.Count, visitor.ExitedFlows.Count);
        Assert.Contains("Transform", visitor.Invocations);
        Assert.Contains("Count", visitor.Invocations);
    }

    private sealed class RecordingVisitor : FlowDataFlowOperationVisitor
    {
        public RecordingVisitor(
            Compilation compilation,
            SemanticModel model,
            FlowPointsToFacade pointsTo,
            FlowValueContentFacade valueContent)
            : base(compilation, model, pointsTo, valueContent)
        {
        }

        public List<(IOperation Operation, IInvocationOperation? Callsite)> EnteredFlows { get; } = new();

        public List<(IOperation Operation, IInvocationOperation? Callsite)> ExitedFlows { get; } = new();

        public List<string> Invocations { get; } = new();

        protected override void OnNestedFlowEntered(in NestedFlowScope scope)
        {
            EnteredFlows.Add((scope.Operation, scope.Callsite));
        }

        protected override void OnNestedFlowExited(in NestedFlowScope scope)
        {
            ExitedFlows.Add((scope.Operation, scope.Callsite));
        }

        protected override void VisitInvocation(IInvocationOperation op)
        {
            Invocations.Add(op.TargetMethod.Name);
            base.VisitInvocation(op);
        }
    }
}
