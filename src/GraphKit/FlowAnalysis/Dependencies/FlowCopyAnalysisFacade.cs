using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Analyzer.Utilities;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using CopyAnalysisResult = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.DataFlowAnalysisResult<Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis.CopyBlockAnalysisResult, Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis.CopyAbstractValue>;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowCopyAnalysisFacade
{
    private static readonly AnalyzerOptions EmptyAnalyzerOptions = new(ImmutableArray<AdditionalText>.Empty);

    private static readonly DiagnosticDescriptor FlowAnalysisRule = new(
        id: "GKFLOW0003",
        title: "GraphKit copy analysis",
        messageFormat: "GraphKit copy analysis placeholder",
        category: "GraphKit",
        defaultSeverity: DiagnosticSeverity.Hidden,
        isEnabledByDefault: true);

    private readonly ConcurrentDictionary<AnalysisCacheKey, Lazy<CopyAnalysisResult?>> _analysisCache = new();

    public FlowCopyAnalysisFacade(InterproceduralSettings configuration, FlowCallsitePredicate pruningPredicate)
    {
        Settings = configuration;
        PruningPredicate = pruningPredicate;
        AnalysisPredicate = FlowPointsToFacade.CreateInterproceduralPredicate(pruningPredicate);
    }

    public InterproceduralSettings Settings { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    private InterproceduralAnalysisPredicate AnalysisPredicate { get; }

    public bool TryGetAbstractValue(IOperation operation, out CopyAbstractValue value)
    {
        value = CopyAbstractValue.Invalid;
        if (operation is null)
        {
            return false;
        }

        if (operation.SemanticModel is not { } model)
        {
            return false;
        }

        var owningSymbol = model.GetEnclosingSymbol(operation.Syntax.SpanStart);
        if (owningSymbol is null)
        {
            return false;
        }

        if (!TryGetAnalysis(owningSymbol, model, operation.Syntax, out var analysis) || analysis is null)
        {
            return false;
        }

        value = analysis[operation];
        return value != CopyAbstractValue.Invalid;
    }

    public ImmutableArray<IFieldSymbol> GetReferencedFields(IOperation operation)
    {
        if (!TryGetAbstractValue(operation, out var value) ||
            value is null ||
            value.Kind == CopyAbstractValueKind.Invalid ||
            value.Kind == CopyAbstractValueKind.NotApplicable)
        {
            return ImmutableArray<IFieldSymbol>.Empty;
        }

        var builder = ImmutableArray.CreateBuilder<IFieldSymbol>();
        foreach (var entity in value.AnalysisEntities)
        {
            if (entity.Symbol is IFieldSymbol field)
            {
                builder.Add(field);
            }
        }

        return builder.ToImmutable();
    }

    private bool TryGetAnalysis(
        ISymbol owningSymbol,
        SemanticModel contextModel,
        SyntaxNode contextSyntax,
        out CopyAnalysisResult? analysis)
    {
        analysis = null;
        var declaration = FlowPointsToFacade.FindDeclarationSyntax(owningSymbol, contextSyntax);
        if (declaration is null)
        {
            return false;
        }

        var key = new AnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
        var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<CopyAnalysisResult?>(() =>
            ComputeAnalysis(owningSymbol, declaration, contextModel), LazyThreadSafetyMode.ExecutionAndPublication));

        analysis = lazy.Value;
        return analysis is not null;
    }

    private CopyAnalysisResult? ComputeAnalysis(
        ISymbol owningSymbol,
        SyntaxNode declarationSyntax,
        SemanticModel contextModel)
    {
        try
        {
            var compilation = contextModel.Compilation;

            if (owningSymbol is IMethodSymbol methodSymbol)
            {
                var methodAnalysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(
                    compilation,
                    methodSymbol,
                    Settings,
                    CancellationToken.None);

                if (!methodAnalysis.CopyAnalysisComputed)
                {
                    var methodContext = methodAnalysis.Context;
                    var declaration = methodContext.Declaration ?? declarationSyntax;
                    var methodSemanticModel = methodContext.SemanticModel ?? compilation.GetSemanticModel(declaration.SyntaxTree);
                    var controlFlow = methodContext.ControlFlowGraph ?? ControlFlowGraph.Create(declaration, methodSemanticModel, CancellationToken.None);

                    CopyAnalysisResult? computed = null;
                    if (controlFlow is not null)
                    {
                        computed = RunCopyAnalysis(controlFlow, owningSymbol, compilation);
                    }

                    methodAnalysis.CopyAnalysis = computed;
                    methodAnalysis.CopyAnalysisComputed = true;
                }

                return methodAnalysis.CopyAnalysis;
            }

            var semanticModel = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);
            var cfg = ControlFlowGraph.Create(declarationSyntax, semanticModel, CancellationToken.None);
            return cfg is null ? null : RunCopyAnalysis(cfg, owningSymbol, compilation);
        }
        catch (Exception ex) when (FlowPointsToFacade.IsBenignAnalysisException(ex))
        {
            return null;
        }
    }

    private CopyAnalysisResult? RunCopyAnalysis(
        ControlFlowGraph controlFlowGraph,
        ISymbol owningSymbol,
        Compilation compilation)
    {
        var wellKnownProvider = WellKnownTypeProvider.GetOrCreate(compilation);
        var settings = Settings;

        var interproceduralConfiguration = InterproceduralAnalysisConfiguration.Create(
            EmptyAnalyzerOptions,
            ImmutableArray.Create(FlowAnalysisRule),
            controlFlowGraph,
            compilation,
            settings.Kind,
            (uint)Math.Max(0, settings.MaxCallChainLength),
            (uint)Math.Max(0, settings.MaxLambdaOrLocalFunctionDepth));

        return CopyAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            EmptyAnalyzerOptions,
            wellKnownProvider,
            interproceduralConfiguration,
            AnalysisPredicate,
            pessimisticAnalysis: false,
            pointsToAnalysisKind: PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
            exceptionPathsAnalysis: false);
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
