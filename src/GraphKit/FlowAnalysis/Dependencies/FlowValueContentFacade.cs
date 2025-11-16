using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

    private static readonly InterproceduralAnalysisPredicate NoOpPredicate = new(
        static _ => false,
        static _ => false,
        static _ => false);

    private readonly ConcurrentDictionary<AnalysisCacheKey, Lazy<ValueContentAnalysisResult?>> _analysisCache = new();

    public FlowValueContentFacade(
        InterproceduralSettings configuration,
        FlowCallsitePredicate pruningPredicate)
    {
        Settings = configuration;
        PruningPredicate = pruningPredicate;
    }

    public InterproceduralSettings Settings { get; }

    public FlowCallsitePredicate PruningPredicate { get; }

    public ValueDescription DescribeStringValue(IOperation? op)
    {
        if (op is null)
        {
            return ValueDescription.None;
        }

        if (op is IConversionOperation conversion)
        {
            return DescribeStringValue(conversion.Operand);
        }

        if (op.ConstantValue is { HasValue: true } constant)
        {
            if (constant.Value is string literal)
            {
                return ValueDescription.FromLiteral(literal);
            }

            if (constant.Value is null)
            {
                return ValueDescription.NullLiteral;
            }
        }

        if (!TryGetAnalysisFor(op, out var analysis) || analysis is null)
        {
            return ValueDescription.None;
        }

        return TryDescribeString(analysis, op, out var description) ? description : ValueDescription.None;
    }

    public bool TryGetStringLiterals(IOperation? op, out ImmutableArray<string> values)
    {
        var description = DescribeStringValue(op);
        if (description.HasLiterals)
        {
            values = description.Literals;
            return true;
        }

        values = ImmutableArray<string>.Empty;
        return false;
    }

    public bool MayBeNull(IOperation? op)
    {
        var description = DescribeStringValue(op);
        return description.MayBeNull;
    }

    public string? TryGetStringValue(IOperation op)
        => DescribeStringValue(op).FirstNonEmptyLiteralOrDefault;

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

        var pointsToResult = PointsToAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            EmptyAnalyzerOptions,
            wellKnownProvider,
            PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
            interproceduralConfiguration,
            NoOpPredicate,
            pessimisticAnalysis: false,
            performCopyAnalysis: false,
            exceptionPathsAnalysis: false);

        var valueContentResult = ValueContentAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            wellKnownProvider,
            EmptyAnalyzerOptions,
            FlowAnalysisRule,
            PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
            settings.Kind,
            pessimisticAnalysis: false);

        return valueContentResult;
    }

    private static bool TryDescribeString(
        ValueContentAnalysisResult analysis,
        IOperation operation,
        out ValueDescription description)
    {
        description = ValueDescription.None;
        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            return false;
        }

        description = BuildDescription(abstractValue);
        return true;
    }

    private static ValueDescription BuildDescription(ValueContentAbstractValue abstractValue)
    {
        var literals = ImmutableArray.CreateBuilder<string>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var includesNull = false;

        if (abstractValue.TryGetSingleNonNullLiteral(out string? literal) && literal is not null)
        {
            seen.Add(literal);
            literals.Add(literal);
        }
        else if (abstractValue.IsLiteralState)
        {
            foreach (var candidate in abstractValue.LiteralValues)
            {
                switch (candidate)
                {
                    case string text when seen.Add(text):
                        literals.Add(text);
                        break;
                    case null:
                        includesNull = true;
                        break;
                }
            }
        }
        else if (Equals(abstractValue, ValueContentAbstractValue.ContainsNullLiteralState))
        {
            includesNull = true;
        }

        seen.Clear();
        return new ValueDescription(literals.ToImmutable(), includesNull, abstractValue.NonLiteralState, hasValue: true);
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

    public readonly record struct ValueDescription
    {
        public static ValueDescription None { get; } = new(
            ImmutableArray<string>.Empty,
            includesNullLiteral: false,
            ValueContainsNonLiteralState.Undefined,
            hasValue: false);

        public static ValueDescription NullLiteral { get; } = new(
            ImmutableArray<string>.Empty,
            includesNullLiteral: true,
            ValueContainsNonLiteralState.No,
            hasValue: true);

        public static ValueDescription FromLiteral(string literal)
            => new(ImmutableArray.Create(literal), includesNullLiteral: false, ValueContainsNonLiteralState.No, hasValue: true);

        public ValueDescription(
            ImmutableArray<string> literals,
            bool includesNullLiteral,
            ValueContainsNonLiteralState nonLiteralState,
            bool hasValue)
        {
            Literals = literals.IsDefault ? ImmutableArray<string>.Empty : literals;
            IncludesNullLiteral = includesNullLiteral;
            NonLiteralState = nonLiteralState;
            HasValue = hasValue;
        }

        public ImmutableArray<string> Literals { get; }

        public bool IncludesNullLiteral { get; }

        public ValueContainsNonLiteralState NonLiteralState { get; }

        public bool HasValue { get; }

        public bool HasLiterals => !Literals.IsDefaultOrEmpty && Literals.Length > 0;

        public bool HasSingleLiteral => HasLiterals && Literals.Length == 1;

        public string? SingleLiteralOrDefault => HasSingleLiteral ? Literals[0] : null;

        public string? FirstLiteralOrDefault => HasLiterals ? Literals[0] : null;

        public string? FirstNonEmptyLiteralOrDefault
        {
            get
            {
                if (!HasLiterals)
                {
                    return null;
                }

                foreach (var literal in Literals)
                {
                    if (!string.IsNullOrWhiteSpace(literal))
                    {
                        return literal;
                    }
                }

                return Literals[0];
            }
        }

        public bool ContainsNonLiteralValues => NonLiteralState is ValueContainsNonLiteralState.Maybe;

        public bool MayBeNull
            => IncludesNullLiteral
               || NonLiteralState is ValueContainsNonLiteralState.Maybe
               || NonLiteralState is ValueContainsNonLiteralState.Invalid
               || NonLiteralState is ValueContainsNonLiteralState.Undefined
               || !HasValue;
    }
}
