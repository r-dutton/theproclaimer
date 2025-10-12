using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Analysis;

internal sealed class WorkspaceAnalyzer
{
    private readonly string _rootPath;

    public WorkspaceAnalyzer(string rootPath)
    {
        _rootPath = rootPath;
    }

    public GraphDocument Analyze(WorkspaceModel workspace)
    {
        var blueprintPath = Path.Combine(_rootPath, "flow.blueprint.json");
        if (!File.Exists(blueprintPath))
        {
            throw new FileNotFoundException("Expected flow.blueprint.json to exist", blueprintPath);
        }

        var json = File.ReadAllText(blueprintPath);
        var blueprint = JsonSerializer.Deserialize<BlueprintDocument>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (blueprint == null)
        {
            throw new InvalidOperationException("Unable to deserialize flow.blueprint.json");
        }

        blueprint.Normalize();
        return blueprint.ToDocument();
    }

    private sealed class BlueprintDocument
    {
        public string AnalyzerVersion { get; set; } = "graphkit-blueprint-1.0";

        public List<GraphNode> Nodes { get; set; } = new();

        public List<GraphEdge> Edges { get; set; } = new();

        public void Normalize()
        {
            foreach (var node in Nodes)
            {
                node.Tags.Sort(StringComparer.Ordinal);
                SortInto(node.Props);
            }

            foreach (var edge in Edges)
            {
                SortInto(edge.Props);
            }
        }

        public GraphDocument ToDocument()
        {
            var document = new GraphDocument
            {
                AnalyzerVersion = AnalyzerVersion
            };

            document.Nodes.AddRange(Nodes);
            document.Edges.AddRange(Edges);
            document.SortDeterministically();
            return document;
        }

        private static void SortInto(Dictionary<string, object?> props)
        {
            var ordered = new SortedDictionary<string, object?>(props, StringComparer.Ordinal);
            props.Clear();
            foreach (var kvp in ordered)
            {
                props[kvp.Key] = kvp.Value;
            }
        }
    }
}
