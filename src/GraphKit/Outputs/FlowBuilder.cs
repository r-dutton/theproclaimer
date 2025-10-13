using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public static class FlowBuilder
{
    public static string BuildFlows(GraphDocument document, Func<GraphNode, bool> controllerPredicate, FlowWorkspaceIndex? workspace = null)
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
            var state = new FlowRenderState(document, nodesById, edgesByFrom, mapLookup, workspace);
            AppendControllerFlow(builder, state, controller);
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static void AppendControllerFlow(StringBuilder builder, FlowRenderState state, GraphNode controller)
    {
        AppendEndpointFlow(builder, state, controller, indent: 0);
    }

    private static void AppendEndpointFlow(StringBuilder builder, FlowRenderState state, GraphNode endpoint, int indent)
    {
        if (!state.EndpointStack.Add(endpoint.Id))
        {
            return;
        }

        try
        {
            var method = GetNodeProp(endpoint, "http_method") ?? "GET";
            var route = GetNodeProp(endpoint, "route") ?? "/";
            var span = endpoint.Span;
            var header = $"[web] {method} {route}  ({endpoint.Fqdn})  [L{span?.StartLine}–L{span?.EndLine}]";

            if (indent <= 0)
            {
                builder.AppendLine(header);
            }
            else
            {
                AppendIndented(builder, indent, header);
            }

            if (!state.EdgesByFrom.TryGetValue(endpoint.Id, out var edges))
            {
                return;
            }

            var childIndent = indent <= 0 ? 1 : indent + 1;

            foreach (var mapEdge in edges.Where(e => e.Kind == "maps_to"))
            {
                AppendMappingEdge(builder, state, mapEdge, childIndent);
            }

            foreach (var castEdge in edges.Where(e => e.Kind == "casts_to"))
            {
                var annotation = castEdge.Props is { } props && props.TryGetValue("cast_kind", out var castValue)
                    ? castValue?.ToString()
                    : null;
                AppendMappingEdge(builder, state, castEdge, childIndent, label: "casts_to", annotation: annotation, includeAutomapper: false);
            }

            foreach (var clientEdge in edges.Where(e => e.Kind == "uses_client"))
            {
                if (!state.NodesById.TryGetValue(clientEdge.To, out var clientNode))
                {
                    continue;
                }

                AppendHttpClientUsage(builder, state, clientEdge, clientNode, childIndent);
            }

            foreach (var validatorEdge in edges.Where(e => e.Kind == "uses_validator"))
            {
                if (!state.NodesById.TryGetValue(validatorEdge.To, out var validatorNode))
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
                AppendIndented(builder, childIndent, $"uses_validator {validatorNode.Name}{extra}{lineText}");
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
                var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var cacheLineText = string.IsNullOrWhiteSpace(cacheMethod) ? lineText : string.Empty;
                var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
                var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
                AppendIndented(builder, childIndent, $"uses_cache {cacheNode.Name}{cacheLineText}");

                if (!string.IsNullOrWhiteSpace(cacheMethod))
                {
                    AppendIndented(builder, childIndent + 1, $"method {cacheMethod}{operationText}{keyText}{lineText}");
                }
            }

            foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
            {
                if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode))
                {
                    continue;
                }

                var section = GetNodeProp(optionsNode, "section");
                var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
                var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, childIndent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
            }

            foreach (var callEdge in edges.Where(e => e.Kind == "calls"))
            {
                if (!state.NodesById.TryGetValue(callEdge.To, out var targetNode))
                {
                    continue;
                }

                var callMethod = callEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = callEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, childIndent, $"calls {targetNode.Name}{serviceMethodText}{lineText}");

                if (targetNode.Type == "app.repository")
                {
                    AppendRepositoryFlow(builder, state, targetNode, childIndent + 1);
                }
            }

            foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to"))
            {
                if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
                {
                    continue;
                }

                var lineText = dataEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var label = dataEdge.Kind == "writes_to" ? "writes_to" : "queries";
                AppendIndented(builder, childIndent, $"{label} {entityNode.Name}{lineText}");

                if (entityNode.Type == "ef.entity")
                {
                    AppendEntityFlow(builder, state, entityNode, childIndent + 1);
                }
            }

            foreach (var serviceEdge in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!state.NodesById.TryGetValue(serviceEdge.To, out var serviceNode))
                {
                    continue;
                }

                var lineText = serviceEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var lifetime = serviceEdge.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                    ? lifetimeValue?.ToString()
                    : null;
                var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                var serviceMethodName = serviceEdge.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceLineText = string.IsNullOrWhiteSpace(serviceMethodName) ? lineText : string.Empty;
                AppendIndented(builder, childIndent, $"uses_service {serviceNode.Name}{suffix}{serviceLineText}");

                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, childIndent + 1, $"method {serviceMethodName}{lineText}");
                }
            }

            foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
            {
                if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode))
                {
                    continue;
                }

                var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, childIndent, $"sends_request {requestNode.Name}{lineText}");
                AppendCommandFlow(builder, state, requestNode, childIndent + 1);
            }

            foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
            {
                if (!state.NodesById.TryGetValue(notificationEdge.To, out var notificationNode))
                {
                    continue;
                }

                var lineText = notificationEdge.Transform?.Location?.Line is int line
                    ? $" [L{line}]"
                    : string.Empty;
                AppendIndented(builder, childIndent, $"publishes_notification {notificationNode.Name}{lineText}");
                AppendNotificationFlow(builder, state, notificationNode, childIndent + 1);
            }
        }
        finally
        {
            state.EndpointStack.Remove(endpoint.Id);
        }
    }

    private static void AppendMappingEdge(
        StringBuilder builder,
        FlowRenderState state,
        GraphEdge edge,
        int indent,
        string label = "maps_to",
        string? annotation = null,
        bool includeAutomapper = true)
    {
        if (!state.NodesById.TryGetValue(edge.To, out var destination))
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
            AppendAutomapperRegistrations(builder, state, edge, indent + 1);
        }

        if (!state.EdgesByFrom.TryGetValue(destination.Id, out var downstreamEdges))
        {
            return;
        }

        foreach (var convertEdge in downstreamEdges.Where(e => e.Kind == "converts_to"))
        {
            AppendConversion(builder, state, convertEdge, indent + 1);
        }

        if (destination.Type == "cqrs.request")
        {
            AppendCommandFlow(builder, state, destination, indent + 1);
        }
    }

    private static void AppendCommandFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode command,
        int indent)
    {
        if (!state.EdgesByFrom.TryGetValue(command.Id, out var edges))
        {
            return;
        }

        foreach (var handlerEdge in edges.Where(e => e.Kind == "handled_by"))
        {
            if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode))
            {
                continue;
            }

            var span = handlerNode.Span;
            AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
            AppendHandlerFlow(builder, state, handlerNode, indent + 1);
        }
    }

    private static void AppendHandlerFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode handler,
        int indent)
    {
        if (!state.HandlerStack.Add(handler.Id))
        {
            return;
        }

        try
        {
            if (!state.EdgesByFrom.TryGetValue(handler.Id, out var edges))
            {
                return;
            }

            foreach (var call in edges.Where(e => e.Kind == "calls"))
            {
                if (!state.NodesById.TryGetValue(call.To, out var target))
                {
                    continue;
                }

                var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                AppendRepositoryFlow(builder, state, target, indent + 1);
            }

            foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
            {
                AppendMappingEdge(builder, state, mapping, indent);
            }

            foreach (var clientEdge in edges.Where(e => e.Kind == "uses_client"))
            {
                if (!state.NodesById.TryGetValue(clientEdge.To, out var clientNode))
                {
                    continue;
                }

                AppendHttpClientUsage(builder, state, clientEdge, clientNode, indent);
            }

            foreach (var service in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!state.NodesById.TryGetValue(service.To, out var serviceNode))
                {
                    continue;
                }

                var lineText = service.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                    ? lifetimeValue?.ToString()
                    : null;
                var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                var serviceMethodName = service.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceLineText = string.IsNullOrWhiteSpace(serviceMethodName) ? lineText : string.Empty;
                AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{serviceLineText}");

                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, indent + 1, $"method {serviceMethodName}{lineText}");
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
                var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var cacheLineText = string.IsNullOrWhiteSpace(cacheMethod) ? lineText : string.Empty;
                var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
                var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
                AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{cacheLineText}");

                if (!string.IsNullOrWhiteSpace(cacheMethod))
                {
                    AppendIndented(builder, indent + 1, $"method {cacheMethod}{operationText}{keyText}{lineText}");
                }
            }

            foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
            {
                if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode))
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
                if (!state.NodesById.TryGetValue(publish.To, out var messageNode))
                {
                    continue;
                }

                var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var details = BuildPublisherDetails(messageNode);
                AppendIndented(builder, indent, $"publishes {messageNode.Name}{details}{lineText}");
                AppendPublisherFlow(builder, state, messageNode, indent + 1);
            }

            foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
            {
                if (!state.NodesById.TryGetValue(notificationEdge.To, out var notificationNode))
                {
                    continue;
                }

                var lineText = notificationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
                AppendNotificationFlow(builder, state, notificationNode, indent + 1);
            }
        }
        finally
        {
            state.HandlerStack.Remove(handler.Id);
        }
    }

    private static void AppendRepositoryFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode repository,
        int indent)
    {
        if (!state.EdgesByFrom.TryGetValue(repository.Id, out var edges))
        {
            return;
        }

        foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, state, mapping, indent);
        }

        foreach (var write in edges.Where(e => e.Kind is "writes_to" or "queries"))
        {
            if (!state.NodesById.TryGetValue(write.To, out var entityNode))
            {
                continue;
            }

            var lineText = write.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var operation = write.Kind == "queries" ? "queries" : "writes_to";
            AppendIndented(builder, indent, $"{operation} {entityNode.Name}{lineText}");
            AppendEntityFlow(builder, state, entityNode, indent + 1);
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
            var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var cacheLineText = string.IsNullOrWhiteSpace(cacheMethod) ? lineText : string.Empty;
            var operationText = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyText = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{cacheLineText}");

            if (!string.IsNullOrWhiteSpace(cacheMethod))
            {
                AppendIndented(builder, indent + 1, $"method {cacheMethod}{operationText}{keyText}{lineText}");
            }
        }

        foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
        {
            if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode))
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
        FlowRenderState state,
        GraphNode entity,
        int indent)
    {
        if (!state.EdgesByFrom.TryGetValue(entity.Id, out var edges))
        {
            return;
        }

        foreach (var tableEdge in edges.Where(e => e.Kind is "writes_to" or "reads_from" or "queries"))
        {
            if (!state.NodesById.TryGetValue(tableEdge.To, out var tableNode))
            {
                continue;
            }

            var transform = tableEdge.Kind switch
            {
                "writes_to" => "writes_to",
                "queries" => "queries",
                _ => "reads_from"
            };
            var lineText = tableEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"{transform} {tableNode.Name}{lineText}");
        }
    }

    private static void AppendConversion(
        StringBuilder builder,
        FlowRenderState state,
        GraphEdge edge,
        int indent)
    {
        if (!state.NodesById.TryGetValue(edge.To, out var destination))
        {
            return;
        }

        var lineText = edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        AppendIndented(builder, indent, $"converts_to {destination.Name}{lineText}");
        AppendAutomapperRegistrations(builder, state, edge, indent + 1);
    }

    private static void AppendAutomapperRegistrations(
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

        foreach (var mapNode in maps)
        {
            var profileName = ResolveProfileName(state, mapNode);
            var mapLabel = mapNode.Props is { } props && props.TryGetValue("map", out var mapValue)
                ? mapValue?.ToString()
                : mapNode.Name;
            AppendIndented(builder, indent, $"automapper.registration {profileName} ({mapLabel}) [L{mapNode.Span?.StartLine}]");
        }
    }

    private static string ResolveProfileName(FlowRenderState state, GraphNode mapNode)
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
        FlowRenderState state,
        GraphEdge clientEdge,
        GraphNode clientNode,
        int indent)
    {
        var lineText = clientEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        AppendIndented(builder, indent, $"uses_client {clientNode.Name}{lineText}");

        if (!state.EdgesByFrom.TryGetValue(clientNode.Id, out var clientEdges))
        {
            return;
        }

        foreach (var callEdge in clientEdges.Where(e => e.Kind == "calls"))
        {
            if (!state.NodesById.TryGetValue(callEdge.To, out var targetNode))
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
            AppendTargetServiceFlow(builder, state, callEdge, indent + 2);
        }
    }

    private static void AppendTargetServiceFlow(StringBuilder builder, FlowRenderState state, GraphEdge callEdge, int indent)
    {
        if (state.Workspace is null)
        {
            return;
        }

        if (callEdge.Props is not { } props || !props.TryGetValue("target_service", out var serviceValue))
        {
            return;
        }

        var serviceName = serviceValue?.ToString();
        if (string.IsNullOrWhiteSpace(serviceName))
        {
            return;
        }

        if (!state.Workspace.TryGetAssemblies(serviceName, out var assemblies) || assemblies.Count == 0)
        {
            AppendIndented(builder, indent, $"target_service {serviceName}");
            return;
        }

        var assemblySet = assemblies is HashSet<string> set
            ? set
            : new HashSet<string>(assemblies, StringComparer.OrdinalIgnoreCase);

        var route = props.TryGetValue("route", out var routeValue) ? routeValue?.ToString() : null;
        var verb = props.TryGetValue("verb", out var verbValue) ? verbValue?.ToString() : null;

        var candidates = state.Document.Nodes
            .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                        && assemblySet.Contains(n.Assembly))
            .ToList();

        AppendIndented(builder, indent, $"target_service {serviceName}");

        if (candidates.Count == 0)
        {
            return;
        }

        var matched = FilterEndpointsByRouteAndVerb(candidates, route, verb);
        foreach (var endpoint in matched)
        {
            AppendEndpointFlow(builder, state, endpoint, indent + 1);
        }
    }

    private static IReadOnlyList<GraphNode> FilterEndpointsByRouteAndVerb(
        List<GraphNode> candidates,
        string? route,
        string? verb)
    {
        static bool Matches(string? expected, string? actual)
            => !string.IsNullOrWhiteSpace(expected) && !string.IsNullOrWhiteSpace(actual)
               && string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(route) && !string.IsNullOrWhiteSpace(verb))
        {
            var both = candidates
                .Where(n => Matches(route, GetNodeProp(n, "route")) && Matches(verb, GetNodeProp(n, "http_method")))
                .ToList();
            if (both.Count > 0)
            {
                return both;
            }
        }

        if (!string.IsNullOrWhiteSpace(route))
        {
            var routeOnly = candidates
                .Where(n => Matches(route, GetNodeProp(n, "route")))
                .ToList();
            if (routeOnly.Count > 0)
            {
                return routeOnly;
            }
        }

        if (!string.IsNullOrWhiteSpace(verb))
        {
            var verbOnly = candidates
                .Where(n => Matches(verb, GetNodeProp(n, "http_method")))
                .ToList();
            if (verbOnly.Count > 0)
            {
                return verbOnly;
            }
        }

        return candidates;
    }

    private sealed class FlowRenderState
    {
        public FlowRenderState(
            GraphDocument document,
            IReadOnlyDictionary<string, GraphNode> nodesById,
            IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
            IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
            FlowWorkspaceIndex? workspace)
        {
            Document = document;
            NodesById = nodesById;
            EdgesByFrom = edgesByFrom;
            MapLookup = mapLookup;
            Workspace = workspace;
        }

        public GraphDocument Document { get; }
        public IReadOnlyDictionary<string, GraphNode> NodesById { get; }
        public IReadOnlyDictionary<string, List<GraphEdge>> EdgesByFrom { get; }
        public IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> MapLookup { get; }
        public FlowWorkspaceIndex? Workspace { get; }
        public HashSet<string> EndpointStack { get; } = new(StringComparer.Ordinal);
        public HashSet<string> HandlerStack { get; } = new(StringComparer.Ordinal);
        public HashSet<string> NotificationStack { get; } = new(StringComparer.Ordinal);
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
        FlowRenderState state,
        GraphNode notification,
        int indent)
    {
        if (!state.NotificationStack.Add(notification.Id))
        {
            return;
        }

        try
        {
            if (!state.EdgesByFrom.TryGetValue(notification.Id, out var edges))
            {
                return;
            }

            foreach (var handlerEdge in edges.Where(e => e.Kind == "handled_by"))
            {
                if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode))
                {
                    continue;
                }

                var span = handlerNode.Span;
                AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
                AppendNotificationHandlerFlow(builder, state, handlerNode, indent + 1);
            }
        }
        finally
        {
            state.NotificationStack.Remove(notification.Id);
        }
    }

    private static void AppendNotificationHandlerFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode handler,
        int indent)
    {
        if (!state.HandlerStack.Add(handler.Id))
        {
            return;
        }

        try
        {
            if (!state.EdgesByFrom.TryGetValue(handler.Id, out var edges))
            {
                return;
            }

            foreach (var call in edges.Where(e => e.Kind == "calls"))
            {
                if (!state.NodesById.TryGetValue(call.To, out var target))
                {
                    continue;
                }

                var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                AppendRepositoryFlow(builder, state, target, indent + 1);
            }

            foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
            {
                AppendMappingEdge(builder, state, mapping, indent);
            }

            foreach (var service in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!state.NodesById.TryGetValue(service.To, out var serviceNode))
                {
                    continue;
                }

                var lineText = service.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                    ? lifetimeValue?.ToString()
                    : null;
                var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                var serviceMethodName = service.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceLineText = string.IsNullOrWhiteSpace(serviceMethodName) ? lineText : string.Empty;
                AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{serviceLineText}");

                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, indent + 1, $"method {serviceMethodName}{lineText}");
                }
            }

            foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
            {
                if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode))
                {
                    continue;
                }

                var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"sends_request {requestNode.Name}{lineText}");
                AppendCommandFlow(builder, state, requestNode, indent + 1);
            }

            foreach (var publish in edges.Where(e => e.Kind == "publishes_notification"))
            {
                if (!state.NodesById.TryGetValue(publish.To, out var notificationNode))
                {
                    continue;
                }

                var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
                AppendNotificationFlow(builder, state, notificationNode, indent + 1);
            }
        }
        finally
        {
            state.HandlerStack.Remove(handler.Id);
        }
    }

    private static void AppendPublisherFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode publisher,
        int indent)
    {
        if (!state.EdgesByFrom.TryGetValue(publisher.Id, out var edges))
        {
            return;
        }

        foreach (var produced in edges.Where(e => e.Kind == "produces_event"))
        {
            if (!state.NodesById.TryGetValue(produced.To, out var contract))
            {
                continue;
            }

            var lineText = produced.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"produces_event {contract.Name}{lineText}");
        }
    }

    private static string BuildPublisherDetails(GraphNode publisher)
    {
        if (publisher.Props is not { Count: > 0 })
        {
            return string.Empty;
        }

        var details = new List<string>();

        if (publisher.Props.TryGetValue("queue", out var queueValue) &&
            queueValue is string { Length: > 0 } queue)
        {
            details.Add($"queue={queue}");
        }

        if (publisher.Props.TryGetValue("subject", out var subjectValue) &&
            subjectValue is string { Length: > 0 } subject)
        {
            details.Add($"subject={subject}");
        }

        return details.Count > 0
            ? $" ({string.Join(", ", details)})"
            : string.Empty;
    }
}
