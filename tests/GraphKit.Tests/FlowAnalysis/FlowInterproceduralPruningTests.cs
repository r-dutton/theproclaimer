using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Analyzers;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.Operations;
using Xunit;

namespace GraphKit.Tests.Flow;

public sealed class FlowInterproceduralPruningTests
{
    private static readonly MetadataReference CorlibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

    [Fact]
    public void ValueContentFacade_PrunesLoggerAndTelemetryCallChains()
    {
        var (compilation, tree) = CreateSampleCompilation();
        var settings = ProjectAnalyzer.ProjectAnalyzerConfiguration.Default.ToInterproceduralSettings();

        FlowCallsitePredicate predicate = invocation => !IsInfrastructureInvocation(invocation);
        var valueContent = new FlowValueContentFacade(settings, predicate, FlowPointsToAnalysisOptions.Fast);

        var model = compilation.GetSemanticModel(tree);
        var root = tree.GetRoot();

        var allowedInvocation = GetInvocationOperation(model, root, "ComposeAllowedMessage");
        var telemetryInvocation = GetInvocationOperation(model, root, "ComposeTelemetryMessage");
        var loggerInvocation = GetInvocationOperation(model, root, "ComposeLoggerMessage");

        var allowedValue = valueContent.TryGetStringValue(allowedInvocation);
        Assert.Equal("allowed-event", allowedValue);

        Assert.Null(valueContent.TryGetStringValue(telemetryInvocation));
        Assert.Null(valueContent.TryGetStringValue(loggerInvocation));
    }

    [Fact]
    public void FlowFacades_RecordConsistentPrunedCallsites()
    {
        var (compilation, tree) = CreateSampleCompilation();
        var settings = new InterproceduralSettings(InterproceduralAnalysisKind.ContextSensitive, 4, 2);

        var pointsToPruned = new List<string>();
        FlowCallsitePredicate pointsToPredicate = invocation =>
        {
            if (IsInfrastructureInvocation(invocation))
            {
                pointsToPruned.Add(invocation.TargetMethod.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
                return false;
            }

            return true;
        };

        var valueContentPruned = new List<string>();
        FlowCallsitePredicate valueContentPredicate = invocation =>
        {
            if (IsInfrastructureInvocation(invocation))
            {
                valueContentPruned.Add(invocation.TargetMethod.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
                return false;
            }

            return true;
        };

        var pointsTo = new FlowPointsToFacade(settings, pointsToPredicate, FlowPointsToAnalysisOptions.Fast);
        var valueContent = new FlowValueContentFacade(settings, valueContentPredicate, FlowPointsToAnalysisOptions.Fast);

        var model = compilation.GetSemanticModel(tree);
        var root = tree.GetRoot();

        var telemetryInvocation = GetInvocationOperation(model, root, "ComposeTelemetryMessage");
        var loggerInvocation = GetInvocationOperation(model, root, "ComposeLoggerMessage");

        pointsTo.TryGetAbstractValue(telemetryInvocation, out _);
        pointsTo.TryGetAbstractValue(loggerInvocation, out _);

        valueContent.TryGetStringValue(telemetryInvocation);
        valueContent.TryGetStringValue(loggerInvocation);

        var expected = new[]
        {
            "Sample.TelemetryClient.ComposeTelemetryMessage()",
            "Sample.LoggerClient.ComposeLoggerMessage()"
        };

        Assert.Equal(expected, pointsToPruned);
        Assert.Equal(expected, valueContentPruned);
    }

    private static (Compilation Compilation, SyntaxTree Tree) CreateSampleCompilation()
    {
        const string source = @"namespace Sample
{
    public sealed class TelemetryClient
    {
        public string ComposeTelemetryMessage() => ComposeCore();
        private string ComposeCore() => \"telemetry-event\";
    }

    public sealed class LoggerClient
    {
        public string ComposeLoggerMessage() => ComposeCore();
        private string ComposeCore() => \"logger-event\";
    }

    public sealed class MessageComposer
    {
        public string ComposeAllowedMessage() => ComposeCore();
        private string ComposeCore() => \"allowed-event\";
    }

    public sealed class MessageService
    {
        private readonly TelemetryClient _telemetry = new();
        private readonly LoggerClient _logger = new();
        private readonly MessageComposer _composer = new();

        public string BuildAllowed() => _composer.ComposeAllowedMessage();
        public string BuildTelemetry() => _telemetry.ComposeTelemetryMessage();
        public string BuildLogger() => _logger.ComposeLoggerMessage();
    }
}";

        var tree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.CSharp12));
        var compilation = CSharpCompilation.Create(
            assemblyName: "Sample",
            syntaxTrees: new[] { tree },
            references: new[] { CorlibReference },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return (compilation, tree);
    }

    private static IInvocationOperation GetInvocationOperation(SemanticModel model, SyntaxNode root, string targetMethodName)
    {
        var invocationSyntax = root.DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .First(node => model.GetOperation(node) is IInvocationOperation operation && operation.TargetMethod.Name == targetMethodName);

        return (IInvocationOperation)model.GetOperation(invocationSyntax)!;
    }

    private static bool IsInfrastructureInvocation(IInvocationOperation invocation)
    {
        var containingTypeName = invocation.TargetMethod.ContainingType?.Name ?? string.Empty;
        return containingTypeName.Contains("Telemetry", StringComparison.OrdinalIgnoreCase)
            || containingTypeName.Contains("Logger", StringComparison.OrdinalIgnoreCase);
    }
}
