using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;

namespace GraphKit.Outputs;

public static class FlowBuilder
{
    public static string BuildFlows(GraphDocument document, Func<GraphNode, bool> controllerPredicate)
    {
        var nodesById = document.Nodes.ToDictionary(n => n.Id, StringComparer.Ordinal);
        var edgesByFrom = document.Edges
            .GroupBy(e => e.From, StringComparer.Ordinal)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(e => nodesById.TryGetValue(e.To, out var node) ? node.Fqdn : e.To, StringComparer.OrdinalIgnoreCase).ToList(),
                StringComparer.Ordinal);
        var mapLookup = BuildMapLookup(document);

        var controllers = document.Nodes
            .Where(n => n.Type == "endpoint.controller" && controllerPredicate(n))
            .OrderBy(n => n.Fqdn, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (controllers.Count == 0)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        foreach (var controller in controllers)
        {
            AppendControllerFlow(builder, document, controller, nodesById, edgesByFrom, mapLookup);
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static void AppendControllerFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode controller,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup)
    {
        var method = GetNodeProp(controller, "http_method") ?? "GET";
        var route = GetNodeProp(controller, "route") ?? "/";
        var span = controller.Span;
        builder.AppendLine($"[web] {method} {route}  ({controller.Fqdn})  [L{span?.StartLine}–L{span?.EndLine}]");

        if (!edgesByFrom.TryGetValue(controller.Id, out var controllerEdges))
        {
            return;
        }

        foreach (var mapEdge in controllerEdges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, document, mapEdge, nodesById, edgesByFrom, mapLookup, indent: 1);
        }

        foreach (var castEdge in controllerEdges.Where(e => e.Kind == "casts_to"))
        {
            var annotation = castEdge.Props is { } props && props.TryGetValue("cast_kind", out var castValue)
                ? castValue?.ToString()
                : null;
            AppendMappingEdge(builder, document, castEdge, nodesById, edgesByFrom, mapLookup, indent: 1, label: "casts_to", annotation: annotation, includeAutomapper: false);
        }

        foreach (var clientEdge in controllerEdges.Where(e => e.Kind == "uses_client"))
        {
            if (!nodesById.TryGetValue(clientEdge.To, out var clientNode))
            {
                continue;
            }

            AppendHttpClientUsage(builder, clientEdge, clientNode, nodesById, edgesByFrom, indent: 1);
        }

        foreach (var validatorEdge in controllerEdges.Where(e => e.Kind == "uses_validator"))
        {
            if (!nodesById.TryGetValue(validatorEdge.To, out var validatorNode))
            {
                continue;
            }

            var lineText = validatorEdge.Transform?.Location?.Line is int line
                ? $" [L{line}]"
                : string.Empty;
            var targetType = validatorEdge.Props is { } props && props.TryGetValue("target_type", out var value)
                ? value?.ToString()
                : null;
            var extra = string.IsNullOrWhiteSpace(targetType) ? string.Empty : $" ({targetType})";
            AppendIndented(builder, 1, $"uses_validator {validatorNode.Name}{extra}{lineText}");
        }

        foreach (var cacheEdge in controllerEdges.Where(e => e.Kind == "uses_cache"))
        {
            if (!nodesById.TryGetValue(cacheEdge.To, out var cacheNode))
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
            var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var methodText = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            AppendIndented(builder, 1, $"uses_cache {cacheNode.Name}{methodText}{operationText}{keyText}{lineText}");
        }

        foreach (var optionsEdge in controllerEdges.Where(e => e.Kind == "uses_options"))
        {
            if (!nodesById.TryGetValue(optionsEdge.To, out var optionsNode))
            {
                continue;
            }

            var section = GetNodeProp(optionsNode, "section");
            var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
            var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, 1, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
        }

        foreach (var notificationEdge in controllerEdges.Where(e => e.Kind == "publishes_notification"))
        {
            if (!nodesById.TryGetValue(notificationEdge.To, out var notificationNode))
            {
                continue;
            }

            var lineText = notificationEdge.Transform?.Location?.Line is int line
                ? $" [L{line}]"
                : string.Empty;
            AppendIndented(builder, 1, $"publishes_notification {notificationNode.Name}{lineText}");
            AppendNotificationFlow(builder, document, notificationNode, nodesById, edgesByFrom, mapLookup, indent: 2);
        }
    }

    private static void AppendMappingEdge(
        StringBuilder builder,
        GraphDocument document,
        GraphEdge edge,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent,
        string label = "maps_to",
        string? annotation = null,
        bool includeAutomapper = true)
    {
        if (!nodesById.TryGetValue(edge.To, out var destination))
        {
            return;
        }

        var lineText = edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        var variable = edge.Props is { } props && props.TryGetValue("variable", out var value)
            ? value?.ToString()
            : null;
        var variableText = string.IsNullOrWhiteSpace(variable) ? string.Empty : $" (var {variable})";
        var annotationText = string.IsNullOrWhiteSpace(annotation) ? string.Empty : $" [{annotation}]";
        AppendIndented(builder, indent, $"{label} {destination.Name}{variableText}{lineText}{annotationText}");

        if (includeAutomapper)
        {
            AppendAutomapperRegistrations(builder, document, edge, mapLookup, indent + 1);
        }

        if (!edgesByFrom.TryGetValue(destination.Id, out var downstreamEdges))
        {
            return;
        }

        foreach (var convertEdge in downstreamEdges.Where(e => e.Kind == "converts_to"))
        {
            AppendConversion(builder, document, convertEdge, nodesById, mapLookup, indent + 1);
        }

        if (destination.Type == "cqrs.request")
        {
            AppendCommandFlow(builder, document, destination, nodesById, edgesByFrom, mapLookup, indent + 1);
        }
    }

    private static void AppendCommandFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode command,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent)
    {
        if (!edgesByFrom.TryGetValue(command.Id, out var edges))
        {
            return;
        }

        foreach (var handlerEdge in edges.Where(e => e.Kind == "handled_by"))
        {
            if (!nodesById.TryGetValue(handlerEdge.To, out var handlerNode))
            {
                continue;
            }

            var span = handlerNode.Span;
            AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
            AppendHandlerFlow(builder, document, handlerNode, nodesById, edgesByFrom, mapLookup, indent + 1);
        }
    }

    private static void AppendHandlerFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode handler,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent)
    {
        if (!edgesByFrom.TryGetValue(handler.Id, out var edges))
        {
            return;
        }

        foreach (var call in edges.Where(e => e.Kind == "calls"))
        {
            if (!nodesById.TryGetValue(call.To, out var target))
            {
                continue;
            }

            var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue)
                ? methodValue?.ToString()
                : null;
            var methodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
            var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"calls {target.Name}{methodText}{lineText}");
            AppendRepositoryFlow(builder, document, target, nodesById, edgesByFrom, mapLookup, indent + 1);
        }

        foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, document, mapping, nodesById, edgesByFrom, mapLookup, indent);
        }

        foreach (var service in edges.Where(e => e.Kind == "uses_service"))
        {
            if (!nodesById.TryGetValue(service.To, out var serviceNode))
            {
                continue;
            }

            var lineText = service.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                ? lifetimeValue?.ToString()
                : null;
            var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
            AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{lineText}");
        }

        foreach (var cacheEdge in edges.Where(e => e.Kind == "uses_cache"))
        {
            if (!nodesById.TryGetValue(cacheEdge.To, out var cacheNode))
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
            var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var methodText = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{methodText}{operationText}{keyText}{lineText}");
        }

        foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
        {
            if (!nodesById.TryGetValue(optionsEdge.To, out var optionsNode))
            {
                continue;
            }

            var section = GetNodeProp(optionsNode, "section");
            var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
            var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
        }

        foreach (var publish in edges.Where(e => e.Kind == "publishes"))
        {
            if (!nodesById.TryGetValue(publish.To, out var messageNode))
            {
                continue;
            }

            var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"publishes {messageNode.Name}{lineText}");
        }

        foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
        {
            if (!nodesById.TryGetValue(notificationEdge.To, out var notificationNode))
            {
                continue;
            }

            var lineText = notificationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
            AppendNotificationFlow(builder, document, notificationNode, nodesById, edgesByFrom, mapLookup, indent + 1);
        }
    }

    private static void AppendRepositoryFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode repository,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent)
    {
        if (!edgesByFrom.TryGetValue(repository.Id, out var edges))
        {
            return;
        }

        foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, document, mapping, nodesById, edgesByFrom, mapLookup, indent);
        }

        foreach (var write in edges.Where(e => e.Kind == "writes_to"))
        {
            if (!nodesById.TryGetValue(write.To, out var entityNode))
            {
                continue;
            }

            var lineText = write.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"writes_to {entityNode.Name}{lineText}");
            AppendEntityFlow(builder, document, entityNode, nodesById, edgesByFrom, indent + 1);
        }

        foreach (var cacheEdge in edges.Where(e => e.Kind == "uses_cache"))
        {
            if (!nodesById.TryGetValue(cacheEdge.To, out var cacheNode))
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
            var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var methodText = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{methodText}{operationText}{keyText}{lineText}");
        }

        foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
        {
            if (!nodesById.TryGetValue(optionsEdge.To, out var optionsNode))
            {
                continue;
            }

            var section = GetNodeProp(optionsNode, "section");
            var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
            var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
        }
    }

    private static void AppendEntityFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode entity,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        int indent)
    {
        if (!edgesByFrom.TryGetValue(entity.Id, out var edges))
        {
            return;
        }

        foreach (var tableEdge in edges.Where(e => e.Kind is "writes_to" or "reads_from"))
        {
            if (!nodesById.TryGetValue(tableEdge.To, out var tableNode))
            {
                continue;
            }

            var transform = tableEdge.Kind == "writes_to" ? "writes_to" : "reads_from";
            var lineText = tableEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"{transform} {tableNode.Name}{lineText}");
        }
    }

    private static void AppendConversion(
        StringBuilder builder,
        GraphDocument document,
        GraphEdge edge,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent)
    {
        if (!nodesById.TryGetValue(edge.To, out var destination))
        {
            return;
        }

        var lineText = edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        AppendIndented(builder, indent, $"converts_to {destination.Name}{lineText}");
        AppendAutomapperRegistrations(builder, document, edge, mapLookup, indent + 1);
    }

    private static void AppendAutomapperRegistrations(
        StringBuilder builder,
        GraphDocument document,
        GraphEdge edge,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
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
        if (!mapLookup.TryGetValue(key, out var maps))
        {
            return;
        }

        foreach (var mapNode in maps)
        {
            var profileName = ResolveProfileName(document, mapNode);
            var mapLabel = mapNode.Props is { } props && props.TryGetValue("map", out var mapValue)
                ? mapValue?.ToString()
                : mapNode.Name;
            AppendIndented(builder, indent, $"automapper.registration {profileName} ({mapLabel}) [L{mapNode.Span?.StartLine}]");
        }
    }

    private static string ResolveProfileName(GraphDocument document, GraphNode mapNode)
    {
        foreach (var edge in document.Edges.Where(e => e.From == mapNode.Id && e.Kind == "generated_from"))
        {
            var profileNode = document.Nodes.FirstOrDefault(n => n.Id == edge.To);
            if (profileNode is not null)
            {
                return profileNode.Name;
            }
        }

        return mapNode.Name;
    }

    private static IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> BuildMapLookup(GraphDocument document)
    {
        var lookup = new Dictionary<(string Source, string Destination), List<GraphNode>>();
        foreach (var node in document.Nodes.Where(n => n.Type == "mapping.automapper.map"))
        {
            var source = node.Props is { } props && props.TryGetValue("source_type", out var sourceValue)
                ? GetSimpleType(sourceValue?.ToString())
                : string.Empty;
            var destination = node.Props is { } props2 && props2.TryGetValue("destination_type", out var destinationValue)
                ? GetSimpleType(destinationValue?.ToString())
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

    private static void AppendHttpClientUsage(
        StringBuilder builder,
        GraphEdge clientEdge,
        GraphNode clientNode,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        int indent)
    {
        var lineText = clientEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        AppendIndented(builder, indent, $"uses_client {clientNode.Name}{lineText}");

        if (!edgesByFrom.TryGetValue(clientNode.Id, out var clientEdges))
        {
            return;
        }

        foreach (var callEdge in clientEdges.Where(e => e.Kind == "calls"))
        {
            if (!nodesById.TryGetValue(callEdge.To, out var targetNode))
            {
                continue;
            }

            var verb = callEdge.Props is { } props && props.TryGetValue("verb", out var verbValue)
                ? verbValue?.ToString()
                : null;
            var route = callEdge.Props is { } props2 && props2.TryGetValue("route", out var routeValue)
                ? routeValue?.ToString()
                : null;
            var baseUrl = callEdge.Props is { } props3 && props3.TryGetValue("base_url", out var baseValue)
                ? baseValue?.ToString()
                : null;
            var configKey = callEdge.Props is { } props4 && props4.TryGetValue("configuration_key", out var configValue)
                ? configValue?.ToString()
                : null;
            var targetService = callEdge.Props is { } props5 && props5.TryGetValue("target_service", out var serviceValue)
                ? serviceValue?.ToString()
                : null;

            var details = new List<string>();
            if (!string.IsNullOrWhiteSpace(verb) && !string.IsNullOrWhiteSpace(route))
            {
                details.Add($"{verb} {route}");
            }

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                details.Add($"base={baseUrl}");
            }

            if (!string.IsNullOrWhiteSpace(configKey))
            {
                details.Add($"config={configKey}");
            }

            if (!string.IsNullOrWhiteSpace(targetService))
            {
                details.Add($"target={targetService}");
            }

            var detailText = details.Count > 0 ? $" ({string.Join(", ", details)})" : string.Empty;
            var callLineText = callEdge.Transform?.Location?.Line is int callLine ? $" [L{callLine}]" : string.Empty;
            AppendIndented(builder, indent + 1, $"calls {targetNode.Name}{detailText}{callLineText}");
        }
    }

    private static string? GetNodeProp(GraphNode node, string key)
        => node.Props is { } props && props.TryGetValue(key, out var value) ? value?.ToString() : null;

    private static string GetSimpleType(string? type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return string.Empty;
        }

        var simple = type.Replace("?", string.Empty, StringComparison.Ordinal);
        var genericIndex = simple.IndexOf('<');
        if (genericIndex >= 0)
        {
            simple = simple[..genericIndex];
        }

        var lastDot = simple.LastIndexOf('.');
        if (lastDot >= 0)
        {
            return simple[(lastDot + 1)..];
        }

        return simple;
    }

    private static void AppendIndented(StringBuilder builder, int indent, string text)
    {
        builder.Append(' ', indent * 2);
        builder.AppendLine($"└─ {text}");
    }

    private static void AppendNotificationFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode notification,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent,
        HashSet<string>? visitedNotifications = null,
        HashSet<string>? visitedHandlers = null)
    {
        visitedNotifications ??= new HashSet<string>(StringComparer.Ordinal);
        if (!visitedNotifications.Add(notification.Id))
        {
            return;
        }

        if (!edgesByFrom.TryGetValue(notification.Id, out var edges))
        {
            visitedNotifications.Remove(notification.Id);
            return;
        }

        try
        {
            foreach (var handlerEdge in edges.Where(e => e.Kind == "handled_by"))
            {
                if (!nodesById.TryGetValue(handlerEdge.To, out var handlerNode))
                {
                    continue;
                }

                var span = handlerNode.Span;
                AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
                AppendNotificationHandlerFlow(
                    builder,
                    document,
                    handlerNode,
                    nodesById,
                    edgesByFrom,
                    mapLookup,
                    indent + 1,
                    visitedNotifications,
                    visitedHandlers);
            }
        }
        finally
        {
            visitedNotifications.Remove(notification.Id);
        }
    }

    private static void AppendNotificationHandlerFlow(
        StringBuilder builder,
        GraphDocument document,
        GraphNode handler,
        IReadOnlyDictionary<string, GraphNode> nodesById,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
        int indent,
        HashSet<string> visitedNotifications,
        HashSet<string>? visitedHandlers = null)
    {
        visitedHandlers ??= new HashSet<string>(StringComparer.Ordinal);
        if (!visitedHandlers.Add(handler.Id))
        {
            return;
        }

        if (!edgesByFrom.TryGetValue(handler.Id, out var edges))
        {
            visitedHandlers.Remove(handler.Id);
            return;
        }

        try
        {
            foreach (var call in edges.Where(e => e.Kind == "calls"))
            {
                if (!nodesById.TryGetValue(call.To, out var target))
                {
                    continue;
                }

                var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var methodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {target.Name}{methodText}{lineText}");
                AppendRepositoryFlow(builder, document, target, nodesById, edgesByFrom, mapLookup, indent + 1);
            }

            foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
            {
                AppendMappingEdge(builder, document, mapping, nodesById, edgesByFrom, mapLookup, indent);
            }

            foreach (var service in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!nodesById.TryGetValue(service.To, out var serviceNode))
                {
                    continue;
                }

                var lineText = service.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                    ? lifetimeValue?.ToString()
                    : null;
                var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{lineText}");
            }

            foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
            {
                if (!nodesById.TryGetValue(requestEdge.To, out var requestNode))
                {
                    continue;
                }

                var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"sends_request {requestNode.Name}{lineText}");
                AppendCommandFlow(builder, document, requestNode, nodesById, edgesByFrom, mapLookup, indent + 1);
            }

            foreach (var publish in edges.Where(e => e.Kind == "publishes_notification"))
            {
                if (!nodesById.TryGetValue(publish.To, out var notificationNode))
                {
                    continue;
                }

                var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
                AppendNotificationFlow(
                    builder,
                    document,
                    notificationNode,
                    nodesById,
                    edgesByFrom,
                    mapLookup,
                    indent + 1,
                    visitedNotifications,
                    visitedHandlers);
            }
        }
        finally
        {
            visitedHandlers.Remove(handler.Id);
        }
    }
}
