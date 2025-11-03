using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;

namespace GraphKit.FlowAnalysis.Dependencies
{
    public sealed class FlowValueContentFacade
    {
        private const string AnalyzerAssemblyFileName = "Microsoft.CodeAnalysis.NetAnalyzers.dll";

        private static readonly AnalyzerOptions EmptyAnalyzerOptions = new(ImmutableArray<AdditionalText>.Empty);

        private static readonly DiagnosticDescriptor FlowAnalysisRule = new(
            id: "GKFLOW0001",
            title: "GraphKit flow analysis",
            messageFormat: "GraphKit flow analysis placeholder",
            category: "GraphKit",
            defaultSeverity: DiagnosticSeverity.Hidden,
            isEnabledByDefault: true);

        private static readonly Lazy<ValueContentInvoker?> Invoker = new(CreateInvoker);

        private readonly ConditionalWeakTable<Compilation, object> _wellKnownTypeProviders = new();
        private readonly ConcurrentDictionary<AnalysisCacheKey, Lazy<object?>> _analysisCache = new();

        public FlowValueContentFacade(
            InterproceduralAnalysisConfiguration configuration,
            FlowCallsitePredicate pruningPredicate)
        {
            Configuration = configuration;
            PruningPredicate = pruningPredicate;
        }

        public InterproceduralAnalysisConfiguration Configuration { get; }

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

            if (!TryGetAnalysis(owningSymbol, model, op.Syntax, out var analysis, out var invoker))
            {
                return null;
            }

            return TryExtractString(analysis, op, invoker, out var reconstructed) ? reconstructed : null;
        }

        private bool TryGetAnalysis(
            ISymbol owningSymbol,
            SemanticModel contextModel,
            SyntaxNode contextSyntax,
            out object? analysis,
            out ValueContentInvoker? invoker)
        {
            analysis = null;
            invoker = Invoker.Value;
            if (invoker is null)
            {
                return false;
            }

            var declaration = FindDeclarationSyntax(owningSymbol, contextSyntax);
            if (declaration is null)
            {
                return false;
            }

            var key = new AnalysisCacheKey(declaration.SyntaxTree, declaration.Span);
            var currentInvoker = invoker!;
            var lazy = _analysisCache.GetOrAdd(key, _ => new Lazy<object?>(() =>
                ComputeAnalysis(owningSymbol, declaration, contextModel, currentInvoker), LazyThreadSafetyMode.ExecutionAndPublication));

            analysis = lazy.Value;
            return analysis is not null;
        }

        private object? ComputeAnalysis(
            ISymbol owningSymbol,
            SyntaxNode declarationSyntax,
            SemanticModel contextModel,
            ValueContentInvoker invoker)
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

                var wellKnownProvider = GetWellKnownTypeProvider(compilation, invoker);
                return invoker.ComputeValueContent(
                    controlFlow,
                    owningSymbol,
                    wellKnownProvider,
                    EmptyAnalyzerOptions,
                    FlowAnalysisRule,
                    Configuration.InterproceduralAnalysisKind);
            }
            catch (Exception ex) when (IsBenignAnalysisException(ex))
            {
                return null;
            }
        }

        private object GetWellKnownTypeProvider(Compilation compilation, ValueContentInvoker invoker)
        {
            if (_wellKnownTypeProviders.TryGetValue(compilation, out var provider))
            {
                return provider;
            }

            provider = invoker.CreateWellKnownTypeProvider(compilation);
            _wellKnownTypeProviders.Add(compilation, provider);
            return provider;
        }

        private static bool TryExtractString(object analysis, IOperation operation, ValueContentInvoker invoker, out string? value)
        {
            var abstractValue = invoker.GetAbstractValue(analysis, operation);
            if (abstractValue is null)
            {
                value = null;
                return false;
            }

            if (invoker.IsNullLiteral(abstractValue))
            {
                value = null;
                return true;
            }

            if (invoker.IsEmptyStringLiteral(abstractValue))
            {
                value = string.Empty;
                return true;
            }

            if (invoker.TryGetSingleString(abstractValue, out var literal) && literal is not null)
            {
                value = literal;
                return true;
            }

            if (invoker.IsLiteralState(abstractValue))
            {
                foreach (var candidate in invoker.GetLiteralValues(abstractValue))
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

        private static ValueContentInvoker? CreateInvoker()
        {
            try
            {
                var baseDirectory = AppContext.BaseDirectory;
                var assemblyPath = Path.Combine(baseDirectory, AnalyzerAssemblyFileName);
                if (!File.Exists(assemblyPath))
                {
                    return null;
                }

                var assembly = Assembly.LoadFrom(assemblyPath);
                return ValueContentInvoker.Create(assembly);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsBenignAnalysisException(Exception exception)
            => exception is InvalidOperationException or NotSupportedException or OperationCanceledException or TargetInvocationException;

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

        private sealed class ValueContentInvoker
        {
            private readonly MethodInfo _analysisMethod;
            private readonly Func<Compilation, object> _providerFactory;
            private readonly PropertyInfo _analysisIndexer;
            private readonly MethodInfo _tryGetSingleString;
            private readonly PropertyInfo _literalValuesProperty;
            private readonly PropertyInfo _isLiteralStateProperty;
            private readonly object _nullLiteralState;
            private readonly object _emptyStringLiteralState;
            private readonly object _partialPointsToKind;
            private readonly Type _interproceduralKindType;

            private ValueContentInvoker(
                MethodInfo analysisMethod,
                Func<Compilation, object> providerFactory,
                PropertyInfo analysisIndexer,
                MethodInfo tryGetSingleString,
                PropertyInfo literalValuesProperty,
                PropertyInfo isLiteralStateProperty,
                object nullLiteralState,
                object emptyStringLiteralState,
                object partialPointsToKind,
                Type interproceduralKindType)
            {
                _analysisMethod = analysisMethod;
                _providerFactory = providerFactory;
                _analysisIndexer = analysisIndexer;
                _tryGetSingleString = tryGetSingleString;
                _literalValuesProperty = literalValuesProperty;
                _isLiteralStateProperty = isLiteralStateProperty;
                _nullLiteralState = nullLiteralState;
                _emptyStringLiteralState = emptyStringLiteralState;
                _partialPointsToKind = partialPointsToKind;
                _interproceduralKindType = interproceduralKindType;
            }

            public static ValueContentInvoker? Create(Assembly assembly)
            {
                var analysisType = assembly.GetType("Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentAnalysis");
                var abstractValueType = assembly.GetType("Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis.ValueContentAbstractValue");
                var providerType = assembly.GetType("Analyzer.Utilities.WellKnownTypeProvider");
                var pointsToKindType = assembly.GetType("Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis.PointsToAnalysisKind");
                var interproceduralKindType = assembly.GetType("Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.InterproceduralAnalysisKind");
                if (analysisType is null || abstractValueType is null || providerType is null || pointsToKindType is null || interproceduralKindType is null)
                {
                    return null;
                }

                var analysisMethod = analysisType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => string.Equals(m.Name, "TryGetOrComputeResult", StringComparison.Ordinal) && m.GetParameters().Length == 8);
                if (analysisMethod is null)
                {
                    return null;
                }

                var providerCtor = providerType.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(Compilation));
                if (providerCtor is null)
                {
                    return null;
                }

                object ProviderFactory(Compilation compilation)
                    => providerCtor.Invoke(new object[] { compilation });

                var analysisResultType = analysisMethod.ReturnType;
                var indexer = analysisResultType.GetProperty("Item", new[] { typeof(IOperation) });
                if (indexer is null)
                {
                    return null;
                }

                var tryGetSingleMethod = abstractValueType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(m => m.Name == "TryGetSingleNonNullLiteral" && m.IsGenericMethodDefinition && m.GetParameters().Length == 1);
                if (tryGetSingleMethod is null)
                {
                    return null;
                }

                var literalValuesProperty = abstractValueType.GetProperty("LiteralValues", BindingFlags.Public | BindingFlags.Instance);
                var isLiteralStateProperty = abstractValueType.GetProperty("IsLiteralState", BindingFlags.Public | BindingFlags.Instance);
                if (literalValuesProperty is null || isLiteralStateProperty is null)
                {
                    return null;
                }

                var nullLiteralProperty = abstractValueType.GetProperty("ContainsNullLiteralState", BindingFlags.Public | BindingFlags.Static);
                var emptyLiteralProperty = abstractValueType.GetProperty("ContainsEmptyStringLiteralState", BindingFlags.Public | BindingFlags.Static);
                if (nullLiteralProperty is null || emptyLiteralProperty is null)
                {
                    return null;
                }

                var partialValue = Enum.Parse(pointsToKindType, "PartialWithoutTrackingFieldsAndProperties", ignoreCase: false);

                return new ValueContentInvoker(
                    analysisMethod,
                    ProviderFactory,
                    indexer,
                    tryGetSingleMethod.MakeGenericMethod(typeof(string)),
                    literalValuesProperty,
                    isLiteralStateProperty,
                    nullLiteralProperty.GetValue(null)!,
                    emptyLiteralProperty.GetValue(null)!,
                    partialValue!,
                    interproceduralKindType);
            }

            public object CreateWellKnownTypeProvider(Compilation compilation)
                => _providerFactory(compilation);

            public object? ComputeValueContent(
                ControlFlowGraph cfg,
                ISymbol owningSymbol,
                object wellKnownTypeProvider,
                AnalyzerOptions options,
                DiagnosticDescriptor rule,
                InterproceduralAnalysisKind interproceduralKind)
            {
                var interproceduralValue = Enum.ToObject(_interproceduralKindType, Convert.ToInt32(interproceduralKind));
                var parameters = new object?[]
                {
                    cfg,
                    owningSymbol,
                    wellKnownTypeProvider,
                    options,
                    rule,
                    _partialPointsToKind,
                    interproceduralValue,
                    false
                };

                return _analysisMethod.Invoke(null, parameters);
            }

            public object? GetAbstractValue(object analysisResult, IOperation operation)
                => _analysisIndexer.GetValue(analysisResult, new object[] { operation });

            public bool TryGetSingleString(object abstractValue, out string? literal)
            {
                var args = new object?[] { null };
                var success = _tryGetSingleString.Invoke(abstractValue, args);
                if (success is bool flag && flag)
                {
                    literal = args[0] as string;
                    return true;
                }

                literal = null;
                return false;
            }

            public bool IsLiteralState(object abstractValue)
                => _isLiteralStateProperty.GetValue(abstractValue) is true;

            public IEnumerable<object?> GetLiteralValues(object abstractValue)
            {
                if (_literalValuesProperty.GetValue(abstractValue) is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        yield return item;
                    }
                }
            }

            public bool IsNullLiteral(object abstractValue)
                => ReferenceEquals(abstractValue, _nullLiteralState);

            public bool IsEmptyStringLiteral(object abstractValue)
                => ReferenceEquals(abstractValue, _emptyStringLiteralState);
        }
    }
}
