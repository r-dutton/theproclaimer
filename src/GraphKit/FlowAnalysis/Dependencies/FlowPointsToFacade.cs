using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading;
using Analyzer.Utilities;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowPointsToFacade
{
    private static readonly AnalyzerOptions EmptyAnalyzerOptions = new(ImmutableArray<AdditionalText>.Empty);

    private static readonly DiagnosticDescriptor FlowAnalysisRule = new(
        id: "GKFLOW0002",
        title: "GraphKit points-to analysis",
        messageFormat: "GraphKit points-to analysis placeholder",
        category: "GraphKit",
        defaultSeverity: DiagnosticSeverity.Hidden,
        isEnabledByDefault: true);

    private readonly ConditionalWeakTable<Compilation, WellKnownTypeProvider> _wellKnownTypeProviders = new();
    private readonly ConcurrentDictionary<FlowAnalysisCacheKey, Lazy<PointsToBundle?>> _analysisCache = new();

    public FlowPointsToFacade(
        FlowInterproceduralConfiguration configuration,
        FlowCallsitePredicate pruningPredicate)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        PruningPredicate = pruningPredicate ?? throw new ArgumentNullException(nameof(pruningPredicate));
    }

    public FlowInterproceduralConfiguration Configuration { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    public PointsToAbstractValue? TryGetPointsToValue(IOperation operation)
        => TryGetPointsToResult(operation)?[operation];

    public PointsToAnalysisResult? TryGetPointsToResult(IOperation operation)
    {
        if (operation is null)
        {
            return null;
        }

        if (operation.SemanticModel is not { } model)
        {
            return null;
        }

        var owningSymbol = model.GetEnclosingSymbol(operation.Syntax.SpanStart);
        if (owningSymbol is null)
        {
            return null;
        }

        if (!TryGetAnalysis(owningSymbol, model, operation.Syntax, out var bundle))
        {
            return null;
        }

        return bundle?.PointsTo;
    }

    public CopyAnalysisResult? TryGetCopyResult(IOperation operation)
    {
        if (operation is null)
        {
            return null;
        }

        if (operation.SemanticModel is not { } model)
        {
            return null;
        }

        var owningSymbol = model.GetEnclosingSymbol(operation.Syntax.SpanStart);
        if (owningSymbol is null)
        {
            return null;
        }

        if (!TryGetAnalysis(owningSymbol, model, operation.Syntax, out var bundle))
        {
            return null;
        }

        return bundle?.Copy;
    }

    private bool TryGetAnalysis(
        ISymbol owningSymbol,
        SemanticModel contextModel,
        SyntaxNode contextSyntax,
        out PointsToBundle? bundle)
    {
        bundle = null;

        var declaration = FindDeclarationSyntax(owningSymbol, contextSyntax);
        if (declaration is null)
        {
            return false;
        }

        var key = new FlowAnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
        var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<PointsToBundle?>(() =>
            ComputeAnalysis(owningSymbol, declaration, contextModel), LazyThreadSafetyMode.ExecutionAndPublication));

        bundle = lazy.Value;
        return bundle is not null;
    }

    private PointsToBundle? ComputeAnalysis(
        ISymbol owningSymbol,
        SyntaxNode declarationSyntax,
        SemanticModel contextModel)
    {
        try
        {
            var compilation = contextModel.Compilation;
            var semanticModel = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);
            var controlFlow = ControlFlowGraph.Create(declarationSyntax, semanticModel, CancellationToken.None);
            if (controlFlow is null)
            {
                return null;
            }

            var wellKnownProvider = GetWellKnownTypeProvider(compilation);
            var interprocedural = InterproceduralAnalysisConfiguration.Create(
                EmptyAnalyzerOptions,
                FlowAnalysisRule,
                controlFlow,
                compilation,
                Configuration.AnalysisKind,
                Configuration.MaxInterproceduralCallChainLength,
                Configuration.MaxInterproceduralLambdaOrLocalFunctionCallChainLength);

            var pointsToResult = PointsToAnalysis.TryGetOrComputeResult(
                controlFlow,
                owningSymbol,
                EmptyAnalyzerOptions,
                wellKnownProvider,
                PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
                out var copyAnalysisResult,
                interprocedural,
                interproceduralAnalysisPredicate: null,
                pessimisticAnalysis: false,
                performCopyAnalysis: true);

            return new PointsToBundle(pointsToResult, copyAnalysisResult);
        }
        catch (Exception ex) when (IsBenignAnalysisException(ex))
        {
            return null;
        }
    }

    private WellKnownTypeProvider GetWellKnownTypeProvider(Compilation compilation)
        => _wellKnownTypeProviders.GetValue(compilation, static c => WellKnownTypeProvider.GetOrCreate(c));

    private static SyntaxNode? FindDeclarationSyntax(ISymbol symbol, SyntaxNode contextSyntax)
    {
        foreach (var reference in symbol.DeclaringSyntaxReferences)
        {
            var syntax = reference.GetSyntax();
            if (syntax.SyntaxTree == contextSyntax.SyntaxTree)
            {
                return syntax;
            }
        }

        return symbol.DeclaringSyntaxReferences.Length > 0
            ? symbol.DeclaringSyntaxReferences[0].GetSyntax()
            : null;
    }

    private static bool IsBenignAnalysisException(Exception exception)
        => exception is InvalidOperationException or NotSupportedException or OperationCanceledException;

    private sealed record PointsToBundle(
        PointsToAnalysisResult? PointsTo,
        CopyAnalysisResult? Copy);
}
