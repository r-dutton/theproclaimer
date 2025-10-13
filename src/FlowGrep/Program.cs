using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit;
using GraphKit.Graph;
using GraphKit.Outputs;

var argsList = args.ToList();
string workspace = Environment.CurrentDirectory;
string output = "out";
string? textFilter = null;
HashSet<string>? tagFilter = null;
string format = "md";
string? flowFilter = null;

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
            flowFilter = argsList[++i];
            break;
    }
}

var generator = new GraphGenerator();
var document = await generator.GenerateAsync(new GraphGenerationOptions(workspace, output));

if (!string.IsNullOrWhiteSpace(flowFilter))
{
    var flow = FlowBuilder.BuildFlows(document, controller =>
        controller.Name.Contains(flowFilter, StringComparison.OrdinalIgnoreCase) ||
        controller.Fqdn.Contains(flowFilter, StringComparison.OrdinalIgnoreCase) ||
        (controller.Props is { } props && props.Values.Any(value => value?.ToString()?.Contains(flowFilter, StringComparison.OrdinalIgnoreCase) == true)));

    if (string.IsNullOrWhiteSpace(flow))
    {
        Console.WriteLine($"No matching flows found for '{flowFilter}'.");
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
