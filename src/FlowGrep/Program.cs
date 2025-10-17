using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Workspace;

var argsList = args.ToList();
string workspace = Environment.CurrentDirectory;
string output = "out";
string? textFilter = null;
HashSet<string>? tagFilter = null;
string format = "md";
var flowPatterns = new List<string>();
var solutions = new List<string>();
int? maxDepth = null;

for (int i = 0; i < argsList.Count; i++)
{
    switch (argsList[i])
    {
        case "--workspace":
            workspace = Path.GetFullPath(argsList[++i]);
            break;
        case "--write-out":
            output = argsList[++i];
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

var generator = new GraphGenerator();
var document = await generator.GenerateAsync(new GraphGenerationOptions(
    workspace,
    output,
    solutions.Count > 0 ? solutions : null));
var workspaceIndex = FlowWorkspaceIndex.Load(workspace);

if (flowPatterns.Count > 0)
{
    var predicate = FlowFilter.BuildPredicate(flowPatterns);
    var flow = FlowBuilder.BuildFlows(document, predicate, workspaceIndex, maxDepth);

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
        var json = System.Text.Json.JsonSerializer.Serialize(candidates, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
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
    Console.WriteLine($"Graph generated at {Path.Combine(output, "graph.json")}");
}
