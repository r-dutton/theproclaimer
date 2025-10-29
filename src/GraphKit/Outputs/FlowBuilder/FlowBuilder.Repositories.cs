using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;

using static GraphKit.Outputs.Utilities;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    public static void AppendRepositoryFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode repository,
        int indent)
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }

        if (!state.EdgesByFrom.TryGetValue(repository.Id, out var edges))
        {
            return;
        }

        foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, state, mapping, indent);
        }

        var writeKinds = new HashSet<string>(new[] { "writes_to", "inserts_into", "updates", "deletes_from", "upserts" }, StringComparer.Ordinal);
        var writeOps = edges.Where(e => writeKinds.Contains(e.Kind)).ToList();
        if (writeOps.Count > 1)
        {
            AppendIndented(builder, indent, $"transaction (writes={writeOps.Count})");
        }

        foreach (var write in edges.Where(e => e.Kind is "writes_to" or "queries" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
        {
            if (!state.NodesById.TryGetValue(write.To, out var entityNode))
            {
                continue;
            }

            var operation = ExtractOperationLabel(write);
            var baseLabel = $"{operation} {entityNode.Name}";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, write.Transform?.Location));
            state.CurrentImpact?.RecordRepositoryOperation(GetDisplayName(repository), write.Kind, GetDisplayName(entityNode));
            if (Utilities.IsEntityNode(entityNode) || Utilities.IsLikelyEntity(entityNode))
            {
                AppendEntityFlow(builder, state, entityNode, indent + 1, write.Kind);
            }
        }

        foreach (var cacheEdge in edges.Where(e => e.Kind == "uses_cache"))
        {
            if (!state.NodesById.TryGetValue(cacheEdge.To, out var cacheNode))
            {
                continue;
            }

            var cacheMethod = cacheEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                ? methodValue?.ToString()
                : null;
            var operation = cacheEdge.Props is { } opProps && opProps.TryGetValue("operation", out var opValue)
                ? opValue?.ToString()
                : null;
            var key = cacheEdge.Props is { } keyProps && keyProps.TryGetValue("key", out var keyValue)
                ? keyValue?.ToString()
                : null;
            var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
            state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
            if (!state.DedupRequests.Add("CACHE::" + cacheKey))
            {
                continue;
            }

            var baseLabel = $"uses_cache {cacheNode.Name}{methodPart}";
            AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, cacheEdge.Transform?.Location)}{opPart}{keyPart}");
            state.CurrentImpact?.RecordCache(GetDisplayName(cacheNode));
        }

        foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
        {
            if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode))
            {
                continue;
            }

            var section = GetNodeProp(optionsNode, "section");
            var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
            var baseLabel = $"uses_options {optionsNode.Name}{sectionText}";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, optionsEdge.Transform?.Location));
            state.CurrentImpact?.RecordOption(GetDisplayName(optionsNode));
        }
    }

    public static void AppendEntityFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode entity,
        int indent,
        string? operationFilter = null)
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }

        if (!state.EdgesByFrom.TryGetValue(entity.Id, out var edges))
        {
            return;
        }

        var candidateEdges = edges
            .Where(e => e.Kind is "writes_to" or "reads_from" or "queries" or "inserts_into" or "updates" or "deletes_from" or "upserts")
            .ToList();

        if (!string.IsNullOrWhiteSpace(operationFilter))
        {
            var filtered = candidateEdges
                .Where(e => string.Equals(e.Kind, operationFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (filtered.Count > 0)
            {
                candidateEdges = filtered;
            }
        }

        foreach (var tableEdge in candidateEdges)
        {
            if (!state.NodesById.TryGetValue(tableEdge.To, out var tableNode))
            {
                continue;
            }

            var transform = tableEdge.Kind == "reads_from"
                ? "reads_from"
                : ExtractOperationLabel(tableEdge);
            var baseLabel = $"{transform} {tableNode.Name}";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, tableEdge.Transform?.Location));
        }

        foreach (var mapEdge in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, state, mapEdge, indent);
        }
    }

    public static void AppendConversion(
        StringBuilder builder,
        FlowRenderState state,
        GraphEdge edge,
        int indent)
    {
        if (!state.NodesById.TryGetValue(edge.To, out var destination))
        {
            return;
        }

        var baseLabel = $"converts_to {destination.Name}";
        AppendIndented(builder, indent, FormatLinkedCode(baseLabel, edge.Transform?.Location));
        AppendAutomapperRegistrations(builder, state, edge, indent + 1);
    }

    public static void AppendAutomapperRegistrations(
        StringBuilder builder,
        FlowRenderState state,
        GraphEdge edge,
        int indent)
    {
        if (edge.Props is null)
        {
            return;
        }

        edge.Props.TryGetValue("source_type", out var sourceObj);
        edge.Props.TryGetValue("destination_type", out var destinationObj);
        var source = sourceObj?.ToString();
        var destination = destinationObj?.ToString();
        if (string.IsNullOrWhiteSpace(destination))
        {
            return;
        }

        var key = (GetSimpleType(source), GetSimpleType(destination));
        if (!state.MapLookup.TryGetValue(key, out var maps))
        {
            return;
        }

        string? callerRoot = null;
        if (state.NodesById.TryGetValue(edge.From, out var callerNode))
        {
            callerRoot = GetAssemblyRoot(callerNode.Assembly);
        }

        IEnumerable<GraphNode> filtered = maps;
        if (!string.IsNullOrWhiteSpace(callerRoot))
        {
            var sameRoot = maps
                .Where(m => string.Equals(GetAssemblyRoot(m.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (sameRoot.Count > 0)
            {
                filtered = sameRoot;
            }
            else
            {
                var withFiles = maps
                    .Where(m => !string.IsNullOrWhiteSpace(m.FilePath) && !m.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (withFiles.Count > 0)
                {
                    filtered = withFiles;
                }
            }
        }

        var filteredSet = new HashSet<string>(filtered.Select(f => f.Id));
        var elided = maps.Where(m => !filteredSet.Contains(m.Id)).ToList();

        foreach (var mapNode in filtered.OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase))
        {
            var profileName = ResolveProfileName(state, mapNode);
            var mapLabel = mapNode.Props is { } props && props.TryGetValue("map", out var mapValue)
                ? mapValue?.ToString()
                : mapNode.Name;
            var baseLabel = $"automapper.registration {profileName} ({mapLabel})";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, mapNode));
        }

        if (elided.Count > 0)
        {
            var elidedRoots = elided
                .Select(e => GetAssemblyRoot(e.Assembly))
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(r => r, StringComparer.OrdinalIgnoreCase)
                .ToList();
            var rootSummary = elidedRoots.Count > 0 ? $" ({string.Join(", ", elidedRoots)})" : string.Empty;
            // Intentionally suppress verbose listing; summary available via elidedRoots.
        }
    }

    public static string ResolveProfileName(FlowRenderState state, GraphNode mapNode)
    {
        foreach (var edge in state.Document.Edges.Where(e => e.From == mapNode.Id && e.Kind == "generated_from"))
        {
            if (state.NodesById.TryGetValue(edge.To, out var profileNode))
            {
                return profileNode.Name;
            }

            profileNode = state.Document.Nodes.FirstOrDefault(n => n.Id == edge.To);
            if (profileNode is not null)
            {
                return profileNode.Name;
            }
        }

        return mapNode.Name;
    }
}
