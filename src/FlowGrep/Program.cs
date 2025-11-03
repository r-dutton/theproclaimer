using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using FlowGrep.Options;
using GraphKit;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Outputs.Abstractions;
using GraphKit.Outputs.Facts;
using GraphKit.Outputs.FlowBuilder;
using GraphKit.Outputs.Legacy;
using GraphKit.Outputs.Narrative;

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
    }
}

renderStyle = renderStyle.Equals("graph", StringComparison.OrdinalIgnoreCase)
    ? "graph"
    : "narrative";

var generator = new GraphGenerator();
var result = await generator.GenerateAsync(new GraphGenerationOptions(
    workspace,
    renderOptions.OutputPath,
    solutions.Count > 0 ? solutions : null));
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
    if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
    {
        var flows = graph.Nodes
            .Where(predicate)
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
                    .ToArray()
            })
            .ToArray();

        flow = JsonSerializer.Serialize(new { flows }, new JsonSerializerOptions { WriteIndented = true });
    }
    else
    {
        flow = FlowBuilderMarkdown.Render(graph, predicate);
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
