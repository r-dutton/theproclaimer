using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using GraphKit.Analysis;
using GraphKit.Graph;
using GraphKit.Output;

var arguments = ParseArguments(args);
var workspace = arguments.TryGetValue("workspace", out var workspacePath)
    ? Path.GetFullPath(workspacePath!)
    : Directory.GetCurrentDirectory();
var output = arguments.TryGetValue("write-out", out var outputPath)
    ? Path.GetFullPath(outputPath!)
    : Path.Combine(workspace, "out");

var pipeline = new GraphPipeline();
var document = pipeline.Build(new GraphPipelineOptions
{
    WorkspaceRoot = workspace
});

var writer = new GraphWriter();
writer.Write(document, output);

var textFilter = arguments.TryGetValue("text", out var text) ? text : null;
var tagFilter = arguments.TryGetValue("tags", out var tags) ? tags?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) : Array.Empty<string>();
var format = arguments.TryGetValue("format", out var fmt) ? fmt : "md";

if (!string.IsNullOrEmpty(textFilter) || (tagFilter?.Length ?? 0) > 0)
{
    var flows = FlowComputer.Compute(document, textFilter, tagFilter ?? Array.Empty<string>(), 8);
    if (flows.Count == 0)
    {
        Console.WriteLine("No flows matched the provided filters.");
    }
    else
    {
        var rendered = FlowRenderer.Render(flows, format);
        Console.WriteLine(rendered);
        var flowDir = Path.Combine(output, "flows");
        Directory.CreateDirectory(flowDir);
        var fileName = $"flows.{(string.IsNullOrEmpty(textFilter) ? "all" : textFilter!.ToLowerInvariant().Replace(' ', '.'))}.{string.Join(".", tagFilter ?? Array.Empty<string>()).ToLowerInvariant()}.{format}";
        File.WriteAllText(Path.Combine(flowDir, fileName), rendered);
    }
}

static Dictionary<string, string?> ParseArguments(string[] args)
{
    var dict = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
    for (var i = 0; i < args.Length; i++)
    {
        var arg = args[i];
        if (!arg.StartsWith("--", StringComparison.Ordinal))
        {
            continue;
        }

        var key = arg.TrimStart('-');
        string? value = null;
        if (i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
        {
            value = args[i + 1];
            i++;
        }

        dict[key] = value;
    }

    return dict;
}

static class FlowComputer
{
    public static List<List<GraphNode>> Compute(GraphDocument document, string? text, string[] tags, int maxDepth)
    {
        var matchingNodes = document.Nodes
            .Where(n => string.IsNullOrEmpty(text) || n.Name.Contains(text, StringComparison.OrdinalIgnoreCase) || n.Props.Values.Any(v => v?.ToString()?.Contains(text, StringComparison.OrdinalIgnoreCase) == true))
            .ToList();

        var adjacency = document.Edges
            .GroupBy(e => e.From)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);

        var result = new List<List<GraphNode>>();
        foreach (var start in matchingNodes)
        {
            var path = new List<GraphNode> { start };
            DepthFirst(document, adjacency, start, new HashSet<string>(StringComparer.Ordinal), path, result, maxDepth);
        }

        if (tags.Length == 0)
        {
            return result;
        }

        return result.Where(path => tags.All(tag => path.Any(node => node.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)))).ToList();
    }

    private static void DepthFirst(GraphDocument document, Dictionary<string, List<GraphEdge>> adjacency, GraphNode current, HashSet<string> visited, List<GraphNode> path, List<List<GraphNode>> result, int depthRemaining)
    {
        if (depthRemaining == 0)
        {
            result.Add(new List<GraphNode>(path));
            return;
        }

        if (!adjacency.TryGetValue(current.Id, out var edges))
        {
            result.Add(new List<GraphNode>(path));
            return;
        }

        foreach (var edge in edges)
        {
            var next = document.Nodes.FirstOrDefault(n => n.Id == edge.To);
            if (next == null)
            {
                continue;
            }

            var key = edge.From + "::" + edge.To;
            if (!visited.Add(key))
            {
                continue;
            }

            path.Add(next);
            DepthFirst(document, adjacency, next, visited, path, result, depthRemaining - 1);
            path.RemoveAt(path.Count - 1);
            visited.Remove(key);
        }
    }
}

static class FlowRenderer
{
    public static string Render(List<List<GraphNode>> flows, string format)
    {
        return format.ToLowerInvariant() switch
        {
            "json" => RenderJson(flows),
            _ => RenderMarkdown(flows)
        };
    }

    private static string RenderMarkdown(List<List<GraphNode>> flows)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < flows.Count; i++)
        {
            builder.AppendLine($"## Flow {i + 1}");
            foreach (var node in flows[i])
            {
                builder.AppendLine($"- {node.Type}: {node.Name} ({string.Join(",", node.Tags)})");
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static string RenderJson(List<List<GraphNode>> flows)
    {
        var payload = flows.Select(flow => flow.Select(node => new
        {
            node.Id,
            node.Name,
            node.Type,
            node.Tags
        }));
        return JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
    }
}
