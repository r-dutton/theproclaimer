using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Analyzers;

internal static class MessageLinker
{
    /// <summary>
    /// Synthetic linking pass that connects message publishers to downstream handlers when the published
    /// contract type matches a handler's notification/domain event contract. This enables cross-solution flows
    /// when multiple analyses are merged.
    /// </summary>
    public static void EmitMessageContractLinks(
        ConcurrentDictionary<string, GraphNode> nodes,
        ConcurrentBag<GraphEdge> edges,
        FlowWorkspaceIndex? workspace)
    {
        try
        {
            if (nodes.IsEmpty || edges.IsEmpty)
            {
                return;
            }

            var nodesById = nodes.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.OrdinalIgnoreCase);

            var contractConsumers = BuildContractConsumerMap(edges, nodesById);
            if (contractConsumers.Count == 0)
            {
                return;
            }

            // Seed from existing linker edges to avoid duplicates on re-run
            var existingLinks = new HashSet<string>(
                edges.Where(e => (string.Equals(e.Kind, "processed_by", StringComparison.OrdinalIgnoreCase) || string.Equals(e.Kind, "calls", StringComparison.OrdinalIgnoreCase)) &&
                                  e.Props is { } props && props.TryGetValue("provenance", out var provenance) &&
                                  string.Equals(provenance?.ToString(), "Linker", StringComparison.OrdinalIgnoreCase))
                     .Select(e => $"{e.From}|{e.To}"),
                StringComparer.OrdinalIgnoreCase);

            foreach (var edge in edges.Where(e => string.Equals(e.Kind, "produces_event", StringComparison.OrdinalIgnoreCase)))
            {
                if (!nodesById.TryGetValue(edge.From, out var publisherNode) ||
                    !string.Equals(publisherNode.Type, "message.publisher", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!nodesById.TryGetValue(edge.To, out var contractNode) ||
                    !string.Equals(contractNode.Type, "message.contract", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var contractFqdn = contractNode.Fqdn;
                if (string.IsNullOrWhiteSpace(contractFqdn))
                {
                    continue;
                }

                if (!contractConsumers.TryGetValue(contractFqdn, out var consumers))
                {
                    continue;
                }

                foreach (var consumerId in consumers)
                {
                    var key = $"{edge.From}|{consumerId}";
                    if (!existingLinks.Add(key))
                    {
                        continue;
                    }

                    var props = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["contract"] = contractFqdn,
                        ["provenance"] = "Linker",
                        ["confidence"] = "High"
                    };

                    edges.Add(new GraphEdge
                    {
                        From = edge.From,
                        To = consumerId,
                        Kind = "processed_by",
                        Source = "synthetic",
                        Confidence = 0.85,
                        Transform = new GraphTransform
                        {
                            Type = "message.linker"
                        },
                        Props = props
                    });
                }
            }

            _ = workspace; // Reserved for future cross-solution contract enrichment.
        }
        catch
        {
            // Swallow synthetic pass errors so they do not break graph generation.
        }
    }

    private static Dictionary<string, List<string>> BuildContractConsumerMap(
        IEnumerable<GraphEdge> edges,
        IDictionary<string, GraphNode> nodes)
    {
        var map = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var edge in edges)
        {
            if (!string.Equals(edge.Kind, "handled_by", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!nodes.TryGetValue(edge.From, out var contractNode))
            {
                continue;
            }

            if (!string.Equals(contractNode.Type, "cqrs.notification", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contractNode.Type, "domain.event", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(contractNode.Fqdn))
            {
                continue;
            }

            if (!nodes.ContainsKey(edge.To))
            {
                continue;
            }

            if (!map.TryGetValue(contractNode.Fqdn, out var list))
            {
                list = new List<string>();
                map[contractNode.Fqdn] = list;
            }

            list.Add(edge.To);
        }

        return map;
    }
}
