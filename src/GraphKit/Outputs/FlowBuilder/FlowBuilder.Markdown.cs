using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphKit.Outputs.Abstractions;

namespace GraphKit.Outputs.FlowBuilder
{
    public static class FlowBuilderMarkdown
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string Render(IGraphProvider provider, Func<FlowNode, bool>? includeFlow = null)
        {
            var graph = FlowBuilderCore.BuildGraph(provider);
            return Render(graph, includeFlow);
        }

        public static string Render(FlowGraph graph, Func<FlowNode, bool>? includeFlow = null)
        {
            var builder = new StringBuilder();

            var flows = graph.Nodes
                .Where(static n => n.Type.StartsWith("endpoint.", StringComparison.OrdinalIgnoreCase))
                .OrderBy(static n => n.DisplayName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var endpoint in flows)
            {
                if (includeFlow is not null && !includeFlow(endpoint))
                {
                    continue;
                }

                AppendEndpoint(builder, graph, endpoint);
                builder.AppendLine();
            }

            if (builder.Length == 0)
            {
                AppendNodeListing(builder, graph);
            }

            return builder.ToString().Trim();
        }

        private static void AppendEndpoint(StringBuilder builder, FlowGraph graph, FlowNode endpoint)
        {
            builder.AppendLine($"## {endpoint.DisplayName}");
            builder.AppendLine();

            AppendNodeMetadata(builder, endpoint);

            var outgoing = graph.GetOutgoingEdges(endpoint.Id)
                .OrderBy(static e => e.Kind, StringComparer.OrdinalIgnoreCase)
                .ThenBy(static e => e.ToId, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (outgoing.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("### Outgoing");
                foreach (var edge in outgoing)
                {
                    AppendEdge(builder, graph, edge);
                }
            }

            var incoming = graph.GetIncomingEdges(endpoint.Id)
                .OrderBy(static e => e.Kind, StringComparer.OrdinalIgnoreCase)
                .ThenBy(static e => e.FromId, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (incoming.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("### Incoming");
                foreach (var edge in incoming)
                {
                    AppendEdge(builder, graph, edge, incoming: true);
                }
            }
        }

        private static void AppendNodeMetadata(StringBuilder builder, FlowNode node)
        {
            builder.AppendLine($"- Id: `{node.Id}`");
            builder.AppendLine($"- Type: `{node.Type}`");

            if (node.TryGetValue("route", out var route) && route is not null)
            {
                builder.AppendLine($"- Route: `{route}`");
            }

            if (node.TryGetValue("verb", out var verb) && verb is not null)
            {
                builder.AppendLine($"- Verb: `{verb}`");
            }

            var tags = node.GetStringList("tags");
            if (tags.Count > 0)
            {
                builder.AppendLine($"- Tags: {string.Join(", ", tags.Select(static t => $"`{t}`"))}");
            }
        }

        private static void AppendEdge(StringBuilder builder, FlowGraph graph, FlowEdge edge, bool incoming = false)
        {
            var direction = incoming ? edge.FromId : edge.ToId;
            var relation = incoming ? "←" : "→";
            var targetDisplay = direction;
            if (graph.TryGetNode(direction, out var targetNode))
            {
                targetDisplay = $"{targetNode.DisplayName} ({targetNode.Type})";
            }

            builder.Append("- ");
            if (!incoming)
            {
                builder.Append("**");
            }
            builder.Append(edge.Kind);
            if (!incoming)
            {
                builder.Append("**");
            }
            builder.Append(' ');
            builder.Append(relation);
            builder.Append(' ');
            builder.AppendLine(targetDisplay);

            if (edge.Props.Count > 0)
            {
                foreach (var kv in edge.Props.OrderBy(static kvp => kvp.Key, StringComparer.OrdinalIgnoreCase))
                {
                    builder.Append("  - ");
                    builder.Append(kv.Key);
                    builder.Append(": ");
                    builder.AppendLine(FormatValue(kv.Value));
                }
            }
        }

        private static void AppendNodeListing(StringBuilder builder, FlowGraph graph)
        {
            builder.AppendLine("# Graph Nodes");
            builder.AppendLine();

            foreach (var group in graph.Nodes
                         .GroupBy(static n => n.Type, StringComparer.OrdinalIgnoreCase)
                         .OrderBy(static g => g.Key, StringComparer.OrdinalIgnoreCase))
            {
                builder.AppendLine($"## {group.Key}");
                foreach (var node in group.OrderBy(static n => n.DisplayName, StringComparer.OrdinalIgnoreCase))
                {
                    builder.AppendLine($"- {node.DisplayName} (`{node.Id}`)");
                }

                builder.AppendLine();
            }
        }

        private static string FormatValue(object? value)
        {
            if (value is null)
            {
                return "null";
            }

            if (value is string s)
            {
                return $"`{s}`";
            }

            if (value is bool b)
            {
                return b ? "true" : "false";
            }

            if (value is IEnumerable<object?> enumerable && value is not IReadOnlyDictionary<string, object?>)
            {
                var parts = enumerable
                    .Select(static v => v is null ? "null" : v is string str ? $"`{str}`" : v.ToString())
                    .Where(static v => !string.IsNullOrWhiteSpace(v))
                    .ToArray();
                return parts.Length > 0 ? string.Join(", ", parts) : "[]";
            }

            return JsonSerializer.Serialize(value, JsonOptions);
        }
    }
}
