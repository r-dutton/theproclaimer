using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading;
using Analyzer.Utilities;
using Analyzer.Utilities.FlowAnalysis.Analysis.TaintedDataAnalysis;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowTaintedDataFacade
{
    private static readonly AnalyzerOptions EmptyAnalyzerOptions = new(ImmutableArray<AdditionalText>.Empty);

    private static readonly DiagnosticDescriptor FlowAnalysisRule = new(
        id: "GKFLOW0004",
        title: "GraphKit tainted data analysis",
        messageFormat: "GraphKit tainted data analysis placeholder",
        category: "GraphKit",
        defaultSeverity: DiagnosticSeverity.Hidden,
        isEnabledByDefault: true);

    private readonly ConcurrentDictionary<AnalysisCacheKey, Lazy<TaintedDataAnalysisResult?>> _analysisCache = new();

    public FlowTaintedDataFacade(
        InterproceduralSettings configuration,
        FlowCallsitePredicate pruningPredicate,
        FlowTaintedDataConfiguration? taintedConfiguration = null)
    {
        Settings = configuration;
        PruningPredicate = pruningPredicate;
        Configuration = taintedConfiguration ?? FlowTaintedDataConfiguration.Empty;
    }

    public InterproceduralSettings Settings { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    public FlowTaintedDataConfiguration Configuration { get; }

    public bool IsEnabled => !Configuration.IsEmpty;

    public bool IsInvocationTainted(IInvocationOperation invocation)
    {
        if (!IsEnabled || invocation is null)
        {
            return false;
        }

        if (invocation.SemanticModel is not { } model)
        {
            return false;
        }

        if (model.GetEnclosingSymbol(invocation.Syntax.SpanStart) is not IMethodSymbol owningMethod)
        {
            return false;
        }

        var analysis = GetOrComputeAnalysis(owningMethod, model, invocation.Syntax);
        if (analysis is null)
        {
            return false;
        }

        var location = invocation.Syntax.GetLocation();
        foreach (var entry in analysis.TaintedDataSourceSinks)
        {
            if (entry.Sink.Location == location)
            {
                return true;
            }
        }

        return false;
    }

    private TaintedDataAnalysisResult? GetOrComputeAnalysis(
        IMethodSymbol owningMethod,
        SemanticModel model,
        SyntaxNode contextSyntax)
    {
        var declaration = FlowPointsToFacade.FindDeclarationSyntax(owningMethod, contextSyntax);
        if (declaration is null)
        {
            return null;
        }

        var key = new AnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
        var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<TaintedDataAnalysisResult?>(() =>
            ComputeAnalysis(owningMethod, declaration, model), LazyThreadSafetyMode.ExecutionAndPublication));
        return lazy.Value;
    }

    private TaintedDataAnalysisResult? ComputeAnalysis(
        IMethodSymbol owningMethod,
        SyntaxNode declarationSyntax,
        SemanticModel contextModel)
    {
        try
        {
            var compilation = contextModel.Compilation;
            var methodAnalysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(
                compilation,
                owningMethod,
                Settings,
                CancellationToken.None);

            if (!methodAnalysis.TaintedDataComputed)
            {
                var methodContext = methodAnalysis.Context;
                var declaration = methodContext.Declaration ?? declarationSyntax;
                var methodSemanticModel = methodContext.SemanticModel ?? compilation.GetSemanticModel(declaration.SyntaxTree);
                var controlFlow = methodContext.ControlFlowGraph ?? ControlFlowGraph.Create(declaration, methodSemanticModel, CancellationToken.None);

                TaintedDataAnalysisResult? computed = null;
                if (controlFlow is not null)
                {
                    computed = RunTaintedDataAnalysis(controlFlow, compilation, owningMethod);
                }

                methodAnalysis.TaintedDataAnalysis = computed;
                methodAnalysis.TaintedDataComputed = true;
            }

            return methodAnalysis.TaintedDataAnalysis;
        }
        catch (Exception ex) when (FlowPointsToFacade.IsBenignAnalysisException(ex))
        {
            return null;
        }
    }

    private TaintedDataAnalysisResult? RunTaintedDataAnalysis(
        ControlFlowGraph controlFlowGraph,
        Compilation compilation,
        ISymbol containingMethod)
    {
        if (!IsEnabled)
        {
            return null;
        }

        var wellKnownProvider = WellKnownTypeProvider.GetOrCreate(compilation);
        var sourceMap = new TaintedDataSymbolMap<SourceInfo>(wellKnownProvider, Configuration.Sources);
        var sanitizerMap = new TaintedDataSymbolMap<SanitizerInfo>(wellKnownProvider, Configuration.Sanitizers);
        var sinkMap = new TaintedDataSymbolMap<SinkInfo>(wellKnownProvider, Configuration.Sinks);
        if (sourceMap.IsEmpty || sinkMap.IsEmpty)
        {
            return null;
        }

        return TaintedDataAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            compilation,
            containingMethod,
            EmptyAnalyzerOptions,
            FlowAnalysisRule,
            sourceMap,
            sanitizerMap,
            sinkMap);
    }

    private readonly struct AnalysisCacheKey : IEquatable<AnalysisCacheKey>
    {
        public AnalysisCacheKey(SyntaxTree tree, TextSpan span)
        {
            Tree = tree;
            Span = span;
        }

        public SyntaxTree Tree { get; }

        public TextSpan Span { get; }

        public bool Equals(AnalysisCacheKey other)
            => ReferenceEquals(Tree, other.Tree) && Span.Equals(other.Span);

        public override bool Equals(object? obj)
            => obj is AnalysisCacheKey other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(Tree, Span.Start, Span.Length);
    }
}
