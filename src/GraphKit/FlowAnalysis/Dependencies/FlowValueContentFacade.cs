using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Analyzer.Utilities;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using ValueContentAnalysisResult = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.DataFlowAnalysisResult<Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentBlockAnalysisResult, Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentAbstractValue>;

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

    private static readonly ValueContentAnalysisResult? PlaceholderResult = null;

    private static readonly InterproceduralAnalysisPredicate AllowAllPredicate = new(
        static _ => true,
        static _ => true,
        static _ => true);

    private readonly ConcurrentDictionary<AnalysisCacheKey, Lazy<ValueContentAnalysisResult?>> _analysisCache = new();

    public FlowValueContentFacade(
        InterproceduralSettings configuration,
        FlowCallsitePredicate pruningPredicate)
    {
        Settings = configuration;
        PruningPredicate = pruningPredicate;
        AnalysisPredicate = CreateInterproceduralPredicate(pruningPredicate);
    }

    public InterproceduralSettings Settings { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    private InterproceduralAnalysisPredicate AnalysisPredicate { get; }

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

        if (!TryGetAnalysis(owningSymbol, model, op.Syntax, out var analysis))
        {
            return null;
        }

        return TryExtractString(analysis, op, out var reconstructed) ? reconstructed : null;
    }

    public bool? TryGetBooleanValue(IOperation op)
    {
        if (op is null) return null;
        if (op.ConstantValue is { HasValue: true, Value: bool b }) return b;
        if (!TryGetAnalysisFor(op, out var analysis)) return null;
        return TryExtractBoolean(analysis!, op, out var value) ? value : null;
    }

    public long? TryGetIntegralValue(IOperation op)
    {
        if (op is null) return null;
        if (op.ConstantValue is { HasValue: true, Value: int i }) return i;
        if (op.ConstantValue is { HasValue: true, Value: long l }) return l;
        if (!TryGetAnalysisFor(op, out var analysis)) return null;
        return TryExtractIntegral(analysis!, op, out var value) ? value : null;
    }

    private bool TryGetAnalysisFor(IOperation op, out ValueContentAnalysisResult? analysis)
    {
        analysis = null;
        if (op.SemanticModel is not { } model)
        {
            return false;
        }

        var owningSymbol = model.GetEnclosingSymbol(op.Syntax.SpanStart);
        if (owningSymbol is null)
        {
            return false;
        }

        return TryGetAnalysis(owningSymbol, model, op.Syntax, out analysis);
    }

    private bool TryGetAnalysis(
        ISymbol owningSymbol,
        SemanticModel contextModel,
        SyntaxNode contextSyntax,
        out ValueContentAnalysisResult? analysis)
    {
        analysis = null;
        var declaration = FindDeclarationSyntax(owningSymbol, contextSyntax);
        if (declaration is null)
        {
            return false;
        }

        var key = new AnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
        var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<ValueContentAnalysisResult?>(() =>
            ComputeAnalysis(owningSymbol, declaration, contextModel), LazyThreadSafetyMode.ExecutionAndPublication));

        analysis = lazy.Value;
        return analysis is not null;
    }

    private ValueContentAnalysisResult? ComputeAnalysis(
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

                if (!methodAnalysis.ValueContentComputed)
                {
                    var methodContext = methodAnalysis.Context;
                    var declaration = methodContext.Declaration ?? declarationSyntax;
                    var methodSemanticModel = methodContext.SemanticModel ?? compilation.GetSemanticModel(declaration.SyntaxTree);
                    var controlFlow = methodContext.ControlFlowGraph ?? ControlFlowGraph.Create(declaration, methodSemanticModel, CancellationToken.None);

                    ValueContentAnalysisResult? computed = PlaceholderResult;
                    if (controlFlow is not null)
                    {
                        computed = RunValueContentAnalysis(controlFlow, owningSymbol, compilation);
                    }

                    methodAnalysis.ValueContentAnalysis = computed;
                    methodAnalysis.ValueContentComputed = true;
                }

                return methodAnalysis.ValueContentAnalysis;
            }

            var semanticModel = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);
            var cfg = ControlFlowGraph.Create(declarationSyntax, semanticModel, CancellationToken.None);
            return cfg is null ? null : RunValueContentAnalysis(cfg, owningSymbol, compilation);
        }
        catch (Exception ex) when (IsBenignAnalysisException(ex))
        {
            return null;
        }
    }

    private ValueContentAnalysisResult? RunValueContentAnalysis(
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

        var valueContentResult = ValueContentAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            EmptyAnalyzerOptions,
            wellKnownProvider,
            PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
            interproceduralConfiguration,
            out _,
            out _,
            pessimisticAnalysis: false,
            performCopyAnalysis: false,
            interproceduralAnalysisPredicate: AnalysisPredicate);

        return valueContentResult;
    }

    private static bool TryExtractString(
        ValueContentAnalysisResult analysis,
        IOperation operation,
        out string? value)
    {
        value = null;
        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            return false;
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

                if (candidate is null)
                {
                    value = null;
                    return true;
                }
            }
        }

        if (Equals(abstractValue, ValueContentAbstractValue.ContainsNullLiteralState))
        {
            value = null;
            return true;
        }

        return false;
    }

    private static bool TryExtractBoolean(
        ValueContentAnalysisResult analysis,
        IOperation operation,
        out bool? value)
    {
        value = null;
        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            return false;
        }

        foreach (var candidate in abstractValue.LiteralValues)
        {
            if (candidate is bool cb)
            {
                value = cb;
                return true;
            }
        }

        return false;
    }

    private static bool TryExtractIntegral(
        ValueContentAnalysisResult analysis,
        IOperation operation,
        out long? value)
    {
        value = null;
        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            return false;
        }

        foreach (var candidate in abstractValue.LiteralValues)
        {
            switch (candidate)
            {
                case int i:
                    value = i; return true;
                case long l:
                    value = l; return true;
            }
        }

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

    private static InterproceduralAnalysisPredicate CreateInterproceduralPredicate(FlowCallsitePredicate predicate)
    {
        if (predicate is null)
        {
            return AllowAllPredicate;
        }

        bool ShouldAnalyzeInvocation(IOperation operation)
            => operation is IInvocationOperation invocation && predicate(invocation);

        return new InterproceduralAnalysisPredicate(
            ShouldAnalyzeInvocation,
            static _ => true,
            static _ => true);
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
