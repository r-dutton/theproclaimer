using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        FlowPointsToFacade pointsToFacade)
        : this(configuration, pointsToFacade?.InterproceduralPredicate, pointsToFacade?.Options)
    {
    }

    public FlowValueContentFacade(
        InterproceduralSettings configuration,
        FlowCallsitePredicate pruningPredicate,
        FlowPointsToAnalysisOptions? pointsToOptions = null)
        : this(configuration, CreateInterproceduralPredicate(pruningPredicate), pointsToOptions)
    {
    }

    public FlowValueContentFacade(
        InterproceduralSettings configuration,
        InterproceduralAnalysisPredicate? analysisPredicate,
        FlowPointsToAnalysisOptions? pointsToOptions = null)
    {
        Settings = configuration;
        AnalysisPredicate = analysisPredicate ?? AllowAllPredicate;
        PointsToOptions = pointsToOptions ?? FlowPointsToAnalysisOptions.Fast;
    }

    public InterproceduralSettings Settings { get; }

    private InterproceduralAnalysisPredicate AnalysisPredicate { get; }

    public FlowPointsToAnalysisOptions PointsToOptions { get; }

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

    public IEnumerable<FlowContentCandidate> EnumerateContentCandidates(IOperation? operation)
    {
        if (operation is null)
        {
            yield break;
        }

        // Use the default comparer to allow null entries without throwing when hashing.
        var emittedLiterals = new HashSet<string?>();
        if (operation.ConstantValue is { HasValue: true } constant)
        {
            if (constant.Value is string constantString)
            {
                if (emittedLiterals.Add(constantString))
                {
                    yield return FlowContentCandidate.FromLiteral(constantString);
                }
            }
            else if (constant.Value is null && emittedLiterals.Add(null))
            {
                yield return FlowContentCandidate.Null;
            }
        }

        if (!TryGetAnalysisFor(operation, out var analysis) || analysis is null)
        {
            yield break;
        }

        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            yield break;
        }

        foreach (var literal in abstractValue.LiteralValues)
        {
            if (literal is string literalString)
            {
                if (emittedLiterals.Add(literalString))
                {
                    yield return FlowContentCandidate.FromLiteral(literalString);
                }
            }
            else if (literal is null && emittedLiterals.Add(null))
            {
                yield return FlowContentCandidate.Null;
            }
        }

        if (abstractValue.NonLiteralState == ValueContainsNonLiteralState.Maybe)
        {
            var segments = DescribeOperation(operation, analysis);
            if (!segments.IsDefaultOrEmpty)
            {
                yield return FlowContentCandidate.FromSegments(segments);
            }
        }
    }

    public bool? MayBeNull(IOperation? operation)
    {
        if (operation is null)
        {
            return null;
        }

        if (operation.ConstantValue is { HasValue: true } constant)
        {
            return constant.Value is null;
        }

        if (!TryGetAnalysisFor(operation, out var analysis) || analysis is null)
        {
            return null;
        }

        var abstractValue = analysis[operation];
        if (abstractValue is null)
        {
            return null;
        }

        if (abstractValue.LiteralValues.Contains(null))
        {
            return true;
        }

        return abstractValue.NonLiteralState switch
        {
            ValueContainsNonLiteralState.No => false,
            _ => null
        };
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
        var pointsToOptions = PointsToOptions;
        var interproceduralConfiguration = InterproceduralAnalysisConfiguration.Create(
            EmptyAnalyzerOptions,
            ImmutableArray.Create(FlowAnalysisRule),
            controlFlowGraph,
            compilation,
            settings.Kind,
            (uint)Math.Max(0, settings.MaxCallChainLength),
            (uint)Math.Max(0, settings.MaxLambdaOrLocalFunctionDepth));

        _ = PointsToAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            EmptyAnalyzerOptions,
            wellKnownProvider,
            pointsToOptions.PointsToAnalysisKind,
            interproceduralConfiguration,
            AnalysisPredicate,
            pessimisticAnalysis: pointsToOptions.PessimisticAnalysis,
            performCopyAnalysis: pointsToOptions.PerformCopyAnalysis,
            exceptionPathsAnalysis: pointsToOptions.ExceptionPathsAnalysis);

        var valueContentResult = ValueContentAnalysis.TryGetOrComputeResult(
            controlFlowGraph,
            owningSymbol,
            wellKnownProvider,
            EmptyAnalyzerOptions,
            FlowAnalysisRule,
            pointsToOptions.PointsToAnalysisKind,
            settings.Kind,
            pessimisticAnalysis: pointsToOptions.PessimisticAnalysis);

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

    private static ImmutableArray<FlowContentSegment> DescribeOperation(
        IOperation operation,
        ValueContentAnalysisResult? analysis)
    {
        var builder = ImmutableArray.CreateBuilder<FlowContentSegment>();
        var visited = new HashSet<IOperation>(ReferenceEqualityComparer.Instance);
        AppendSegments(operation, analysis, builder, visited);
        return builder.ToImmutable();
    }

    private static void AppendSegments(
        IOperation? operation,
        ValueContentAnalysisResult? analysis,
        ImmutableArray<FlowContentSegment>.Builder builder,
        HashSet<IOperation> visited)
    {
        if (operation is null)
        {
            return;
        }

        if (!visited.Add(operation))
        {
            builder.Add(FlowContentSegment.NonLiteral());
            return;
        }

        switch (operation)
        {
            case ILiteralOperation literal:
                var literalValue = literal.ConstantValue.HasValue ? literal.ConstantValue.Value : null;
                builder.Add(FlowContentSegment.FromLiteral(literalValue));
                break;

            case IInterpolatedStringOperation interpolated:
                foreach (var part in interpolated.Parts)
                {
                    switch (part)
                    {
                        case IInterpolatedStringTextOperation text:
                            builder.Add(FlowContentSegment.Literal(text.Text));
                            break;
                        case IInterpolationOperation interpolation:
                            AppendSegments(interpolation.Expression, analysis, builder, visited);
                            break;
                        default:
                            builder.Add(FlowContentSegment.NonLiteral());
                            break;
                    }
                }
                break;

            case IBinaryOperation binary when binary.OperatorKind is BinaryOperatorKind.Add or BinaryOperatorKind.Concatenate:
                AppendSegments(binary.LeftOperand, analysis, builder, visited);
                AppendSegments(binary.RightOperand, analysis, builder, visited);
                break;

            case IConversionOperation conversion when conversion.Type?.SpecialType == SpecialType.System_String:
                AppendSegments(conversion.Operand, analysis, builder, visited);
                break;

            default:
                if (!TryAppendLiteralFromAnalysis(operation, analysis, builder))
                {
                    builder.Add(FlowContentSegment.NonLiteral());
                }

                break;
        }

        visited.Remove(operation);
    }

    private static bool TryAppendLiteralFromAnalysis(
        IOperation operation,
        ValueContentAnalysisResult? analysis,
        ImmutableArray<FlowContentSegment>.Builder builder)
    {
        if (analysis is null)
        {
            return false;
        }

        var abstractValue = analysis[operation];
        if (abstractValue is null || abstractValue.LiteralValues.Count == 0)
        {
            return false;
        }

        if (abstractValue.LiteralValues.Contains(null))
        {
            builder.Add(FlowContentSegment.Null());
            return true;
        }

        var literal = abstractValue.LiteralValues.OfType<string>().FirstOrDefault();
        if (literal is not null)
        {
            builder.Add(FlowContentSegment.Literal(literal));
            return true;
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

public enum FlowContentSegmentKind
{
    Literal,
    NonLiteral,
    NullLiteral
}

public readonly record struct FlowContentSegment(FlowContentSegmentKind Kind, string? Value)
{
    public static FlowContentSegment Literal(string? value)
        => new(FlowContentSegmentKind.Literal, value ?? string.Empty);

    public static FlowContentSegment NonLiteral(string? value = null)
        => new(FlowContentSegmentKind.NonLiteral, value);

    public static FlowContentSegment Null()
        => new(FlowContentSegmentKind.NullLiteral, null);

    public static FlowContentSegment FromLiteral(object? literal)
    {
        if (literal is null)
        {
            return Null();
        }

        if (literal is string text)
        {
            return Literal(text);
        }

        return Literal(literal.ToString());
    }
}

public sealed record FlowContentCandidate
{
    public FlowContentCandidate(ImmutableArray<FlowContentSegment> segments)
    {
        Segments = segments;
    }

    public ImmutableArray<FlowContentSegment> Segments { get; }

    public static FlowContentCandidate FromLiteral(string? value)
        => new(ImmutableArray.Create(value is null ? FlowContentSegment.Null() : FlowContentSegment.Literal(value)));

    public static FlowContentCandidate FromSegments(ImmutableArray<FlowContentSegment> segments)
        => new(segments);

    public static FlowContentCandidate Null { get; } = new(ImmutableArray.Create(FlowContentSegment.Null()));

    public bool IsLiteral => !Segments.IsDefaultOrEmpty && Segments.All(segment => segment.Kind == FlowContentSegmentKind.Literal);

    public bool ContainsNonLiteralSegments => !Segments.IsDefaultOrEmpty && Segments.Any(segment => segment.Kind == FlowContentSegmentKind.NonLiteral);

    public string? TryGetLiteralText()
    {
        if (Segments.IsDefaultOrEmpty)
        {
            return null;
        }

        if (Segments.Length == 1 && Segments[0].Kind == FlowContentSegmentKind.NullLiteral)
        {
            return null;
        }

        if (!IsLiteral)
        {
            return null;
        }

        var builder = new StringBuilder();
        foreach (var segment in Segments)
        {
            builder.Append(segment.Value);
        }

        return builder.ToString();
    }

    public string ToDisplayString(string nonLiteralPlaceholder = "{*}")
    {
        if (Segments.IsDefaultOrEmpty)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        foreach (var segment in Segments)
        {
            switch (segment.Kind)
            {
                case FlowContentSegmentKind.Literal:
                    builder.Append(segment.Value);
                    break;
                case FlowContentSegmentKind.NullLiteral:
                    builder.Append("null");
                    break;
                default:
                    builder.Append(string.IsNullOrWhiteSpace(segment.Value) ? nonLiteralPlaceholder : segment.Value);
                    break;
            }
        }

        return builder.ToString();
    }
}
