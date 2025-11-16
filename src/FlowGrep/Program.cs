using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using FlowGrep.Options;
using GraphKit;
using GraphKit.Analyzers;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Outputs.Abstractions;
using GraphKit.Outputs.Facts;
using GraphKit.Outputs.FlowBuilder;
using GraphKit.Outputs.Legacy;
using GraphKit.Outputs.Narrative;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using GraphKit.FlowAnalysis.Dependencies;

var argsList = args.ToList();
string workspace = Environment.CurrentDirectory;
var renderOptions = new RenderOptions();
string renderStyle = "narrative";
string? textFilter = null;
HashSet<string>? tagFilter = null;
string format = "md";
var flowPatterns = new List<string>();
var solutions = new List<string>();
bool quiet = argsList.Remove("--quiet");
bool noMsg = argsList.Remove("--no-msg");
bool noDb  = argsList.Remove("--no-db");
bool noCache = argsList.Remove("--no-cache");
bool turbo = argsList.Remove("--turbo") || argsList.Remove("-t"); // preserved for compatibility, currently no effect
// Workspace mode flags:
// - Roslyn/MSBuild is now the default when available.
// - --legacy-workspace / --no-roslyn can force the legacy loader.
// - --use-roslyn is retained as a deprecated alias for compatibility.
bool disableRoslyn = argsList.Remove("--legacy-workspace") || argsList.Remove("--no-roslyn");
bool explicitUseRoslyn = argsList.Remove("--use-roslyn");
int? interprocCallChain = null;
int? interprocLambdaDepth = null;
InterproceduralAnalysisKind? interprocKind = null;
PointsToAnalysisKind? pointsToKind = null;
bool? pointsToCopyAnalysis = null;
bool? pointsToPessimisticAnalysis = null;
bool? pointsToExceptionPathsAnalysis = null;
FlowPointsToPrecision? pointsToPrecision = null;
if (argsList.Remove("--legacy"))
{
    renderOptions.Source = RenderSource.Legacy;
}
int? maxDepth = null; // parsed for compatibility

for (int i = 0; i < argsList.Count; i++)
{
    switch (argsList[i])
    {
        case "--workspace":
            workspace = Path.GetFullPath(argsList[++i]);
            break;
        case "--write-out":
            renderOptions.OutputPath = argsList[++i];
            break;
        case "--output":
            renderOptions.OutputPath = argsList[++i];
            break;
        case "--text":
            textFilter = argsList[++i];
            break;
        case "--tags":
            tagFilter = new HashSet<string>(argsList[++i].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), StringComparer.OrdinalIgnoreCase);
            break;
        case "--format":
            format = argsList[++i];
            break;
        case "--render-style":
            renderStyle = argsList[++i];
            break;
        case "--render-source":
            var sourceValue = argsList[++i];
            renderOptions.Source = sourceValue.Equals("legacy", StringComparison.OrdinalIgnoreCase)
                ? RenderSource.Legacy
                : RenderSource.Facts;
            break;
        case "--flow":
        case "--flows":
            flowPatterns.AddRange(argsList[++i].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            break;
        case "--solution":
            solutions.Add(argsList[++i]);
            break;
        case "--solutions":
            solutions.AddRange(argsList[++i].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            break;
        case "--max-depth":
            if (int.TryParse(argsList[++i], out var md) && md >= 0)
            {
                maxDepth = md;
            }
            break;
        case "--interproc-call-chain":
            if (int.TryParse(argsList[++i], out var callDepth) && callDepth >= 0)
            {
                interprocCallChain = callDepth;
            }
            else
            {
                Console.Error.WriteLine($"[warn] Ignoring invalid interproc call-chain length '{argsList[i]}'.");
            }
            break;
        case "--interproc-lambda-depth":
            if (int.TryParse(argsList[++i], out var lambdaDepth) && lambdaDepth >= 0)
            {
                interprocLambdaDepth = lambdaDepth;
            }
            else
            {
                Console.Error.WriteLine($"[warn] Ignoring invalid interproc lambda depth '{argsList[i]}'.");
            }
            break;
        case "--interproc-kind":
            var kindValue = argsList[++i];
            if (Enum.TryParse<InterproceduralAnalysisKind>(kindValue, ignoreCase: true, out var parsedKind))
            {
                interprocKind = parsedKind;
            }
            else
            {
                Console.Error.WriteLine($"[warn] Unknown interprocedural analysis kind '{kindValue}'.");
            }
            break;
        case "--points-to-kind":
            var pointsToKindValue = argsList[++i];
            if (Enum.TryParse<PointsToAnalysisKind>(pointsToKindValue, ignoreCase: true, out var parsedPointsToKind))
            {
                pointsToKind = parsedPointsToKind;
            }
            else
            {
                Console.Error.WriteLine($"[warn] Unknown points-to analysis kind '{pointsToKindValue}'.");
            }
            break;
        case "--points-to-copy-analysis":
            if (TryParseBooleanOption(argsList[++i], out var copyAnalysis))
            {
                pointsToCopyAnalysis = copyAnalysis;
            }
            else
            {
                Console.Error.WriteLine("[warn] Expected true/false after --points-to-copy-analysis.");
            }
            break;
        case "--points-to-pessimistic":
        case "--points-to-pessimistic-analysis":
            if (TryParseBooleanOption(argsList[++i], out var pessimistic))
            {
                pointsToPessimisticAnalysis = pessimistic;
            }
            else
            {
                Console.Error.WriteLine("[warn] Expected true/false after --points-to-pessimistic-analysis.");
            }
            break;
        case "--points-to-exception-paths":
        case "--points-to-exception-paths-analysis":
            if (TryParseBooleanOption(argsList[++i], out var exceptionPaths))
            {
                pointsToExceptionPathsAnalysis = exceptionPaths;
            }
            else
            {
                Console.Error.WriteLine("[warn] Expected true/false after --points-to-exception-paths.");
            }
            break;
        case "--points-to-precision":
            var precisionValue = argsList[++i];
            if (Enum.TryParse<FlowPointsToPrecision>(precisionValue, ignoreCase: true, out var parsedPrecision))
            {
                pointsToPrecision = parsedPrecision;
            }
            else
            {
                Console.Error.WriteLine($"[warn] Unknown points-to precision '{precisionValue}'.");
            }
            break;
    }
}

var requestedMermaid = format.Equals("mermaid", StringComparison.OrdinalIgnoreCase);
if (requestedMermaid)
{
    Console.Error.WriteLine("[warn] Mermaid output has been retired; defaulting to markdown.");
    format = "md";
}

renderStyle = renderStyle.Equals("graph", StringComparison.OrdinalIgnoreCase)
    ? "graph"
    : "narrative";

ProjectAnalyzer.ProjectAnalyzerConfiguration? analyzerConfiguration = null;
if (interprocCallChain is not null ||
    interprocLambdaDepth is not null ||
    interprocKind is not null ||
    pointsToKind is not null ||
    pointsToCopyAnalysis is not null ||
    pointsToPessimisticAnalysis is not null ||
    pointsToExceptionPathsAnalysis is not null ||
    pointsToPrecision is not null)
{
    var configuration = ProjectAnalyzer.ProjectAnalyzerConfiguration.Default;
    if (interprocCallChain is { } callChainValue)
    {
        configuration = configuration with { MaxInterproceduralCallChainLength = callChainValue };
    }

    if (interprocLambdaDepth is { } lambdaDepthValue)
    {
        configuration = configuration with { MaxInterproceduralLambdaOrLocalFunctionDepth = lambdaDepthValue };
    }

    if (interprocKind is { } kindValue)
    {
        configuration = configuration with { InterproceduralAnalysisKind = kindValue };
    }

    if (pointsToKind is { } pointsToKindValue)
    {
        configuration = configuration with { PointsToAnalysisKind = pointsToKindValue };
    }

    if (pointsToCopyAnalysis is { } copyAnalysisValue)
    {
        configuration = configuration with { PerformCopyAnalysis = copyAnalysisValue };
    }

    if (pointsToPessimisticAnalysis is { } pessimisticValue)
    {
        configuration = configuration with { PessimisticAnalysis = pessimisticValue };
    }

    if (pointsToExceptionPathsAnalysis is { } exceptionPathsValue)
    {
        configuration = configuration with { ExceptionPathsAnalysis = exceptionPathsValue };
    }

    if (pointsToPrecision is { } precisionValue)
    {
        configuration = configuration with { DefaultPointsToPrecision = precisionValue };
    }

    analyzerConfiguration = configuration.Normalize();
}

static bool TryParseBooleanOption(string value, out bool result)
{
    if (bool.TryParse(value, out result))
    {
        return true;
    }

    if (int.TryParse(value, out var numeric))
    {
        result = numeric != 0;
        return true;
    }

    result = false;
    return false;
}

MSBuildWorkspace? roslynWorkspace = null;
bool useRoslyn = !disableRoslyn;

try
{
    if (explicitUseRoslyn && disableRoslyn)
    {
        Console.Error.WriteLine("[warn] Both --use-roslyn and --legacy-workspace/--no-roslyn specified; preferring Roslyn/MSBuild.");
    }

    if (explicitUseRoslyn)
    {
        Console.Error.WriteLine("[warn] --use-roslyn is now the default; this flag is deprecated.");
        useRoslyn = true;
    }

    if (useRoslyn)
    {
        try
        {
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }

            roslynWorkspace = MSBuildWorkspace.Create();
            roslynWorkspace.WorkspaceFailed += (_, args) =>
            {
                var prefix = args.Diagnostic.Kind == WorkspaceDiagnosticKind.Warning ? "[roslyn][warn]" : "[roslyn][error]";
                var writer = args.Diagnostic.Kind == WorkspaceDiagnosticKind.Warning ? Console.Out : Console.Error;
                writer.WriteLine($"{prefix} {args.Diagnostic.Message}");
            };

            Console.Error.WriteLine("[graph] Workspace mode: Roslyn/MSBuild");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[roslyn][error] Failed to initialize MSBuild workspace ({ex.GetType().Name}: {ex.Message}). Falling back to legacy workspace.");
            Console.Error.WriteLine("[graph] Workspace mode: Legacy/Adhoc");
            useRoslyn = false;
            roslynWorkspace = null;
        }
    }
    else
    {
        Console.Error.WriteLine("[graph] Workspace mode: Legacy/Adhoc");
    }

    var generator = new GraphGenerator();
    var result = await generator.GenerateAsync(new GraphGenerationOptions(
        workspace,
        renderOptions.OutputPath,
        solutions.Count > 0 ? solutions : null,
        useRoslyn,
        roslynWorkspace,
        analyzerConfiguration));
    var document = result.Document;
    var factBag = result.Facts;
    var outputDirFull = Path.GetFullPath(Path.Combine(workspace, renderOptions.OutputPath));

    if (renderStyle == "narrative")
    {
        var narrativeOutputPath = Path.Combine(outputDirFull, "flow.md");
        LegacyNarrativeRenderer.Render(factBag, workspace, narrativeOutputPath);
    }

    if (flowPatterns.Count > 0)
    {
        var predicate = FlowFilter.BuildPredicate(flowPatterns);
        IGraphProvider provider = renderOptions.Source == RenderSource.Facts
            ? new FactsGraphProvider(factBag)
            : new LegacyGraphProvider(document.Nodes, document.Edges);
        string flow;

        var graph = FlowBuilderCore.BuildGraph(provider);
	    var narratives = LegacyNarrativeRenderer.Collect(factBag, workspace);
	    var narrativeLookup = narratives.ToDictionary(n => n.Endpoint.Id, StringComparer.Ordinal);
        var matchedNodes = graph.Nodes.Where(predicate).ToList();

        if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
        {
            var flows = matchedNodes
                .Select(node => new
                {
                    node.Id,
                    node.Type,
                    Props = node.Props,
                    Outgoing = graph.GetOutgoingEdges(node.Id)
                        .Select(edge => new
                        {
                            edge.Kind,
                            edge.ToId,
                            Props = edge.Props
                        })
                        .ToArray(),
                    Incoming = graph.GetIncomingEdges(node.Id)
                        .Select(edge => new
                        {
                            edge.Kind,
                            edge.FromId,
                            Props = edge.Props
                        })
                    .ToArray(),
                 	Narrative = narrativeLookup.TryGetValue(node.Id, out var entry) ? entry.Text : null
                })
                .ToArray();

            flow = JsonSerializer.Serialize(new { flows }, new JsonSerializerOptions { WriteIndented = true });
        }
        else
        {
            var matchedIds = new HashSet<string>(matchedNodes.Select(n => n.Id), StringComparer.Ordinal);
            var sb = new StringBuilder();
            var wroteAny = false;

            foreach (var entry in narratives)
            {
                if (matchedIds.Count > 0 && !matchedIds.Contains(entry.Endpoint.Id))
                {
                    continue;
                }

                sb.Append(entry.Text.TrimEnd());
                sb.AppendLine();
                sb.AppendLine();
                wroteAny = true;
                matchedIds.Remove(entry.Endpoint.Id);
            }

            if (matchedIds.Count > 0)
            {
                foreach (var node in matchedNodes)
                {
                    if (!matchedIds.Contains(node.Id))
                    {
                        continue;
                    }

                    var fallback = FlowBuilderMarkdown.Render(graph, n => string.Equals(n.Id, node.Id, StringComparison.Ordinal));
                    if (!string.IsNullOrWhiteSpace(fallback))
                    {
                        sb.Append(fallback.TrimEnd());
                        sb.AppendLine();
                        sb.AppendLine();
                        wroteAny = true;
                    }

                    matchedIds.Remove(node.Id);
                }
            }

            flow = wroteAny ? sb.ToString().TrimEnd() : string.Empty;
        }

        if (string.IsNullOrWhiteSpace(flow))
        {
            Console.WriteLine($"No matching flows found for patterns: {string.Join(", ", flowPatterns)}.");
        }
        else
        {
            Console.WriteLine(flow);
        }
    }
    else if (!string.IsNullOrWhiteSpace(textFilter) || (tagFilter is { Count: > 0 }))
    {
        IEnumerable<GraphNode> candidates = document.Nodes;
        if (!string.IsNullOrWhiteSpace(textFilter))
        {
            candidates = candidates.Where(n => n.Name.Contains(textFilter, StringComparison.OrdinalIgnoreCase) || n.Fqdn.Contains(textFilter, StringComparison.OrdinalIgnoreCase) || (n.Props is { } props && props.Values.Any(v => v?.ToString()?.Contains(textFilter, StringComparison.OrdinalIgnoreCase) == true)));
        }

        if (tagFilter is { Count: > 0 })
        {
            candidates = candidates.Where(n => n.Tags.Any(tagFilter.Contains));
        }

        if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
        {
            var json = JsonSerializer.Serialize(candidates, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
        else
        {
            foreach (var node in candidates.OrderBy(n => n.Fqdn, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- {node.Type}: {node.Fqdn} ({node.FilePath})");
            }
        }
    }
    else
    {
        if (renderStyle == "graph")
        {
            Console.WriteLine($"Graph generated at {Path.Combine(renderOptions.OutputPath, "graph.json")}");
        }
        else
        {
            var relativeNarrative = Path.Combine(renderOptions.OutputPath, "flow.md");
            Console.WriteLine($"Narrative generated at {relativeNarrative}");
        }
    }
}
finally
{
    roslynWorkspace?.Dispose();
}
