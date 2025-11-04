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
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.FlowAnalysis.Dependencies;

public sealed class FlowValueContentFacade
{
    private static readonly AnalyzerOptions EmptyAnalyzerOptions = new(ImmutableArray<AdditionalText>.Empty);

    private static readonly DiagnosticDescriptor FlowAnalysisRule = new(
        id: "GKFLOW0001",
        title: "GraphKit flow analysis",
        messageFormat: "GraphKit flow analysis placeholder",
        category: "GraphKit",
        defaultSeverity: DiagnosticSeverity.Hidden,
        isEnabledByDefault: true);

    private readonly ConditionalWeakTable<Compilation, WellKnownTypeProvider> _wellKnownTypeProviders = new();
    private readonly ConcurrentDictionary<FlowAnalysisCacheKey, Lazy<AnalysisBundle?>> _analysisCache = new();

    public FlowValueContentFacade(
        FlowInterproceduralConfiguration configuration,
        FlowCallsitePredicate pruningPredicate)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        PruningPredicate = pruningPredicate ?? throw new ArgumentNullException(nameof(pruningPredicate));
    }

    public FlowInterproceduralConfiguration Configuration { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    public string? TryGetStringValue(IOperation op)
    {
        if (op is null)
        {
            return null;
        }

        if (op.ConstantValue is { HasValue: true, Value: string constant })
        {
            return constant;
        }

        if (op.SemanticModel is not { } model)
        {
            return null;
        }

        var owningSymbol = model.GetEnclosingSymbol(op.Syntax.SpanStart);
        if (owningSymbol is null)
        {
            return null;
        }

        if (!TryGetAnalysis(owningSymbol, model, op.Syntax, out var bundle) ||
            bundle?.ValueContent is not { } analysis)
        {
            return null;
        }

        return TryExtractString(analysis, op, out var reconstructed) ? reconstructed : null;
    }

    private bool TryGetAnalysis(
        ISymbol owningSymbol,
        SemanticModel contextModel,
        SyntaxNode contextSyntax,
        out AnalysisBundle? bundle)
    {
        bundle = null;

        var declaration = FindDeclarationSyntax(owningSymbol, contextSyntax);
        if (declaration is null)
        {
            return false;
        }

        var key = new FlowAnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
        var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<AnalysisBundle?>(() =>
            ComputeAnalysis(owningSymbol, declaration, contextModel), LazyThreadSafetyMode.ExecutionAndPublication));

        bundle = lazy.Value;
        return bundle is not null;
    }

    private AnalysisBundle? ComputeAnalysis(
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

            var valueContentResult = ValueContentAnalysis.TryGetOrComputeResult(
                controlFlow,
                owningSymbol,
                wellKnownProvider,
                EmptyAnalyzerOptions,
                FlowAnalysisRule,
                PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
                out var copyAnalysisResult,
                out var pointsToAnalysisResult,
                interprocedural.InterproceduralAnalysisKind,
                pessimisticAnalysis: false,
                performCopyAnalysisIfNotUserConfigured: true);

            return new AnalysisBundle(valueContentResult, pointsToAnalysisResult, copyAnalysisResult);
        }
        catch (Exception ex) when (IsBenignAnalysisException(ex))
        {
            return null;
        }
    }

    private WellKnownTypeProvider GetWellKnownTypeProvider(Compilation compilation)
        => _wellKnownTypeProviders.GetValue(compilation, static c => WellKnownTypeProvider.GetOrCreate(c));

    private static bool TryExtractString(
        ValueContentAnalysisResult analysis,
        IOperation operation,
        out string? value)
    {
        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            value = null;
            return false;
        }

        if (ReferenceEquals(abstractValue, ValueContentAbstractValue.ContainsNullLiteralState))
        {
            value = null;
            return true;
        }

        if (ReferenceEquals(abstractValue, ValueContentAbstractValue.ContainsEmptyStringLiteralState))
        {
            value = string.Empty;
            return true;
        }

        if (abstractValue.TryGetSingleNonNullLiteral(out string? literal) && literal is not null)
        {
            value = literal;
            return true;
        }

        if (abstractValue.IsLiteralState)
        {
            foreach (var candidate in abstractValue.LiteralValues)
            {
                if (candidate is string text)
                {
                    value = text;
                    return true;
                }
            }
        }

        value = null;
        return false;
    }

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

    private sealed record AnalysisBundle(
        ValueContentAnalysisResult? ValueContent,
        PointsToAnalysisResult? PointsTo,
        CopyAnalysisResult? Copy);
}
