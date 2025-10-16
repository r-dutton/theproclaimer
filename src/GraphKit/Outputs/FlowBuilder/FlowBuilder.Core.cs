using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    private static readonly HashSet<string> WriteOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "writes_to",
        "inserts_into",
        "updates",
        "deletes_from",
        "upserts"
    };

    private static readonly HashSet<string> ReadOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "queries",
        "reads_from",
        "selects"
    };

    private static bool IsWriteOperationKind(string? operationKind)
        => !string.IsNullOrWhiteSpace(operationKind) && WriteOperationKinds.Contains(operationKind!);

    private static bool IsReadOperationKind(string? operationKind)
    {
        if (string.IsNullOrWhiteSpace(operationKind))
        {
            return true;
        }

        if (WriteOperationKinds.Contains(operationKind!))
        {
            return false;
        }

        if (ReadOperationKinds.Contains(operationKind!))
        {
            return true;
        }

        return string.Equals(operationKind, "read", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(operationKind, "query", StringComparison.OrdinalIgnoreCase);
    }

    public static string BuildFlows(GraphDocument document, Func<GraphNode, bool> controllerPredicate, FlowWorkspaceIndex? workspace = null, int? maxDepth = null)
    {
        // Core node index (unique IDs)
        var nodesById = new Dictionary<string, GraphNode>(document.Nodes.Count, StringComparer.Ordinal);
        foreach (var n in document.Nodes)
        {
            nodesById[n.Id] = n;
        }

        // Build outgoing adjacency without LINQ GroupBy to reduce transient allocations
        var edgesByFrom = new Dictionary<string, List<GraphEdge>>(StringComparer.Ordinal);
        // Also build incoming adjacency for inbound reachability
        var edgesByTo = new Dictionary<string, List<GraphEdge>>(StringComparer.Ordinal);
        foreach (var e in document.Edges)
        {
            if (!edgesByFrom.TryGetValue(e.From, out var listFrom))
            {
                listFrom = new List<GraphEdge>(4);
                edgesByFrom[e.From] = listFrom;
            }
            listFrom.Add(e);

            if (!edgesByTo.TryGetValue(e.To, out var listTo))
            {
                listTo = new List<GraphEdge>(2);
                edgesByTo[e.To] = listTo;
            }
            listTo.Add(e);
        }

        // Retain name/fqdn lookups (needed for implementation heuristics)
        var nodesByFqdn = new Dictionary<string, IReadOnlyList<GraphNode>>(StringComparer.OrdinalIgnoreCase);
        var fqdnGroups = new Dictionary<string, List<GraphNode>>(StringComparer.OrdinalIgnoreCase);
        foreach (var n in document.Nodes)
        {
            if (string.IsNullOrWhiteSpace(n.Fqdn)) continue;
            if (!fqdnGroups.TryGetValue(n.Fqdn!, out var list))
            {
                list = new List<GraphNode>(1);
                fqdnGroups[n.Fqdn!] = list;
            }
            list.Add(n);
        }
        foreach (var kv in fqdnGroups)
        {
            nodesByFqdn[kv.Key] = kv.Value;
        }

        var nodesByName = new Dictionary<string, IReadOnlyList<GraphNode>>(StringComparer.OrdinalIgnoreCase);
        var nameGroups = new Dictionary<string, List<GraphNode>>(StringComparer.OrdinalIgnoreCase);
        foreach (var n in document.Nodes)
        {
            if (string.IsNullOrWhiteSpace(n.Name)) continue;
            if (!nameGroups.TryGetValue(n.Name!, out var list))
            {
                list = new List<GraphNode>(1);
                nameGroups[n.Name!] = list;
            }
            list.Add(n);
        }
        foreach (var kv in nameGroups)
        {
            nodesByName[kv.Key] = kv.Value;
        }

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
            // Reachability (outbound from controller)
            var outward = CollectReachable(controller.Id, edgesByFrom);
            // Inbound (who references controller) – may surface upstream context if needed later
            var inbound = CollectInbound(controller.Id, edgesByTo);
            // Union for allowed expansion set
            var allowed = new HashSet<string>(outward.Count + inbound.Count, StringComparer.Ordinal);
            foreach (var id in outward) allowed.Add(id);
            foreach (var id in inbound) allowed.Add(id);

            var state = new FlowRenderState(document, nodesById, edgesByFrom, nodesByFqdn, nodesByName, mapLookup, workspace, maxDepth)
            {
                AllowedIds = allowed
            };
            AppendControllerFlow(builder, state, controller);
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static HashSet<string> CollectReachable(string rootId, Dictionary<string, List<GraphEdge>> edgesByFrom)
    {
        var visited = new HashSet<string>(StringComparer.Ordinal) { rootId };
        var queue = new Queue<string>();
        queue.Enqueue(rootId);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!edgesByFrom.TryGetValue(current, out var list)) continue;
            foreach (var e in list)
            {
                if (visited.Add(e.To)) queue.Enqueue(e.To);
            }
        }
        return visited;
    }

    private static HashSet<string> CollectInbound(string rootId, Dictionary<string, List<GraphEdge>> edgesByTo)
    {
        var visited = new HashSet<string>(StringComparer.Ordinal) { rootId };
        var queue = new Queue<string>();
        queue.Enqueue(rootId);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!edgesByTo.TryGetValue(current, out var incoming)) continue;
            foreach (var e in incoming)
            {
                if (visited.Add(e.From)) queue.Enqueue(e.From);
            }
        }
        return visited;
    }

    private static string BuildCallDedupKey(GraphEdge edge, GraphNode target, string? method)
    {
        var methodKey = string.IsNullOrWhiteSpace(method) ? "*" : method.Trim();
        var location = edge.Transform?.Location;
        var lineKey = location?.Line.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "*";
        var fileKey = location?.File ?? string.Empty;
        var ilRange = edge.Transform?.IlRange;
        var ilStart = ilRange?.StartOffset.HasValue == true
            ? ilRange.StartOffset.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
            : "*";
        var ilEnd = ilRange?.EndOffset.HasValue == true
            ? ilRange.EndOffset.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
            : "*";
        return $"{edge.From}->{target.Id}::{edge.Source}::{methodKey}::{fileKey}::{lineKey}::{ilStart}-{ilEnd}";
    }

    private static string GetDisplayName(GraphNode node)
    {
        if (!string.IsNullOrWhiteSpace(node.Name))
        {
            return node.Name!;
        }

        if (!string.IsNullOrWhiteSpace(node.Fqdn))
        {
            return node.Fqdn!;
        }

        return node.Id;
    }

    private static bool IsMutationVerb(string verb)
        => !string.Equals(verb, "GET", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "HEAD", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "OPTIONS", StringComparison.OrdinalIgnoreCase);

    private static string? ExtractHost(string? baseUrl, string? route)
    {
        if (!string.IsNullOrWhiteSpace(baseUrl) && Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri))
        {
            return baseUri.Host.ToLowerInvariant();
        }

        if (!string.IsNullOrWhiteSpace(route) && Uri.TryCreate(route, UriKind.Absolute, out var routeUri))
        {
            return routeUri.Host.ToLowerInvariant();
        }

        return null;
    }

    private static string DetermineRemoteScope(string? host, string label, string callerRoot)
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            return string.Equals(label, callerRoot, StringComparison.OrdinalIgnoreCase) ? "internal" : "service";
        }

        var lowered = host.ToLowerInvariant();
        if (lowered.Contains("localhost", StringComparison.Ordinal) ||
            lowered.StartsWith("127.", StringComparison.Ordinal) ||
            lowered.StartsWith("10.", StringComparison.Ordinal) ||
            lowered.StartsWith("192.168.", StringComparison.Ordinal) ||
            IsPrivate172(lowered))
        {
            return "internal";
        }

        if (lowered.EndsWith(".internal", StringComparison.Ordinal) ||
            lowered.EndsWith(".local", StringComparison.Ordinal) ||
            lowered.EndsWith(".svc", StringComparison.Ordinal))
        {
            return "internal";
        }

        if (!string.IsNullOrWhiteSpace(callerRoot) && lowered.Contains(callerRoot.ToLowerInvariant(), StringComparison.Ordinal))
        {
            return "internal";
        }

        return "external";
    }

    private static bool IsPrivate172(string host)
    {
        if (!host.StartsWith("172.", StringComparison.Ordinal))
        {
            return false;
        }

        var parts = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            return false;
        }

        if (int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out var secondOctet))
        {
            return secondOctet >= 16 && secondOctet <= 31;
        }

        return false;
    }

    private static void AppendControllerFlow(StringBuilder builder, FlowRenderState state, GraphNode controller)
    {
        var impact = new ImpactAccumulator(GetAssemblyRoot(controller.Assembly));
        state.PushImpact(impact);
        try
        {
            AppendEndpointFlow(builder, state, controller, indent: 0);
            AppendImpactSummary(builder, impact);
        }
        finally
        {
            state.PopImpact();
        }
    }

    private static void AppendEndpointFlow(StringBuilder builder, FlowRenderState state, GraphNode endpoint, int indent)
    {
        if (state.AllowedIds != null && !state.AllowedIds.Contains(endpoint.Id))
        {
            return; // outside reachability scope
        }
        if (!state.EndpointStack.Add(endpoint.Id))
        {
            // Already in stack => recursion path
            AppendIndented(builder, indent, "endpoint_recursion_suppressed " + (endpoint.Fqdn ?? endpoint.Name ?? endpoint.Id));
            return;
        }

        // Global expansion dedup: if we've already fully expanded this endpoint earlier in the overall flow output,
        // emit only a summary header with "(see previous expansion)" to avoid repeated deep expansions across
        // multi-solution references. This preserves unbounded depth for first occurrence while preventing memory blow-up.
        state.RenderedEndpoints ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var previouslyRendered = !state.RenderedEndpoints.Add(endpoint.Id);

        try
        {
            var method = GetNodeProp(endpoint, "http_method") ?? "GET";
            var route = GetNodeProp(endpoint, "route") ?? "/";
            var span = endpoint.Span;
            var authAnnotation = BuildAuthorizationAnnotation(endpoint);
            string? statusCodes = null;
            if (endpoint.Props is { } ep && ep.TryGetValue("status_codes", out var scObj) && scObj is not null)
            {
                IEnumerable<string>? codes = scObj switch
                {
                    int[] ints => ints.Select(i => i.ToString()),
                    IEnumerable<int> intEnum => intEnum.Select(i => i.ToString()),
                    object[] objects => objects.Select(o => o?.ToString() ?? string.Empty),
                    IEnumerable<object> objEnum => objEnum.Select(o => o?.ToString() ?? string.Empty),
                    _ => null
                };
                if (codes is not null)
                {
                    statusCodes = string.Join(',', codes.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct());
                }
            }
            var statusText = string.IsNullOrWhiteSpace(statusCodes) ? string.Empty : $" status={statusCodes}";
            var simulateText = endpoint.Props is { } eprops && eprops.TryGetValue("simulation", out var simVal) && simVal is bool sb && sb ? " [simulate]" : string.Empty;
            var header = $"[web] {method} {route}  ({endpoint.Fqdn})  [L{span?.StartLine}–L{span?.EndLine}]{statusText}{authAnnotation}{simulateText}";

            if (previouslyRendered)
            {
                header += " (see previous expansion)";
            }
            if (indent <= 0) builder.AppendLine(header); else AppendIndented(builder, indent, header);

            if (previouslyRendered)
            {
                // Do not re-expand internals for previously rendered endpoint.
                return;
            }
            if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
            {
                AppendIndented(builder, indent + 1, "... (max depth reached)");
                return;
            }

            if (!state.EdgesByFrom.TryGetValue(endpoint.Id, out var edges))
            {
                return;
            }

            var childIndent = indent <= 0 ? 1 : indent + 1;

            // Configuration usages (uses_configuration edges)
            foreach (var configEdge in edges.Where(e => e.Kind == "uses_configuration"))
            {
                if (!state.NodesById.TryGetValue(configEdge.To, out var configNode))
                {
                    continue;
                }
                if (state.AllowedIds != null && !state.AllowedIds.Contains(configNode.Id)) continue;

                var key = configEdge.Props is { } cprops && cprops.TryGetValue("key", out var keyVal)
                    ? keyVal?.ToString()
                    : null;
                var accessor = configEdge.Props is { } cprops2 && cprops2.TryGetValue("accessor", out var accVal)
                    ? accVal?.ToString()
                    : null;
                var value = configEdge.Props is { } cprops3 && cprops3.TryGetValue("value", out var valVal)
                    ? valVal?.ToString()
                    : null;
                var lineText = configEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var detailParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
                if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
                var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
                var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
                AppendIndented(builder, childIndent, $"uses_configuration {details}{valueText}{lineText}");
            }

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
                state.CurrentImpact?.RecordValidator(GetDisplayName(validatorNode));
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
                var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
                var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
                var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
                var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
                state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
                if (!state.DedupRequests.Add("CACHE::" + cacheKey)) continue;
                AppendIndented(builder, childIndent, $"uses_cache {cacheNode.Name}{methodPart}{opPart}{keyPart}{lineText}");
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
                var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, childIndent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
                state.CurrentImpact?.RecordOption(GetDisplayName(optionsNode));
            }

            // Group repository calls: aggregate consecutive calls to same repository with list of methods
            var callEdges = edges.Where(e => e.Kind == "calls").ToList();
            var printedDirectCallKeys = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < callEdges.Count; i++)
            {
                var callEdge = callEdges[i];
                if (!state.NodesById.TryGetValue(callEdge.To, out var targetNode)) continue;
                var isRepo = targetNode.Type == "app.repository" || targetNode.Type == "repository";
                if (!isRepo)
                {
                    var callMethod = callEdge.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var dedupKey = BuildCallDedupKey(callEdge, targetNode, callMethod);
                    if (!printedDirectCallKeys.Add(dedupKey))
                    {
                        continue;
                    }
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = callEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, childIndent, $"calls {targetNode.Name}{serviceMethodText}{lineText}");
                    if (targetNode.Type == "app.repository" || targetNode.Type == "repository")
                    {
                        AppendRepositoryFlow(builder, state, targetNode, childIndent + 1);
                    }
                    continue;
                }

                var methods = new List<string>();
                int? firstLine = callEdge.Transform?.Location?.Line;
                int j = i;
                while (j < callEdges.Count)
                {
                    var ej = callEdges[j];
                    if (ej.To != callEdge.To) break;
                    var m = ej.Props is { } p && p.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                    if (!string.IsNullOrWhiteSpace(m)) methods.Add(m!);
                    if (!firstLine.HasValue && ej.Transform?.Location?.Line is int ln) firstLine = ln;
                    j++;
                }
                i = j - 1;
                var uniqueMethods = methods.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                if (uniqueMethods.Count <= 1)
                {
                    var callMethod = callEdge.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = callEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, childIndent, $"calls {targetNode.Name}{serviceMethodText}{lineText}");
                }
                else
                {
                    var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                    var lineTextGroup = firstLine.HasValue ? $" [L{firstLine}]" : string.Empty;
                    AppendIndented(builder, childIndent, $"calls {targetNode.Name}{methodsPart}{lineTextGroup}");
                }
                AppendRepositoryFlow(builder, state, targetNode, childIndent + 1);
            }

            foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
            {
                if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
                {
                    continue;
                }

                var lineText = dataEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var label = ExtractOperationLabel(dataEdge);
                AppendIndented(builder, childIndent, $"{label} {entityNode.Name}{lineText}");
                state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);

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

                if (IsInfrastructureNoiseService(serviceNode))
                {
                    // Skip verbose logger / mapper service contract expansion; logging & mapping already captured via 'logs' and 'maps_to'
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
                // Attempt collapse: single concrete implementation for interface service
                GraphNode? collapseImpl = null;
                bool collapse = false;
                if (serviceNode.Name is { } sname && sname.StartsWith("I", StringComparison.Ordinal) && serviceNode.Type == "app.service")
                {
                    var single = TryResolveSingleImplementation(state, endpoint, serviceNode);
                    if (single != null && single.Type == "app.service")
                    {
                        collapse = true;
                        collapseImpl = single;
                    }
                }

                // Specialized handling for generic controlled repositories: treat interface invocation as a direct repository call
                var isControlledRepoInterface = serviceNode.Name != null && serviceNode.Name.StartsWith("IControlledRepository<", StringComparison.Ordinal);
                if (isControlledRepoInterface)
                {
                    var repoImpl = TryResolveControlledRepository(state, endpoint, serviceNode) ?? collapseImpl ?? TryResolveSingleImplementation(state, endpoint, serviceNode);
                    if (repoImpl != null && repoImpl.Type is "app.repository" or "repository")
                    {
                        var alreadyPrinted = callEdges.Any(call => call.To == repoImpl.Id && string.Equals(GetCallMethod(call), serviceMethodName, StringComparison.OrdinalIgnoreCase));
                        if (!alreadyPrinted)
                        {
                            var repoMethodSuffix = string.IsNullOrWhiteSpace(serviceMethodName) ? string.Empty : $".{serviceMethodName}";
                            AppendIndented(builder, childIndent, $"calls {repoImpl.Name}{repoMethodSuffix}{lineText}");
                        }
                        AppendRepositoryFlow(builder, state, repoImpl, childIndent + 1);
                        continue; // Skip generic service expansion path
                    }
                }

                var printedName = collapse && collapseImpl != null ? collapseImpl.Name : serviceNode.Name;
                if (!string.IsNullOrWhiteSpace(printedName) && printedName.EndsWith('>') && printedName.Count(c => c == '<') == 0)
                {
                    // Defensive: trim dangling '>' that can appear if generic argument stripped earlier
                    printedName = printedName.TrimEnd('>');
                }
                state.CurrentImpact?.RecordServiceUsage(printedName ?? GetDisplayName(serviceNode));
                AppendIndented(builder, childIndent, $"uses_service {printedName}{suffix}{serviceLineText}");

                var nextIndent = childIndent + 1;
                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, childIndent + 1, $"method {serviceMethodName}{lineText}");
                    nextIndent = childIndent + 2;
                }
                if (collapse && collapseImpl != null)
                {
                    // Expand implementation (show header for clarity now that collapsed uses_service line renamed)
                    AppendServiceImplementationFlow(builder, state, endpoint, collapseImpl, serviceMethodName, nextIndent, heuristic: !state.EdgesByFrom.ContainsKey(collapseImpl.Id), suppressHeader: true);
                }
                else
                {
                    AppendServiceContractFlow(builder, state, endpoint, serviceNode, serviceMethodName, nextIndent);
                }
            }

                // service_located (service locator pattern usage) edges (self-referential by analyzer design)
            foreach (var locatedEdge in edges.Where(e => e.Kind == "service_located"))
            {
                if (!state.NodesById.TryGetValue(locatedEdge.To, out var locatedNode))
                {
                    continue;
                }
                var lineText = locatedEdge.Transform?.Location?.Line is int l ? $" [L{l}]" : string.Empty;
                var locatorMethod = locatedEdge.Props is { } lprops && lprops.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                var methodText = string.IsNullOrWhiteSpace(locatorMethod) ? string.Empty : $".{locatorMethod}";
                AppendIndented(builder, childIndent, $"service_located {locatedNode.Name}{methodText}{lineText}");
            }

            foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
            {
                if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
                {
                    continue;
                }

                var lineText = storageEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var value)
                    ? value?.ToString()
                    : null;
                var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
                AppendIndented(builder, childIndent, $"uses_storage {storageNode.Name}{methodSuffix}{lineText}");
                state.CurrentImpact?.RecordStorage(GetDisplayName(storageNode));
            }

            foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
            {
                if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode))
                {
                    continue;
                }

                var lineText = logEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var level = logEdge.Props is { } props && props.TryGetValue("level", out var levelValue)
                    ? levelValue?.ToString()
                    : null;
                var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
                AppendIndented(builder, childIndent, $"logs {loggerNode.Name}{levelText}{lineText}");
            }

            foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
            {
                if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode))
                {
                    continue;
                }

                var lineText = validationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var validationMethod = validationEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
                AppendIndented(builder, childIndent, $"validation {guardNode.Name}{methodText}{lineText}");
            }

            foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
            {
                if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode)) continue;

                // Skip duplicate sends/dispatch of same request at same line
                var requestKey = $"{requestNode.Id}:{requestEdge.Transform?.Location?.Line}";
                state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
                if (!state.DedupRequests.Add(requestKey)) continue;

                var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var responseType = requestEdge.Props is { } rprops && rprops.TryGetValue("response_type", out var rt) ? rt?.ToString() : null;
                var handlerName = string.Empty;
                if (state.EdgesByFrom.TryGetValue(requestNode.Id, out var downstream) && downstream.FirstOrDefault(e => e.Kind == "handled_by") is { } handled && state.NodesById.TryGetValue(handled.To, out var handlerNode))
                {
                    handlerName = handlerNode.Name;
                }
                var handlerPart = string.IsNullOrWhiteSpace(handlerName) ? string.Empty : $" -> {handlerName}";
                var responsePart = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" : {responseType}";
                var synthetic = string.Equals(requestEdge.Source, "synthetic", StringComparison.OrdinalIgnoreCase) && requestEdge.Transform?.Type == "requestprocessor.dispatch";
                var prefix = synthetic ? "dispatches" : "sends_request";
                AppendIndented(builder, childIndent, $"{prefix} {requestNode.Name}{handlerPart}{responsePart}{lineText}");
                state.CurrentImpact?.RecordRequest(GetDisplayName(requestNode));
                if (!string.IsNullOrWhiteSpace(handlerName))
                {
                    state.CurrentImpact?.RecordHandler(handlerName);
                }
                AppendCommandFlow(builder, state, requestNode, childIndent + 1);
            }

            foreach (var returnEdge in edges.Where(e => e.Kind == "returns"))
            {
                var annotation = returnEdge.Props is { } props && props.TryGetValue("kind", out var kindValue)
                    ? kindValue?.ToString()
                    : null;
                AppendMappingEdge(builder, state, returnEdge, childIndent, label: "returns", annotation: annotation);
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
                state.CurrentImpact?.RecordNotification(GetDisplayName(notificationNode));
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
        if (state.AllowedIds != null && !state.AllowedIds.Contains(destination.Id))
        {
            return; // not in reachability scope
        }

        var lineText = edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
        var variable = edge.Props is { } props && props.TryGetValue("variable", out var value)
            ? value?.ToString()
            : null;
        var dedupKey = edge.From + "::" + edge.To + "::" + (variable ?? string.Empty) + "::" + label + "::" + (annotation ?? string.Empty);
        if (!state.PrintedMappings.Add(dedupKey))
        {
            return; // suppress exact duplicate mapping emission within same flow render context
        }
        var variableText = string.IsNullOrWhiteSpace(variable) ? string.Empty : $" (var {variable})";
        var annotationText = string.IsNullOrWhiteSpace(annotation) ? string.Empty : $" [{annotation}]";
        AppendIndented(builder, indent, $"{label} {destination.Name}{variableText}{lineText}{annotationText}");
        state.CurrentImpact?.RecordMapping(GetDisplayName(destination));

        if (includeAutomapper)
        {
            AppendAutomapperRegistrations(builder, state, edge, indent + 1);
        }

        if (!state.EdgesByFrom.TryGetValue(destination.Id, out var downstreamEdges))
        {
            return;
        }
        // Prevent expansion of downstream if outside allowed set
    if (state.AllowedIds != null && !state.AllowedIds.Contains(destination.Id)) return;

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

        state.CurrentImpact?.RecordRequest(GetDisplayName(command));

        // Pipeline behaviors / request processors (processed_by edges)
        foreach (var pipelineEdge in edges.Where(e => e.Kind == "processed_by"))
        {
            if (!state.NodesById.TryGetValue(pipelineEdge.To, out var behaviorNode))
            {
                continue;
            }
            if (state.AllowedIds != null && !state.AllowedIds.Contains(behaviorNode.Id)) continue;

            var stage = behaviorNode.Props is { } bProps && bProps.TryGetValue("stage", out var stageVal)
                ? stageVal?.ToString()
                : null;
            var responseType = pipelineEdge.Props is { } peProps && peProps.TryGetValue("response_type", out var respVal)
                ? respVal?.ToString()
                : null;
            var stageText = string.IsNullOrWhiteSpace(stage) ? string.Empty : $" [{stage}]";
            var responseText = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" (response={responseType})";
            AppendIndented(builder, indent, $"processed_by {behaviorNode.Name}{stageText}{responseText}");
            state.CurrentImpact?.RecordPipelineBehavior(GetDisplayName(behaviorNode));
        }

        // If no concrete pipeline edges, attempt to list generic pipeline behaviors (nodes tagged generic_request)
        if (!edges.Any(e => e.Kind == "processed_by"))
        {
            var genericBehaviors = state.Document.Nodes
                .Where(n => n.Type == "cqrs.pipeline_behavior" && n.Props is { } p && p.TryGetValue("generic_request", out var gr) && gr is bool b && b)
                .ToList();
            if (genericBehaviors.Count > 0)
            {
                AppendIndented(builder, indent, $"generic_pipeline_behaviors {genericBehaviors.Count}");
                state.CurrentImpact?.RecordGenericPipelineBehaviors(genericBehaviors.Count);
                foreach (var gb in genericBehaviors.OrderBy(n => n.Name, StringComparer.OrdinalIgnoreCase).Take(5))
                {
                    AppendIndented(builder, indent + 1, $"{gb.Name}");
                }
                if (genericBehaviors.Count > 5)
                {
                    AppendIndented(builder, indent + 1, $"+{genericBehaviors.Count - 5} more");
                }
            }
        }

        var handlerEdges = edges.Where(e => e.Kind == "handled_by")
            .GroupBy(e => e.To, StringComparer.Ordinal)
            .Select(g => g.First());
        foreach (var handlerEdge in handlerEdges)
        {
            if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode)) continue;
            if (state.AllowedIds != null && !state.AllowedIds.Contains(handlerNode.Id)) continue;
            var span = handlerNode.Span;
            var handlerKey = $"{handlerNode.Id}:{span?.StartLine}:{span?.EndLine}";
            state.DedupHandlers ??= new HashSet<string>(StringComparer.Ordinal);
            if (!state.DedupHandlers.Add(handlerKey)) continue;
            AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
            state.CurrentImpact?.RecordHandler(GetDisplayName(handlerNode));
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
            if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
            {
                AppendIndented(builder, indent, "... (max depth reached)");
                return;
            }
            if (!state.EdgesByFrom.TryGetValue(handler.Id, out var edges))
            {
                return;
            }

            // Configuration for handler
            foreach (var configEdge in edges.Where(e => e.Kind == "uses_configuration"))
            {
                if (!state.NodesById.TryGetValue(configEdge.To, out var configNode))
                {
                    continue;
                }

                var key = configEdge.Props is { } cprops && cprops.TryGetValue("key", out var keyVal)
                    ? keyVal?.ToString()
                    : null;
                var accessor = configEdge.Props is { } cprops2 && cprops2.TryGetValue("accessor", out var accVal)
                    ? accVal?.ToString()
                    : null;
                var value = configEdge.Props is { } cprops3 && cprops3.TryGetValue("value", out var valVal)
                    ? valVal?.ToString()
                    : null;
                var lineText = configEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var detailParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
                if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
                var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
                var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
                AppendIndented(builder, indent, $"uses_configuration {details}{valueText}{lineText}");
            }

            // Group repository calls in handler
            var handlerCallEdges = edges.Where(e => e.Kind == "calls").ToList();
            var printedHandlerCallKeys = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < handlerCallEdges.Count; i++)
            {
                var call = handlerCallEdges[i];
                if (!state.NodesById.TryGetValue(call.To, out var target)) continue;
                if (state.AllowedIds is { } allow && !allow.Contains(target.Id)) continue;
                var isRepo = target.Type == "app.repository" || target.Type == "repository";
                if (!isRepo)
                {
                    var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var dedupKey = BuildCallDedupKey(call, target, callMethod);
                    if (!printedHandlerCallKeys.Add(dedupKey))
                    {
                        continue;
                    }
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                    continue;
                }
                var methods = new List<string>();
                int? firstLine = call.Transform?.Location?.Line;
                int j = i;
                while (j < handlerCallEdges.Count)
                {
                    var ej = handlerCallEdges[j];
                    if (ej.To != call.To) break;
                    var m = ej.Props is { } p && p.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                    if (!string.IsNullOrWhiteSpace(m)) methods.Add(m!);
                    if (!firstLine.HasValue && ej.Transform?.Location?.Line is int ln) firstLine = ln;
                    j++;
                }
                i = j - 1;
                var uniqueMethods = methods.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                if (uniqueMethods.Count <= 1)
                {
                    var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                }
                else
                {
                    var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                    var lineTextGroup = firstLine.HasValue ? $" [L{firstLine}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{methodsPart}{lineTextGroup}");
                }
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
                if (state.AllowedIds is { } allow && !allow.Contains(clientNode.Id)) continue;

                AppendHttpClientUsage(builder, state, clientEdge, clientNode, indent);
            }

            foreach (var service in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!state.NodesById.TryGetValue(service.To, out var serviceNode))
                {
                    continue;
                }
                if (state.AllowedIds is { } allow && !allow.Contains(serviceNode.Id)) continue;

                if (IsInfrastructureNoiseService(serviceNode))
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

                var nextIndent = indent + 1;
                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, indent + 1, $"method {serviceMethodName}{lineText}");
                    nextIndent = indent + 2;
                }

                AppendServiceContractFlow(builder, state, handler, serviceNode, serviceMethodName, nextIndent);
            }

            foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
            {
                if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
                {
                    continue;
                }

                var lineText = storageEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var value)
                    ? value?.ToString()
                    : null;
                var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
                AppendIndented(builder, indent, $"uses_storage {storageNode.Name}{methodSuffix}{lineText}");
            }

            foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
            {
                if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode))
                {
                    continue;
                }

                var lineText = logEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var level = logEdge.Props is { } props && props.TryGetValue("level", out var levelValue)
                    ? levelValue?.ToString()
                    : null;
                var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
                AppendIndented(builder, indent, $"logs {loggerNode.Name}{levelText}{lineText}");
            }

            foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
            {
                if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode))
                {
                    continue;
                }

                var lineText = validationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var validationMethod = validationEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
                AppendIndented(builder, indent, $"validation {guardNode.Name}{methodText}{lineText}");
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
                var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
                var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
                var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
                var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
                state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
                if (!state.DedupRequests.Add("CACHE::" + cacheKey)) continue;
                AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{methodPart}{opPart}{keyPart}{lineText}");
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
                state.CurrentImpact?.RecordMessage(GetDisplayName(messageNode));
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

        // Transaction inference: if multiple write operations, annotate transaction
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

            var lineText = write.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var operation = ExtractOperationLabel(write);
            AppendIndented(builder, indent, $"{operation} {entityNode.Name}{lineText}");
            state.CurrentImpact?.RecordRepositoryOperation(GetDisplayName(repository), write.Kind, GetDisplayName(entityNode));
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
            var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
            state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
            if (!state.DedupRequests.Add("CACHE::" + cacheKey)) continue;
            AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{methodPart}{opPart}{keyPart}{lineText}");
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
            var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
            state.CurrentImpact?.RecordOption(GetDisplayName(optionsNode));
        }
    }

    private static void AppendEntityFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode entity,
        int indent)
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

        foreach (var tableEdge in edges.Where(e => e.Kind is "writes_to" or "reads_from" or "queries" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
        {
            if (!state.NodesById.TryGetValue(tableEdge.To, out var tableNode))
            {
                continue;
            }

            var transform = tableEdge.Kind == "reads_from"
                ? "reads_from"
                : ExtractOperationLabel(tableEdge);
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

        // Determine caller root (edge.From is the node performing mapping)
        string? callerRoot = null;
        if (state.NodesById.TryGetValue(edge.From, out var callerNode))
        {
            callerRoot = GetAssemblyRoot(callerNode.Assembly);
        }

        IEnumerable<GraphNode> filtered = maps;
        if (!string.IsNullOrWhiteSpace(callerRoot))
        {
            var sameRoot = maps.Where(m => string.Equals(GetAssemblyRoot(m.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
            if (sameRoot.Count > 0)
            {
                filtered = sameRoot;
            }
            else
            {
                // Fallback: keep those with concrete source files
                var withFiles = maps.Where(m => !string.IsNullOrWhiteSpace(m.FilePath) && !m.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase)).ToList();
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
            AppendIndented(builder, indent, $"automapper.registration {profileName} ({mapLabel}) [L{mapNode.Span?.StartLine}]");
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
            // Disabled AppendIndented(builder, indent, $"automapper.registrations_elided {elided.Count}{rootSummary}");
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
        var clientDisplay = GetDisplayName(clientNode);
        AppendIndented(builder, indent, $"uses_client {clientDisplay}{lineText}");
        state.CurrentImpact?.RecordClient(clientDisplay);

        List<string>? allowedMethods = null;
        if (clientEdge.Props is { } clientProps)
        {
            if (clientProps.TryGetValue("method", out var methodValue) && methodValue is not null)
            {
                var methodName = methodValue.ToString()?.Trim();
                if (!string.IsNullOrWhiteSpace(methodName))
                {
                    allowedMethods ??= new List<string>();
                    allowedMethods.Add(methodName!);
                }
            }

            if (clientProps.TryGetValue("methods", out var methodsValue) && methodsValue is IEnumerable<object> methodList)
            {
                foreach (var candidate in methodList)
                {
                    var text = candidate?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        continue;
                    }

                    allowedMethods ??= new List<string>();
                    allowedMethods.Add(text!);
                }
            }
        }

        if (!state.EdgesByFrom.TryGetValue(clientNode.Id, out var clientEdges))
        {
            return;
        }

        // Candidate call edges
        var allCallEdges = clientEdges.Where(e => e.Kind == "calls").ToList();
        // Restrict by allowed methods if provided
        if (allowedMethods is { Count: > 0 })
        {
            allCallEdges = allCallEdges.Where(e =>
            {
                if (e.Props is not { } p) return false;
                if (p.TryGetValue("client_method", out var mv) && mv is string ms && allowedMethods.Any(m => string.Equals(m, ms.Trim(), StringComparison.OrdinalIgnoreCase))) return true;
                if (p.TryGetValue("method", out var mv2) && mv2 is string ms2 && allowedMethods.Any(m => string.Equals(m, ms2.Trim(), StringComparison.OrdinalIgnoreCase))) return true;
                return false;
            }).ToList();
        }

        // Focus on invocation line: only edges whose transform line matches the client usage line
        if (clientEdge.Transform?.Location?.Line is int invocationLine)
        {
            var sameLine = allCallEdges.Where(e => e.Transform?.Location?.Line == invocationLine).ToList();
            if (sameLine.Count > 0)
            {
                allCallEdges = sameLine;
            }
            else
            {
                // Fallback ±1 line tolerance if no exact match
                var nearLine = allCallEdges.Where(e => e.Transform?.Location?.Line is int l && Math.Abs(l - invocationLine) <= 1).ToList();
                if (nearLine.Count > 0) allCallEdges = nearLine;
            }
        }

        // Distinct by method + verb + route + target_service to collapse duplicates
        var distinctCalls = allCallEdges
            .Select(e => new
            {
                Edge = e,
                Method = ExtractProp(e, "client_method") ?? ExtractProp(e, "method") ?? e.Props?.GetValueOrDefault("method")?.ToString() ?? string.Empty,
                Verb = ExtractProp(e, "verb") ?? string.Empty,
                Route = ExtractProp(e, "route") ?? string.Empty,
                TargetService = ExtractProp(e, "target_service") ?? string.Empty
            })
            .GroupBy(x => (x.Method, x.Verb, x.Route, x.TargetService))
            .Select(g => g.First())
            .ToList();

        foreach (var call in distinctCalls)
        {
            var callEdge = call.Edge;
            var baseUrlValue = callEdge.Props is { } baseProps && baseProps.TryGetValue("base_url", out var baseObj)
                ? baseObj?.ToString()
                : null;
            state.CurrentImpact?.RecordRemoteCall(clientDisplay, call.Verb, call.Route, baseUrlValue, call.TargetService);
        }

        const int ExplosionThreshold = 25;
        if (distinctCalls.Count > ExplosionThreshold)
        {
            AppendIndented(builder, indent + 1, $"calls {clientNode.Name} (distinct_methods={distinctCalls.Count}) [elided]");
            foreach (var sample in distinctCalls.Take(10))
            {
                var sampleLine = sample.Edge.Transform?.Location?.Line is int sl ? $" [L{sl}]" : string.Empty;
                var detailParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(sample.Verb) && !string.IsNullOrWhiteSpace(sample.Route)) detailParts.Add($"{sample.Verb} {sample.Route}");
                if (!string.IsNullOrWhiteSpace(sample.Method)) detailParts.Add(sample.Method);
                if (!string.IsNullOrWhiteSpace(sample.TargetService)) detailParts.Add($"target={sample.TargetService}");
                var detail = detailParts.Count > 0 ? " (" + string.Join(", ", detailParts) + ")" : string.Empty;
                AppendIndented(builder, indent + 2, $"calls{detail}{sampleLine}");
            }
            var remaining = distinctCalls.Count - 10;
            if (remaining > 0)
            {
                AppendIndented(builder, indent + 2, $"+{remaining} more");
            }
            // Skip deeper remote expansion in summary mode
            return;
        }

        foreach (var call in distinctCalls)
        {
            var callEdge = call.Edge;
            if (!state.NodesById.TryGetValue(callEdge.To, out var targetNode)) continue;
            var verb = call.Verb;
            var route = call.Route;
            var baseUrl = callEdge.Props is { } p3 && p3.TryGetValue("base_url", out var b) ? b?.ToString() : null;
            var configKey = callEdge.Props is { } p4 && p4.TryGetValue("configuration_key", out var c) ? c?.ToString() : null;
            var targetService = call.TargetService;
            var queryParams = callEdge.Props is { } p5 && p5.TryGetValue("query_params", out var qObj) && qObj is IEnumerable<object> rawParams
                ? rawParams.Select(x => x?.ToString()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList()
                : null;
            var details = new List<string>();
            if (!string.IsNullOrWhiteSpace(verb) && !string.IsNullOrWhiteSpace(route)) details.Add($"{verb} {route}");
            if (!string.IsNullOrWhiteSpace(call.Method)) details.Add($"method={call.Method}");
            if (!string.IsNullOrWhiteSpace(baseUrl)) details.Add($"base={baseUrl}");
            if (!string.IsNullOrWhiteSpace(configKey)) details.Add($"config={configKey}");
            if (!string.IsNullOrWhiteSpace(targetService)) details.Add($"target={targetService}");
            if (queryParams is { Count: > 0 }) details.Add($"query={string.Join('&', queryParams)}");
            var detailText = details.Count > 0 ? $" ({string.Join(", ", details)})" : string.Empty;
            var callLineText = callEdge.Transform?.Location?.Line is int callLine ? $" [L{callLine}]" : string.Empty;
            AppendIndented(builder, indent + 1, $"calls {targetNode.Name}{detailText}{callLineText}");
            var expKey = clientNode.Id + "::" + (targetService ?? "*") + "::" + (verb ?? "*") + "::" + (route ?? "*");
            if (!state.HttpClientExpansionKeys.Add(expKey))
            {
                AppendIndented(builder, indent + 2, "remote_endpoint_expansion_suppressed (see previous expansion)");
                continue;
            }
            AppendTargetServiceFlow(builder, state, callEdge, indent + 2);
        }

        // If we printed only the client usage and either there are no call edges or none include route/verb, annotate gap.
        var callEdges = clientEdges.Where(e => e.Kind == "calls").ToList();
        var anyCallEdges = callEdges.Count > 0;
        var anyCallWithMetadata = callEdges.Any(e => e.Props is { } cp && (cp.ContainsKey("route") || cp.ContainsKey("verb")));
        if (!anyCallEdges || !anyCallWithMetadata)
        {
            AppendIndented(builder, indent + 1, "remote_endpoint_metadata_missing (no route/verb captured for client calls)");
        }
    }

    private static string? ExtractProp(GraphEdge edge, string key)
    {
        if (edge.Props is not { } props) return null;
        return props.TryGetValue(key, out var value) ? value?.ToString() : null;
    }

    private static void AppendTargetServiceFlow(StringBuilder builder, FlowRenderState state, GraphEdge callEdge, int indent)
    {
        if (state.Workspace is null)
        {
            return;
        }

        if (callEdge.Props is not { } props)
        {
            return;
        }

        // Route/verb extracted regardless of target_service so we can attempt global matching.
        var route = props.TryGetValue("route", out var routeValue) ? routeValue?.ToString() : null;
        var verb = props.TryGetValue("verb", out var verbValue) ? verbValue?.ToString() : null;

        // If target_service is present we use existing assembly mapping logic; otherwise attempt global match.
        var serviceName = props.TryGetValue("target_service", out var serviceValue) ? serviceValue?.ToString() : null;

        if (string.IsNullOrWhiteSpace(serviceName))
        {
            // No explicit target service; if we have route/verb attempt a global endpoint match.
            if (string.IsNullOrWhiteSpace(route) && string.IsNullOrWhiteSpace(verb))
            {
                return; // Nothing to resolve.
            }

            var routeText = route ?? string.Empty;
            var verbText = verb ?? string.Empty;
            var lookupKey = $"lookup::{callEdge.From}::{verbText}::{routeText}";
            if (!state.RemoteLookupKeys.Add(lookupKey))
            {
                AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText} (see previous lookup)");
                return;
            }

            var globalCandidates = state.Document.Nodes
                .Where(n => n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                .ToList();
            var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, route, verb);
            AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}");
            if (globalMatched.Count == 0)
            {
                AppendIndented(builder, indent + 1, $"unmatched_endpoint route={routeText} verb={verbText}");
                return;
            }

            foreach (var endpoint in globalMatched)
            {
                AppendEndpointFlow(builder, state, endpoint, indent + 1);
            }
            return;
        }

        if (!state.Workspace.TryGetAssemblies(serviceName, out var assemblies) || assemblies.Count == 0)
        {
            // No explicit assemblies mapped. Still attempt a global endpoint match so we can
            // provide value even before workspace config is completed.
            AppendIndented(builder, indent, $"target_service {serviceName}");

            var globalCandidates = state.Document.Nodes
                .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api"))
                .ToList();
            var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, route, verb);
            if (globalMatched.Count > 0)
            {
                AppendIndented(builder, indent + 1, "fallback_global_endpoint_match (no assemblies mapped)");
                foreach (var endpoint in globalMatched)
                {
                    AppendEndpointFlow(builder, state, endpoint, indent + 2);
                }
            }
            else
            {
                AppendIndented(builder, indent + 1, "unresolved_target_service (no assemblies mapped)");
            }
            return;
        }

        var assemblySet = assemblies is HashSet<string> set
            ? set
            : new HashSet<string>(assemblies, StringComparer.OrdinalIgnoreCase);

        var key = $"{callEdge.From}->{serviceName}:{route}:{verb}";
        if (!state.TargetServiceVisited.Add(key))
        {
            // Summarize rather than fully re-expand
            AppendIndented(builder, indent, $"target_service {serviceName} (see previous expansion)");
            return;
        }

        var candidates = state.Document.Nodes
            .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                        && assemblySet.Contains(n.Assembly))
            .ToList();

        AppendIndented(builder, indent, $"target_service {serviceName}");

        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent + 1, "... (max depth reached)");
            return;
        }

        if (candidates.Count == 0)
        {
            AppendIndented(builder, indent + 1, "unresolved_target_service (no endpoints in mapped assemblies)");
            return;
        }

        var matched = FilterEndpointsByRouteAndVerb(candidates, route, verb);
        if (matched.Count == 0)
        {
            // Fallback: global search across all endpoints if specific assembly match failed
            var globalCandidates = state.Document.Nodes
                .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api"))
                .ToList();
            var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, route, verb);
            if (globalMatched.Count > 0)
            {
                AppendIndented(builder, indent + 1, "fallback_global_endpoint_match");
                foreach (var endpoint in globalMatched)
                {
                    AppendEndpointFlow(builder, state, endpoint, indent + 2);
                }
                return;
            }

            AppendIndented(builder, indent + 1, $"unmatched_endpoint route={route} verb={verb}");
            return;
        }

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
        static bool MatchesRoute(string? callRoute, string? endpointRoute)
        {
            var callCanonical = CanonicalizeRoute(callRoute);
            var endpointCanonical = CanonicalizeRoute(endpointRoute);
            return !string.IsNullOrWhiteSpace(callCanonical) &&
                   !string.IsNullOrWhiteSpace(endpointCanonical) &&
                   string.Equals(callCanonical, endpointCanonical, StringComparison.OrdinalIgnoreCase);
        }

        static bool MatchesVerb(string? expected, string? actual)
            => !string.IsNullOrWhiteSpace(expected) && !string.IsNullOrWhiteSpace(actual)
               && string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(route) && !string.IsNullOrWhiteSpace(verb))
        {
            var both = candidates
                .Where(n => MatchesRoute(route, GetNodeProp(n, "route")) && MatchesVerb(verb, GetNodeProp(n, "http_method")))
                .ToList();
            if (both.Count > 0)
            {
                return both;
            }
        }

        if (!string.IsNullOrWhiteSpace(route))
        {
            var routeOnly = candidates
                .Where(n => MatchesRoute(route, GetNodeProp(n, "route")))
                .ToList();
            if (routeOnly.Count > 0)
            {
                return routeOnly;
            }
        }

        if (!string.IsNullOrWhiteSpace(verb))
        {
            var verbOnly = candidates
                .Where(n => MatchesVerb(verb, GetNodeProp(n, "http_method")))
                .ToList();
            if (verbOnly.Count > 0)
            {
                return verbOnly;
            }
        }

        return candidates;
    }

    private static GraphNode? TryResolveSingleImplementation(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
    {
        var candidates = new List<GraphNode>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        if (state.EdgesByFrom.TryGetValue(serviceNode.Id, out var edges))
        {
            foreach (var edge in edges.Where(e => e.Kind == "implemented_by"))
            {
                if (!state.NodesById.TryGetValue(edge.To, out var impl)) continue;
                if (!ShouldIncludeImplementation(caller, impl)) continue;
                if (seen.Add(impl.Id)) candidates.Add(impl);
            }
        }
        if (candidates.Count == 0)
        {
            foreach (var c in state.FindCandidateImplementations(serviceNode))
            {
                if (!ShouldIncludeImplementation(caller, c)) continue;
                if (seen.Add(c.Id)) candidates.Add(c);
            }
        }
        if (candidates.Count == 0)
        {
            foreach (var c in state.FindHeuristicImplementations(serviceNode))
            {
                if (!ShouldIncludeImplementation(caller, c)) continue;
                if (seen.Add(c.Id)) candidates.Add(c);
            }
        }
        if (candidates.Count == 0) return null;

        var callerRoot = GetAssemblyRoot(caller.Assembly);
        var sameRoot = candidates.Where(c => string.Equals(GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
        if (sameRoot.Count > 0) candidates = sameRoot;

        var grouped = candidates
            .GroupBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.OrderByDescending(c => state.EdgesByFrom.ContainsKey(c.Id)).ThenBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase).First())
            .ToList();
        return grouped.Count == 1 ? grouped[0] : null;
    }

    private static GraphNode? TryResolveControlledRepository(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
    {
        var name = serviceNode.Name ?? serviceNode.Fqdn ?? string.Empty;
        var genericStart = name.IndexOf('<');
        var genericEnd = name.LastIndexOf('>');
        if (genericStart < 0 || genericEnd <= genericStart + 1)
        {
            return null;
        }

        var genericArgument = name.Substring(genericStart + 1, genericEnd - genericStart - 1).Trim();
        if (string.IsNullOrWhiteSpace(genericArgument))
        {
            return null;
        }

        var simpleArg = genericArgument.Split('.').Last();
        if (string.IsNullOrWhiteSpace(simpleArg))
        {
            return null;
        }

        var candidateNames = new[]
        {
            simpleArg + "Repository",
            simpleArg + "ControlledRepository",
        };

        GraphNode? best = null;
        var callerRoot = GetAssemblyRoot(caller.Assembly);

        foreach (var candidateName in candidateNames)
        {
            if (!state.NodesByName.TryGetValue(candidateName, out var matches))
            {
                continue;
            }

            var repositories = matches
                .Where(n => n.Type is "app.repository" or "repository")
                .ToList();

            repositories = repositories
                .Where(r => IsWithinCallerSolution(caller, r))
                .ToList();

            if (repositories.Count == 0)
            {
                continue;
            }

            if (repositories.Count == 1)
            {
                return repositories[0];
            }

            var sameRoot = repositories
                .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (sameRoot.Count == 1)
            {
                return sameRoot[0];
            }

            if (sameRoot.Count > 1)
            {
                best ??= sameRoot
                    .OrderBy(r => r.Fqdn ?? r.Name, StringComparer.OrdinalIgnoreCase)
                    .First();
            }
            else
            {
                best ??= repositories
                    .OrderBy(r => r.Fqdn ?? r.Name, StringComparer.OrdinalIgnoreCase)
                    .First();
            }
        }

        return best;
    }

    private static string? GetCallMethod(GraphEdge edge)
    {
        return edge.Props is { } props && props.TryGetValue("method", out var methodValue)
            ? methodValue?.ToString()
            : null;
    }

    private static bool IsInfrastructureNoiseService(GraphNode serviceNode)
    {
        var name = serviceNode.Name ?? string.Empty;
        var fqdn = serviceNode.Fqdn ?? string.Empty;

        if (name.StartsWith("ILogger<", StringComparison.Ordinal) || fqdn.StartsWith("Microsoft.Extensions.Logging.ILogger<", StringComparison.Ordinal))
            return true;
        if (string.Equals(name, "ILogger", StringComparison.Ordinal) || fqdn.Contains("Microsoft.Extensions.Logging.ILogger", StringComparison.Ordinal))
            return true;
        if (string.Equals(name, "IMapper", StringComparison.Ordinal) || fqdn.Contains("AutoMapper.IMapper", StringComparison.Ordinal))
            return true;
        // Additional pseudo / cross-cutting service suppressions (these typically do not add domain semantics in flow output)
        if (name is "ITimeProvider" or "IDateTimeProvider" or "IDateTime" or "IClock" || fqdn.Contains("TimeProvider", StringComparison.Ordinal))
            return true;
        if (name is "IGuidGenerator" or "ICorrelationIdAccessor" or "ICorrelationContextAccessor")
            return true;
        if (name is "ICurrentUserService" or "ICurrentUser" or "IUserContext" or "IUserAccessor")
            return true;
        if (name is "ITelemetryClient" or "ITelemetry" || fqdn.Contains("TelemetryClient", StringComparison.Ordinal))
            return true;
        if (name is "ITracer" || fqdn.Contains("Tracing", StringComparison.Ordinal))
            return true;
        if (name is "IDistributedCache" || fqdn.Contains("Microsoft.Extensions.Caching.Distributed.IDistributedCache", StringComparison.Ordinal))
            return true;
        if (string.Equals(name, "IHttpContextAccessor", StringComparison.Ordinal) || fqdn.Contains("Microsoft.AspNetCore.Http.IHttpContextAccessor", StringComparison.Ordinal))
            return true;
        return false;
    }

    private static void AppendGenericServiceNode(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode node,
        string? invokedMethod,
        int indent,
        bool suppressSelfHeuristic = false)
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }

        if (!state.EdgesByFrom.TryGetValue(node.Id, out var edges) || edges.Count == 0)
        {
            if (!suppressSelfHeuristic && !string.IsNullOrWhiteSpace(invokedMethod))
            {
                AppendIndented(builder, indent, $"implementation {node.Fqdn}.{invokedMethod} [heuristic:extension-or-external]");
            }
            return;
        }

        var printedServiceCallKeys = new HashSet<string>(StringComparer.Ordinal);

        // Configuration usages
        foreach (var configEdge in edges.Where(e => e.Kind == "uses_configuration"))
        {
            if (!state.NodesById.TryGetValue(configEdge.To, out var configNode)) continue;
            var key = configEdge.Props is { } cprops && cprops.TryGetValue("key", out var keyVal) ? keyVal?.ToString() : null;
            var accessor = configEdge.Props is { } cprops2 && cprops2.TryGetValue("accessor", out var accVal) ? accVal?.ToString() : null;
            var value = configEdge.Props is { } cprops3 && cprops3.TryGetValue("value", out var valVal) ? valVal?.ToString() : null;
            var lineText = configEdge.Transform?.Location?.Line is int cl ? $" [L{cl}]" : string.Empty;
            var detailParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
            if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
            var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
            var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
            AppendIndented(builder, indent, $"uses_configuration {details}{valueText}{lineText}");
        }

        // Group repository calls while honoring invokedMethod filter
        var filteredCalls = edges.Where(e => e.Kind == "calls" && EdgeMatchesMethod(invokedMethod, e)).ToList();
        for (int i = 0; i < filteredCalls.Count; i++)
        {
            var callEdge = filteredCalls[i];
            if (!state.NodesById.TryGetValue(callEdge.To, out var targetNode)) continue;

            var isRepo = targetNode.Type == "app.repository" || targetNode.Type == "repository";
            if (!isRepo)
            {
                var callMethod = callEdge.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                var dedupKey = BuildCallDedupKey(callEdge, targetNode, callMethod);
                if (!printedServiceCallKeys.Add(dedupKey))
                {
                    continue;
                }
                var methodSuffix = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = callEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {targetNode.Name}{methodSuffix}{lineText}");
                if (targetNode.Type == "cqrs.request")
                {
                    AppendCommandFlow(builder, state, targetNode, indent + 1);
                }
                continue;
            }

            var methods = new List<string>();
            int? firstLine = callEdge.Transform?.Location?.Line;
            int j = i;
            while (j < filteredCalls.Count)
            {
                var ej = filteredCalls[j];
                if (ej.To != callEdge.To) break;
                var m = ej.Props is { } p && p.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                if (!string.IsNullOrWhiteSpace(m)) methods.Add(m!);
                if (!firstLine.HasValue && ej.Transform?.Location?.Line is int ln) firstLine = ln;
                j++;
            }
            i = j - 1;
            var uniqueMethods = methods.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            if (uniqueMethods.Count <= 1)
            {
                var callMethod = callEdge.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                var methodSuffix = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                var lineText = callEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {targetNode.Name}{methodSuffix}{lineText}");
            }
            else
            {
                var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                var lineTextGroup = firstLine.HasValue ? $" [L{firstLine}]" : string.Empty;
                AppendIndented(builder, indent, $"calls {targetNode.Name}{methodsPart}{lineTextGroup}");
            }
            AppendRepositoryFlow(builder, state, targetNode, indent + 1);
        }

        foreach (var clientEdge in edges.Where(e => e.Kind == "uses_client"))
        {
            if (invokedMethod == null && !EdgeMatchesMethod(invokedMethod, clientEdge)) continue;

            if (!state.NodesById.TryGetValue(clientEdge.To, out var clientNode))
            {
                continue;
            }

            AppendHttpClientUsage(builder, state, clientEdge, clientNode, indent);
        }

        foreach (var serviceEdge in edges.Where(e => e.Kind == "uses_service"))
        {
            if (invokedMethod != null && !EdgeMatchesMethod(invokedMethod, serviceEdge)) continue;
            if (invokedMethod == null && !EdgeMatchesMethod(invokedMethod, serviceEdge)) continue;

            if (!state.NodesById.TryGetValue(serviceEdge.To, out var serviceNode))
            {
                continue;
            }

            if (IsInfrastructureNoiseService(serviceNode))
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
            AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{serviceLineText}");

            var nextIndent = indent + 1;
            if (!string.IsNullOrWhiteSpace(serviceMethodName))
            {
                AppendIndented(builder, indent + 1, $"method {serviceMethodName}{lineText}");
                nextIndent = indent + 2;
            }

            AppendServiceContractFlow(builder, state, node, serviceNode, serviceMethodName, nextIndent);
        }

        foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
        {
            if (invokedMethod != null && !EdgeMatchesMethod(invokedMethod, storageEdge)) continue;
            if (invokedMethod == null && !EdgeMatchesMethod(invokedMethod, storageEdge)) continue;

            if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
            {
                continue;
            }

            var lineText = storageEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                ? methodValue?.ToString()
                : null;
            var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
            AppendIndented(builder, indent, $"uses_storage {storageNode.Name}{methodSuffix}{lineText}");
            state.CurrentImpact?.RecordStorage(GetDisplayName(storageNode));
        }

        foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
        {
            if (invokedMethod != null && !EdgeMatchesMethod(invokedMethod, dataEdge)) continue;
            if (invokedMethod == null && !EdgeMatchesMethod(invokedMethod, dataEdge)) continue;

            if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
            {
                continue;
            }

            var lineText = dataEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var label = ExtractOperationLabel(dataEdge);
            AppendIndented(builder, indent, $"{label} {entityNode.Name}{lineText}");
            state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);
        }

        // Mapping edges directly off the service implementation
        foreach (var mapEdge in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, state, mapEdge, indent);
        }

        // Cache usages
        foreach (var cacheEdge in edges.Where(e => e.Kind == "uses_cache"))
        {
            if (!state.NodesById.TryGetValue(cacheEdge.To, out var cacheNode)) continue;
            var cacheMethod = cacheEdge.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
            var operation = cacheEdge.Props is { } opProps && opProps.TryGetValue("operation", out var opValue) ? opValue?.ToString() : null;
            var key = cacheEdge.Props is { } keyProps && keyProps.TryGetValue("key", out var keyValue) ? keyValue?.ToString() : null;
            var lineText = cacheEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
            var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
            var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
            var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
            state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal); // reuse generic set if available
            if (!state.DedupRequests.Add("CACHE::" + cacheKey))
            {
                continue; // suppress duplicate
            }
            AppendIndented(builder, indent, $"uses_cache {cacheNode.Name}{methodPart}{opPart}{keyPart}{lineText}");
            state.CurrentImpact?.RecordCache(GetDisplayName(cacheNode));
        }

        // Options
        foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
        {
            if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode)) continue;
            var section = GetNodeProp(optionsNode, "section");
            var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
            var lineText = optionsEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"uses_options {optionsNode.Name}{sectionText}{lineText}");
            state.CurrentImpact?.RecordOption(GetDisplayName(optionsNode));
        }

        // Logging
        foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
        {
            if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode)) continue;
            var lineText = logEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var level = logEdge.Props is { } lprops && lprops.TryGetValue("level", out var levelValue) ? levelValue?.ToString() : null;
            var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
            AppendIndented(builder, indent, $"logs {loggerNode.Name}{levelText}{lineText}");
        }

        // Validation / guard clauses
        foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
        {
            if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode)) continue;
            var lineText = validationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var validationMethod = validationEdge.Props is { } vprops && vprops.TryGetValue("method", out var vm) ? vm?.ToString() : null;
            var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
            AppendIndented(builder, indent, $"validation {guardNode.Name}{methodText}{lineText}");
        }

        // Sends / dispatches requests
        foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
        {
            if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode)) continue;
            var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var responseType = requestEdge.Props is { } rprops && rprops.TryGetValue("response_type", out var rt) ? rt?.ToString() : null;
            var handlerName = string.Empty;
            if (state.EdgesByFrom.TryGetValue(requestNode.Id, out var downstream) && downstream.FirstOrDefault(e => e.Kind == "handled_by") is { } handled && state.NodesById.TryGetValue(handled.To, out var handlerNode))
            {
                handlerName = handlerNode.Name;
            }
            var handlerPart = string.IsNullOrWhiteSpace(handlerName) ? string.Empty : $" -> {handlerName}";
            var responsePart = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" : {responseType}";
            var synthetic = string.Equals(requestEdge.Source, "synthetic", StringComparison.OrdinalIgnoreCase) && requestEdge.Transform?.Type == "requestprocessor.dispatch";
            var prefix = synthetic ? "dispatches" : "sends_request";
            var requestKey = requestNode.Id + "::" + handlerName + "::" + responseType;
            state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
            if (!state.DedupRequests.Add("REQFLOW::" + requestKey))
            {
                AppendIndented(builder, indent, $"{prefix} {requestNode.Name}{handlerPart}{responsePart}{lineText} ... (reused)");
                continue;
            }
            AppendIndented(builder, indent, $"{prefix} {requestNode.Name}{handlerPart}{responsePart}{lineText}");
            state.CurrentImpact?.RecordRequest(GetDisplayName(requestNode));
            if (!string.IsNullOrWhiteSpace(handlerName))
            {
                state.CurrentImpact?.RecordHandler(handlerName);
            }
            AppendCommandFlow(builder, state, requestNode, indent + 1);
        }

        // Return edges
        foreach (var returnEdge in edges.Where(e => e.Kind == "returns"))
        {
            var annotation = returnEdge.Props is { } props && props.TryGetValue("kind", out var kindValue) ? kindValue?.ToString() : null;
            AppendMappingEdge(builder, state, returnEdge, indent, label: "returns", annotation: annotation);
        }

        // Notification publishing
        foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
        {
            if (!state.NodesById.TryGetValue(notificationEdge.To, out var notificationNode)) continue;
            var lineText = notificationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
            state.CurrentImpact?.RecordNotification(GetDisplayName(notificationNode));
            AppendNotificationFlow(builder, state, notificationNode, indent + 1);
        }

        // Event/message publishing
        foreach (var publishEdge in edges.Where(e => e.Kind == "publishes"))
        {
            if (!state.NodesById.TryGetValue(publishEdge.To, out var messageNode)) continue;
            var lineText = publishEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var details = BuildPublisherDetails(messageNode);
            AppendIndented(builder, indent, $"publishes {messageNode.Name}{details}{lineText}");
            state.CurrentImpact?.RecordMessage(GetDisplayName(messageNode));
            AppendPublisherFlow(builder, state, messageNode, indent + 1);
        }
    }

    private static bool EdgeMatchesMethod(string? invokedMethod, GraphEdge edge)
    {
        if (string.IsNullOrWhiteSpace(invokedMethod))
        {
            return true;
        }

        if (edge.Props is not { } props)
        {
            return false;
        }

        if (props.TryGetValue("method", out var methodValue) && methodValue is string methodName && !string.IsNullOrWhiteSpace(methodName))
        {
            return string.Equals(methodName.Trim(), invokedMethod.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        if (props.TryGetValue("client_method", out var clientMethodValue) && clientMethodValue is string clientMethod && !string.IsNullOrWhiteSpace(clientMethod))
        {
            return string.Equals(clientMethod.Trim(), invokedMethod.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    private static void AppendRequestProcessorFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode caller,
        int indent)
    {
        // Discover all synthetic requestprocessor dispatch edges from the caller context
        if (!state.EdgesByFrom.TryGetValue(caller.Id, out var edges))
        {
            AppendIndented(builder, indent, "... (no dispatches detected)");
            return;
        }

        var dispatches = edges
            .Where(e => e.Kind == "sends_request" &&
                        string.Equals(e.Source, "synthetic", StringComparison.OrdinalIgnoreCase) &&
                        e.Transform?.Type == "requestprocessor.dispatch" &&
                        state.NodesById.TryGetValue(e.To, out _))
            .Select(e => new { Edge = e, Node = state.NodesById[e.To] })
            .OrderBy(d => d.Node.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (dispatches.Count == 0)
        {
            AppendIndented(builder, indent, "... (no dispatches detected)");
            return;
        }

        // Emit generic IRequestProcessor narration once per unique TRequest/TResult pair
        var genericPairs = dispatches
            .Select(d => new
            {
                TRequest = d.Node.Name,
                TResult = d.Edge.Props is { } p && p.TryGetValue("response_type", out var rt) && !string.IsNullOrWhiteSpace(rt?.ToString())
                    ? GetSimpleType(rt!.ToString())
                    : "Unit"
            })
            .Distinct()
            .ToList();

        foreach (var g in genericPairs)
        {
            AppendIndented(builder, indent, $"constructs RequestProcessorWrapper<{g.TRequest},{g.TResult}>");
            AppendIndented(builder, indent, $"resolves IPipelineBehavior<{g.TRequest},{g.TResult}> chain");
            AppendIndented(builder, indent, $"invokes IAsyncRequestHandler<{g.TRequest},{g.TResult}>.Handle");
        }

        foreach (var d in dispatches)
        {
            var lineText = d.Edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var responseType = d.Edge.Props is { } props && props.TryGetValue("response_type", out var rt) ? rt?.ToString() : null;
            var responsePart = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" : {responseType}";
            AppendIndented(builder, indent, $"dispatches {d.Node.Name}{responsePart}{lineText}");

            // Expand pipeline behaviors (processed_by edges) under the request
            if (state.EdgesByFrom.TryGetValue(d.Node.Id, out var requestEdges))
            {
                var behaviors = requestEdges.Where(e => e.Kind == "processed_by").ToList();
                foreach (var be in behaviors)
                {
                    if (!state.NodesById.TryGetValue(be.To, out var behaviorNode)) continue;
                    var stage = behaviorNode.Props is { } bProps && bProps.TryGetValue("stage", out var stageVal) ? stageVal?.ToString() : null;
                    var stageText = string.IsNullOrWhiteSpace(stage) ? string.Empty : $" [{stage}]";
                    AppendIndented(builder, indent + 1, $"processed_by {behaviorNode.Name}{stageText}");
                }
            }

            // Expand handler
            AppendCommandFlow(builder, state, d.Node, indent + 1);
        }
    }

    private static string GetSolutionRoot(string? project)
    {
        if (string.IsNullOrWhiteSpace(project))
        {
            return string.Empty;
        }

        var normalized = project.Replace('\\', '/');
        var separatorIndex = normalized.IndexOf('/');
        return separatorIndex > 0 ? normalized[..separatorIndex] : normalized;
    }

    private static bool IsWithinCallerSolution(GraphNode caller, GraphNode candidate)
    {
        var callerSolution = GetSolutionRoot(caller.Project);
        var candidateSolution = GetSolutionRoot(candidate.Project);

        if (!string.IsNullOrWhiteSpace(callerSolution) && !string.IsNullOrWhiteSpace(candidateSolution))
        {
            return string.Equals(callerSolution, candidateSolution, StringComparison.OrdinalIgnoreCase);
        }

        var callerRoot = GetAssemblyRoot(caller.Assembly);
        var candidateRoot = GetAssemblyRoot(candidate.Assembly);
        if (!string.IsNullOrWhiteSpace(callerRoot) && !string.IsNullOrWhiteSpace(candidateRoot))
        {
            return string.Equals(callerRoot, candidateRoot, StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    private static bool ShouldIncludeImplementation(GraphNode caller, GraphNode implementation)
    {
        if (string.Equals(caller.Id, implementation.Id, StringComparison.Ordinal))
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(implementation.Assembly))
        {
            return true;
        }

        if (implementation.Assembly.IndexOf(".Tests", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return false;
        }

        if (!IsWithinCallerSolution(caller, implementation))
        {
            return false;
        }

        // Heuristic inclusion rules (after solution filtering):
        // 1. Always include if the implementation has a concrete source file (internal code).
        // 2. Include if assembly roots match (likely same bounded context / solution segment).
        // 3. Exclude otherwise (likely external / framework / heuristic duplicate) to avoid noisy expansions.

        var callerRoot = GetAssemblyRoot(caller.Assembly);
        var implRoot = GetAssemblyRoot(implementation.Assembly);
        var hasFile = !string.IsNullOrWhiteSpace(implementation.FilePath) && !implementation.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase);
        if (hasFile)
        {
            return true; // Internal source present
        }

        if (string.Equals(callerRoot, implRoot, StringComparison.OrdinalIgnoreCase))
        {
            return true; // Same root; treat as in-scope
        }

        return false; // Different root & no file evidence => skip
    }

    private static string GetAssemblyRoot(string assembly)
    {
        if (string.IsNullOrWhiteSpace(assembly))
        {
            return string.Empty;
        }

        var separatorIndex = assembly.IndexOf('.');
        return separatorIndex > 0 ? assembly[..separatorIndex] : assembly;
    }

    private static string? CanonicalizeRoute(string? route)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            return null;
        }

        var trimmed = route.Trim();

        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var absolute))
        {
            trimmed = absolute.AbsolutePath;
        }
        else if (trimmed.Contains("://", StringComparison.Ordinal))
        {
            var parts = trimmed.Split(new[] { "://" }, 2, StringSplitOptions.None);
            var remainder = parts.Length == 2 ? parts[1] : trimmed;
            var slashIndex = remainder.IndexOf('/');
            trimmed = slashIndex >= 0 ? remainder[slashIndex..] : "/";
        }

        var questionIndex = trimmed.IndexOf('?', StringComparison.Ordinal);
        if (questionIndex >= 0)
        {
            trimmed = trimmed[..questionIndex];
        }

        trimmed = trimmed.Replace('\\', '/');
        while (trimmed.Contains("//", StringComparison.Ordinal))
        {
            trimmed = trimmed.Replace("//", "/", StringComparison.Ordinal);
        }

        trimmed = trimmed.Trim();
        if (trimmed.Length == 0)
        {
            return "/";
        }

        if (!trimmed.StartsWith("/", StringComparison.Ordinal))
        {
            trimmed = "/" + trimmed;
        }

        trimmed = Uri.UnescapeDataString(trimmed);

        var segments = trimmed.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var cleaned = new List<string>(segments.Length);
        for (var i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];
            if (segment.StartsWith("{", StringComparison.Ordinal) && segment.EndsWith("}", StringComparison.Ordinal))
            {
                var colonIndex = segment.IndexOf(':');
                if (colonIndex > 0)
                {
                    segment = segment[..colonIndex] + "}";
                }
            }

            var lowered = segment.ToLowerInvariant();
            if (IsVersionSegment(lowered) && (cleaned.Count == 0 || (cleaned.Count == 1 && cleaned[0] == "api")))
            {
                continue;
            }

            cleaned.Add(lowered);
        }

        if (cleaned.Count == 0)
        {
            return "/";
        }

        return "/" + string.Join('/', cleaned);
    }

    private static bool IsVersionSegment(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
        {
            return false;
        }

        if (!segment.StartsWith('v'))
        {
            return false;
        }

        var payload = segment[1..];
        if (payload.StartsWith("{", StringComparison.Ordinal))
        {
            return true;
        }

        var hasDigit = false;
        foreach (var ch in payload)
        {
            if (char.IsDigit(ch))
            {
                hasDigit = true;
                continue;
            }

            if (ch is '.' or '_' or '-')
            {
                continue;
            }

            return false;
        }

        return hasDigit;
    }


}
