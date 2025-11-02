using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphKit.Outputs.FlowBuilder
{
    public sealed class FlowGraph
    {
        private static readonly FlowEdge[] EmptyEdges = Array.Empty<FlowEdge>();
        private readonly IReadOnlyDictionary<string, FlowNode> _nodesById;
        private readonly IReadOnlyDictionary<string, IReadOnlyList<FlowEdge>> _edgesByFrom;
        private readonly IReadOnlyDictionary<string, IReadOnlyList<FlowEdge>> _edgesByTo;

        public FlowGraph(IEnumerable<FlowNode> nodes, IEnumerable<FlowEdge> edges)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (edges is null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            var nodeList = nodes.ToList();
            var edgeList = edges.ToList();

            _nodesById = new ReadOnlyDictionary<string, FlowNode>(
                nodeList.ToDictionary(static n => n.Id, static n => n, StringComparer.Ordinal));

            _edgesByFrom = new ReadOnlyDictionary<string, IReadOnlyList<FlowEdge>>(
                edgeList
                    .GroupBy(static e => e.FromId, StringComparer.Ordinal)
                    .ToDictionary(static g => g.Key, g => (IReadOnlyList<FlowEdge>)g.ToList(), StringComparer.Ordinal));

            _edgesByTo = new ReadOnlyDictionary<string, IReadOnlyList<FlowEdge>>(
                edgeList
                    .GroupBy(static e => e.ToId, StringComparer.Ordinal)
                    .ToDictionary(static g => g.Key, g => (IReadOnlyList<FlowEdge>)g.ToList(), StringComparer.Ordinal));

            Nodes = new ReadOnlyCollection<FlowNode>(nodeList);
            Edges = new ReadOnlyCollection<FlowEdge>(edgeList);
        }

        public IReadOnlyList<FlowNode> Nodes { get; }
        public IReadOnlyList<FlowEdge> Edges { get; }

        public bool TryGetNode(string id, out FlowNode node)
            => _nodesById.TryGetValue(id, out node!);

        public IReadOnlyList<FlowEdge> GetOutgoingEdges(string id)
            => _edgesByFrom.TryGetValue(id, out var edges) ? edges : EmptyEdges;

        public IReadOnlyList<FlowEdge> GetIncomingEdges(string id)
            => _edgesByTo.TryGetValue(id, out var edges) ? edges : EmptyEdges;
    }

    public sealed class FlowNode
    {
        private static readonly IReadOnlyDictionary<string, object?> EmptyProps =
            new ReadOnlyDictionary<string, object?>(new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase));

        public FlowNode(string id, string type, IReadOnlyDictionary<string, object?>? props)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Props = props ?? EmptyProps;
        }

        public string Id { get; }
        public string Type { get; }
        public IReadOnlyDictionary<string, object?> Props { get; }

        public string DisplayName => GetString("name") ?? Id;

        public string? GetString(string key)
        {
            if (TryGetValue(key, out var value))
            {
                return value?.ToString();
            }

            return null;
        }

        public IReadOnlyList<string> GetStringList(string key)
        {
            if (!TryGetValue(key, out var value) || value is null)
            {
                return Array.Empty<string>();
            }

            if (value is string s)
            {
                return new[] { s };
            }

            if (value is IEnumerable<object?> enumerable)
            {
                return enumerable
                    .Select(v => v?.ToString())
                    .Where(static v => !string.IsNullOrWhiteSpace(v))
                    .Cast<string>()
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToArray();
            }

            return new[] { value.ToString() ?? string.Empty };
        }

        public bool TryGetValue(string key, out object? value)
        {
            if (string.IsNullOrEmpty(key))
            {
                value = null;
                return false;
            }

            if (Props.TryGetValue(key, out value))
            {
                return true;
            }

            foreach (var kv in Props)
            {
                if (string.Equals(kv.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    value = kv.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }
    }

    public sealed class FlowEdge
    {
        private static readonly IReadOnlyDictionary<string, object?> EmptyProps =
            new ReadOnlyDictionary<string, object?>(new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase));

        public FlowEdge(string fromId, string toId, string kind, IReadOnlyDictionary<string, object?>? props)
        {
            FromId = fromId ?? throw new ArgumentNullException(nameof(fromId));
            ToId = toId ?? throw new ArgumentNullException(nameof(toId));
            Kind = kind ?? string.Empty;
            Props = props ?? EmptyProps;
        }

        public string FromId { get; }
        public string ToId { get; }
        public string Kind { get; }
        public IReadOnlyDictionary<string, object?> Props { get; }

        public string? GetString(string key)
        {
            if (Props.TryGetValue(key, out var value))
            {
                return value?.ToString();
            }

            foreach (var kv in Props)
            {
                if (string.Equals(kv.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    return kv.Value?.ToString();
                }
            }

            return null;
        }
    }
}
