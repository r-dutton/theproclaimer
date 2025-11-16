using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.CodeAnalysis.Operations;
using GraphKit.FlowAnalysis.Interprocedural;
using InterproceduralAnalysisConfiguration = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.InterproceduralAnalysisConfiguration;
using InterproceduralAnalysisKind = Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.InterproceduralAnalysisKind;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private readonly ProjectAnalyzerConfiguration _configuration;
    private readonly InterproceduralSettings _interproceduralConfiguration;

    internal InterproceduralSettings InterproceduralConfiguration => _interproceduralConfiguration;

    private static ProjectAnalyzerConfiguration EnsureConfiguration(ProjectAnalyzerConfiguration? configuration)
    {
        var normalized = (configuration ?? ProjectAnalyzerConfiguration.Default).Normalize();
        return normalized;
    }

    private FlowPointsToFacade CreatePointsToFacade(
        FlowCallsitePredicate predicate,
        FlowPointsToPrecision? precision = null,
        string? feature = null)
    {
        var options = EnsureCopyAnalysisOption(
            _configuration.GetPointsToAnalysisOptions(precision),
            feature);
        return new(_interproceduralConfiguration, predicate, options);
    }

    private FlowValueContentFacade CreateValueContentFacade(
        FlowCallsitePredicate predicate,
        string? feature = null,
        FlowPointsToPrecision? precision = null)
    {
        var options = EnsureCopyAnalysisOption(
            _configuration.GetPointsToAnalysisOptions(precision),
            feature);
        return new(_interproceduralConfiguration, predicate, options);
    }

    private FlowValueContentFacade CreateValueContentFacade(
        FlowPointsToFacade pointsToFacade,
        string? feature = null)
    {
        if (pointsToFacade is null)
        {
            throw new ArgumentNullException(nameof(pointsToFacade));
        }

        var requiresCopyAnalysis = ShouldPerformCopyAnalysis(feature);
        if (!requiresCopyAnalysis || pointsToFacade.Options.PerformCopyAnalysis)
        {
            return new(_interproceduralConfiguration, pointsToFacade);
        }

        var upgradedOptions = pointsToFacade.Options with { PerformCopyAnalysis = true };
        return new(_interproceduralConfiguration, pointsToFacade, upgradedOptions);
    }

    private FlowCopyAnalysisFacade CreateCopyAnalysisFacade(FlowCallsitePredicate predicate)
        => new(_interproceduralConfiguration, predicate);

    private FlowNullAnalysisFacade CreateNullAnalysisFacade(FlowPointsToFacade pointsTo)
        => new(pointsTo);

    private FlowPredicateAnalysisFacade CreatePredicateAnalysisFacade(FlowPointsToFacade pointsTo)
        => new(pointsTo);

    private FlowTaintedDataFacade CreateTaintedDataFacade(FlowCallsitePredicate predicate)
        => new(_interproceduralConfiguration, predicate, FlowTaintedDataConfiguration.Empty);

    private static FlowCallsitePredicate ComposeInterproceduralPredicate(FlowCallsitePredicate predicate)
        => invocation => !ShouldPruneInterproceduralInvocation(invocation) && predicate(invocation);

    private static bool ShouldPruneInterproceduralInvocation(IInvocationOperation invocation)
    {
        if (invocation?.TargetMethod is not { } method)
        {
            return false;
        }

        var receiver = invocation.Instance?.Type?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        var containing = method.ContainingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);

        if (IsLoggerType(receiver) || IsLoggerType(containing))
        {
            return true;
        }

        if (IsMetricsType(receiver) || IsMetricsType(containing))
        {
            return true;
        }

        if (IsTelemetryType(receiver) || IsTelemetryType(containing))
        {
            return true;
        }

        return false;
    }

    public sealed record ProjectAnalyzerConfiguration
    {
        public int MaxInterproceduralCallChainLength { get; init; } = 4;

        public int MaxInterproceduralLambdaOrLocalFunctionDepth { get; init; } = 2;

        public InterproceduralAnalysisKind InterproceduralAnalysisKind { get; init; } = InterproceduralAnalysisKind.ContextSensitive;

        public FlowPointsToPrecision DefaultPointsToPrecision { get; init; } = FlowPointsToPrecision.Fast;

        public bool EnableValueContentCopyAnalysis { get; init; }

        public string[] ValueContentCopyAnalysisFeatures { get; init; } = Array.Empty<string>();

        public PointsToAnalysisKind PointsToAnalysisKind { get; init; } = FlowPointsToAnalysisOptions.Fast.PointsToAnalysisKind;

        public bool PerformCopyAnalysis { get; init; } = FlowPointsToAnalysisOptions.Fast.PerformCopyAnalysis;

        public bool PessimisticAnalysis { get; init; } = FlowPointsToAnalysisOptions.Fast.PessimisticAnalysis;

        public bool ExceptionPathsAnalysis { get; init; } = FlowPointsToAnalysisOptions.Fast.ExceptionPathsAnalysis;

        public static ProjectAnalyzerConfiguration Default { get; } = new();

        public ProjectAnalyzerConfiguration Normalize()
        {
            var normalizedCallChain = Math.Max(0, MaxInterproceduralCallChainLength);
            var normalizedLambdaDepth = Math.Max(0, MaxInterproceduralLambdaOrLocalFunctionDepth);

            var normalizedFeatures = ValueContentCopyAnalysisFeatures ?? Array.Empty<string>();

            if (normalizedCallChain == MaxInterproceduralCallChainLength &&
                normalizedLambdaDepth == MaxInterproceduralLambdaOrLocalFunctionDepth &&
                ReferenceEquals(ValueContentCopyAnalysisFeatures, normalizedFeatures))
            {
                return this;
            }

            return this with
            {
                MaxInterproceduralCallChainLength = normalizedCallChain,
                MaxInterproceduralLambdaOrLocalFunctionDepth = normalizedLambdaDepth,
                ValueContentCopyAnalysisFeatures = normalizedFeatures
            };
        }

        public InterproceduralSettings ToInterproceduralSettings()
            => new(
                InterproceduralAnalysisKind,
                MaxInterproceduralCallChainLength,
                MaxInterproceduralLambdaOrLocalFunctionDepth);

        public FlowPointsToAnalysisOptions GetPointsToAnalysisOptions(FlowPointsToPrecision? precision = null)
        {
            var effectivePrecision = precision ?? DefaultPointsToPrecision;
            if (effectivePrecision == FlowPointsToPrecision.HighPrecision)
            {
                return FlowPointsToAnalysisOptions.HighPrecision;
            }

            return new FlowPointsToAnalysisOptions(
                PointsToAnalysisKind,
                PerformCopyAnalysis,
                PessimisticAnalysis,
                ExceptionPathsAnalysis);
        }
    }

    private FlowPointsToAnalysisOptions EnsureCopyAnalysisOption(
        FlowPointsToAnalysisOptions options,
        string? feature)
    {
        if (ShouldPerformCopyAnalysis(feature) && !options.PerformCopyAnalysis)
        {
            return options with { PerformCopyAnalysis = true };
        }

        return options;
    }

    private bool ShouldPerformCopyAnalysis(string? feature)
    {
        if (_configuration.EnableValueContentCopyAnalysis)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(feature))
        {
            return false;
        }

        return _valueContentCopyAnalysisFeatures.Contains(feature);
    }

    private static bool IsConfigurationType(string? typeName)
        => !string.IsNullOrWhiteSpace(typeName)
            && typeName!.Contains("IConfiguration", StringComparison.Ordinal);

    private ConfigurationUsage? TryCaptureConfigurationUsage(
        MemberAccessExpressionSyntax memberAccess,
        InvocationExpressionSyntax invocation,
        string configurationType,
        SyntaxTree tree,
        SemanticModel? model = null,
        FlowValueContentFacade? valueContent = null)
    {
        var methodName = GetMemberName(memberAccess.Name);
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
        var key = argument is null ? null : ExtractConfigurationKey(argument, model, valueContent);
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var line = GetLineNumber(tree, invocation);
        var filePath = GetRelativePath(tree.FilePath);
        return new ConfigurationUsage(configurationType, methodName!, key, line, filePath);
    }

    private ConfigurationUsage? TryCaptureConfigurationIndexer(
        ElementAccessExpressionSyntax elementAccess,
        string configurationType,
        SyntaxTree tree,
        SemanticModel? model = null,
        FlowValueContentFacade? valueContent = null)
    {
        var argument = elementAccess.ArgumentList.Arguments.FirstOrDefault()?.Expression;
        var key = argument is null ? null : ExtractConfigurationKey(argument, model, valueContent);
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var line = GetLineNumber(tree, elementAccess);
        var filePath = GetRelativePath(tree.FilePath);
        return new ConfigurationUsage(configurationType, "indexer", key, line, filePath);
    }

    private string? ExtractConfigurationKey(
        ExpressionSyntax expression,
        SemanticModel? model,
        FlowValueContentFacade? valueContent)
    {
        if (model is not null && valueContent is not null)
        {
            try
            {
                var operation = model.GetOperation(expression);
                if (operation is not null)
                {
                    foreach (var candidate in valueContent.EnumerateContentCandidates(operation))
                    {
                        var literal = candidate.TryGetLiteralText();
                        if (!string.IsNullOrWhiteSpace(literal))
                        {
                            return literal;
                        }

                        var placeholder = candidate.ToDisplayString();
                        if (!string.IsNullOrWhiteSpace(placeholder))
                        {
                            return placeholder;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Ignore semantic model failures; fall back to syntax heuristics.
            }
        }

        return ExtractConfigurationKeyFromSyntax(expression);
    }

    private static string? ExtractConfigurationKeyFromSyntax(ExpressionSyntax expression)
    {
        return expression switch
        {
            LiteralExpressionSyntax literal when literal.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression)
                => literal.Token.ValueText,
            InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "nameof" }, ArgumentList.Arguments.Count: > 0 } nameofInvocation
                => nameofInvocation.ArgumentList.Arguments[0].Expression switch
                {
                    IdentifierNameSyntax identifier => identifier.Identifier.Text,
                    _ => null
                },
            _ => null
        };
    }

    private string EnsureConfigurationNode(string key)
    {
        var id = StableId.For("config.value", key, "configuration", key);
        if (_nodes.ContainsKey(id))
        {
            return id;
        }

        if (_configurationValues.TryGetValue(key, out var configuration))
        {
            var props = new Dictionary<string, object>
            {
                ["value"] = configuration.Value
            };

            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "config.value",
                Name = key,
                Fqdn = key,
                Assembly = "configuration",
                Project = string.Empty,
                FilePath = configuration.FilePath,
                Span = configuration.Span,
                SymbolId = key,
                Tags = new[] { "configuration", "infra" },
                Props = props
            };
        }
        else
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "config.value",
                Name = key,
                Fqdn = key,
                Assembly = "configuration",
                Project = string.Empty,
                FilePath = string.Empty,
                Span = null,
                SymbolId = key,
                Tags = new[] { "configuration", "infra" }
            };
        }

        return id;
    }

    private void EmitConfigurationEdges(string ownerId, IEnumerable<ConfigurationUsage> usages)
    {
        var comparer = new ConfigurationUsageComparer();
        var seen = new HashSet<(string Key, string Accessor, string FilePath)>(comparer);

        foreach (var usage in usages
            .Where(u => !string.IsNullOrWhiteSpace(u.Key))
            .OrderBy(u => u.Line))
        {
            var accessor = usage.Accessor ?? string.Empty;
            var signature = (usage.Key!, accessor, usage.FilePath);
            if (!seen.Add(signature))
            {
                continue;
            }

            var configId = EnsureConfigurationNode(usage.Key!);
            var props = new Dictionary<string, object>
            {
                ["key"] = usage.Key!,
                ["accessor"] = accessor
            };

            if (_configurationValues.TryGetValue(usage.Key!, out var configuration))
            {
                props["value"] = configuration.Value;
                props["source_file"] = configuration.FilePath;
            }

            _edges.Add(new GraphEdge
            {
                From = ownerId,
                To = configId,
                Kind = "uses_configuration",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "configuration.access",
                    Location = new GraphLocation { File = usage.FilePath, Line = usage.Line }
                },
                Props = props,
                Evidence = CreateEvidence(usage.FilePath, usage.Line)
            });
        }
    }

    private static class FlowAnalysisFeature
    {
        public const string Controllers = "controllers";
        public const string Http = "http";
        public const string Mapping = "mapping";
        public const string Messaging = "messaging";
        public const string Notifications = "notifications";
        public const string DomainEvents = "domain-events";
        public const string Services = "services";
        public const string Cqrs = "cqrs";
        public const string Pipelines = "pipelines";
    }

    private sealed class ConfigurationUsageComparer : IEqualityComparer<(string Key, string Accessor, string FilePath)>
    {
        public bool Equals((string Key, string Accessor, string FilePath) x, (string Key, string Accessor, string FilePath) y)
            => string.Equals(x.Key, y.Key, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.Accessor, y.Accessor, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.FilePath, y.FilePath, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode((string Key, string Accessor, string FilePath) obj)
        {
            var keyHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Key);
            var accessorHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Accessor);
            var fileHash = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.FilePath);
            return HashCode.Combine(keyHash, accessorHash, fileHash);
        }
    }
}
