using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Outputs;

internal sealed class FlowGraphIndex
{
    private FlowGraphIndex(
        GraphDocument document,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> nodesByFqdn,
        IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> nodesByName,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup)
    {
        Document = document;
        NodesById = nodesById;
        EdgesByFrom = edgesByFrom;
        NodesByFqdn = nodesByFqdn;
        NodesByName = nodesByName;
        MapLookup = mapLookup;
    }

    public GraphDocument Document { get; }
    public IReadOnlyDictionary<string, GraphNode> NodesById { get; }
    public IReadOnlyDictionary<string, List<GraphEdge>> EdgesByFrom { get; }
    public IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> NodesByFqdn { get; }
    public IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> NodesByName { get; }
    public IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> MapLookup { get; }

    public static FlowGraphIndex Build(GraphDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var nodesById = new Dictionary<string, GraphNode>(document.Nodes.Count, StringComparer.Ordinal);
        foreach (var node in document.Nodes)
        {
            if (string.IsNullOrWhiteSpace(node.Id))
            {
                continue;
            }

            nodesById[node.Id] = node;
        }

        var edgesByFrom = new Dictionary<string, List<GraphEdge>>(StringComparer.Ordinal);
        foreach (var edge in document.Edges)
        {
            if (string.IsNullOrWhiteSpace(edge.From))
            {
                continue;
            }

            if (!edgesByFrom.TryGetValue(edge.From, out var list))
            {
                list = new List<GraphEdge>(4);
                edgesByFrom[edge.From] = list;
            }

            list.Add(edge);
        }

        var nodesByFqdnLookup = new Dictionary<string, List<GraphNode>>(StringComparer.OrdinalIgnoreCase);
        var nodesByNameLookup = new Dictionary<string, List<GraphNode>>(StringComparer.OrdinalIgnoreCase);

        foreach (var node in document.Nodes)
        {
            if (!string.IsNullOrWhiteSpace(node.Fqdn))
            {
                if (!nodesByFqdnLookup.TryGetValue(node.Fqdn!, out var list))
                {
                    list = new List<GraphNode>(2);
                    nodesByFqdnLookup[node.Fqdn!] = list;
                }

                list.Add(node);
            }

            if (!string.IsNullOrWhiteSpace(node.Name))
            {
                if (!nodesByNameLookup.TryGetValue(node.Name!, out var list))
                {
                    list = new List<GraphNode>(2);
                    nodesByNameLookup[node.Name!] = list;
                }

                list.Add(node);
            }
        }

        var nodesByFqdn = nodesByFqdnLookup.ToDictionary(
            pair => pair.Key,
            pair => (IReadOnlyList<GraphNode>)pair.Value,
            StringComparer.OrdinalIgnoreCase);

        var nodesByName = nodesByNameLookup.ToDictionary(
            pair => pair.Key,
            pair => (IReadOnlyList<GraphNode>)pair.Value,
            StringComparer.OrdinalIgnoreCase);

        var mapLookup = BuildMapLookup(document);

        return new FlowGraphIndex(document, nodesById, edgesByFrom, nodesByFqdn, nodesByName, mapLookup);
    }

    private static IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> BuildMapLookup(GraphDocument document)
    {
        var lookup = new Dictionary<(string Source, string Destination), List<GraphNode>>();

        foreach (var node in document.Nodes.Where(static n => string.Equals(n.Type, "mapping.automapper.map", StringComparison.Ordinal)))
        {
            var source = node.Props is { } props && props.TryGetValue("source_type", out var sourceValue)
                ? Utilities.GetSimpleType(sourceValue?.ToString())
                : string.Empty;

            var destination = node.Props is { } props2 && props2.TryGetValue("destination_type", out var destinationValue)
                ? Utilities.GetSimpleType(destinationValue?.ToString())
                : string.Empty;

            if (string.IsNullOrWhiteSpace(destination))
            {
                continue;
            }

            var key = (source, destination);
            if (!lookup.TryGetValue(key, out var list))
            {
                list = new List<GraphNode>();
                lookup[key] = list;
            }

            list.Add(node);
        }

        return lookup;
    }
}
