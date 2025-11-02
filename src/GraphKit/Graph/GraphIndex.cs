using System;
using System.Collections.Generic;

namespace GraphKit.Graph;

using System.Runtime.CompilerServices;

/// <summary>
/// Internal int-indexed projection of GraphDocument for fast traversal.
/// </summary>
public sealed class GraphIndex
{
    public readonly GraphNode[] Nodes;                 // index -> node
    public readonly Dictionary<string,int> IdxById;    // nodeId -> index
    public readonly List<int>[] Adj;                   // index -> [to...]

    private GraphIndex(GraphNode[] nodes, Dictionary<string,int> idxById, List<int>[] adj)
        => (Nodes, IdxById, Adj) = (nodes, idxById, adj);

    public static GraphIndex Build(GraphDocument doc)
    {
        var n = doc.Nodes.Count;
        var nodes = new GraphNode[n];
        var idxById = new Dictionary<string,int>(n, StringComparer.Ordinal);
        for (int i = 0; i < n; i++)
        {
            var node = doc.Nodes[i];
            nodes[i] = node;
            idxById[node.Id] = i;
        }

        var adj = new List<int>[n];
        // Deduplicate edges (from|to|kind|verb|route)
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var e in doc.Edges)
        {
            if (!idxById.TryGetValue(e.From, out var f) || !idxById.TryGetValue(e.To, out var t)) continue;
            var verb  = e.Props is { } && e.Props.TryGetValue("verb",  out var v) ? v?.ToString() : "";
            var route = e.Props is { } && e.Props.TryGetValue("route", out var r) ? r?.ToString() : "";
            var key = $"{f}|{t}|{e.Kind}|{verb}|{route}";
            if (!seen.Add(key)) continue;
            (adj[f] ??= new List<int>(4)).Add(t);
        }

        return new GraphIndex(nodes, idxById, adj);
    }
}
