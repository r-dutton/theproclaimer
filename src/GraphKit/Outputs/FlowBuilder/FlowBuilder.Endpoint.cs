using GraphKit.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GraphKit.Outputs.Utilities;
namespace GraphKit.Outputs
{
    public static partial class FlowBuilder
    {
        public static void AppendEndpointFlow(StringBuilder builder, FlowRenderState state, GraphNode endpoint, int indent)
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
                var headerLabel = $"[web] {method} {route}  ({endpoint.Fqdn})";
                var header = $"{FormatLinkedCode(headerLabel, endpoint.FilePath, span?.StartLine, span?.EndLine)}{statusText}{authAnnotation}{simulateText}";

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
                    var detailParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
                    if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
                    var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
                    var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
                    var baseLabel = $"uses_configuration {details}";
                    AppendIndented(builder, childIndent, $"{FormatLinkedCode(baseLabel, configEdge.Transform?.Location)}{valueText}");
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

                    var targetType = validatorEdge.Props is { } props && props.TryGetValue("target_type", out var value)
                        ? value?.ToString()
                        : null;
                    var extra = string.IsNullOrWhiteSpace(targetType) ? string.Empty : $" ({targetType})";
                    var baseLabel = $"uses_validator {validatorNode.Name}{extra}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, validatorEdge.Transform?.Location));
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

                    var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
                    var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
                    var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
                    var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
                    state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
                    if (!state.DedupRequests.Add("CACHE::" + cacheKey)) continue;
                    var baseLabel = $"uses_cache {cacheNode.Name}{methodPart}";
                    AppendIndented(builder, childIndent, $"{FormatLinkedCode(baseLabel, cacheEdge.Transform?.Location)}{opPart}{keyPart}");
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
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, optionsEdge.Transform?.Location));
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
                        var label = $"calls {targetNode.Name}{serviceMethodText}";
                        AppendIndented(builder, childIndent, FormatLinkedCode(label, callEdge.Transform?.Location));
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
                        var label = $"calls {targetNode.Name}{serviceMethodText}";
                        AppendIndented(builder, childIndent, FormatLinkedCode(label, callEdge.Transform?.Location));
                    }
                    else
                    {
                        var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                        var label = $"calls {targetNode.Name}{methodsPart}";
                        var linked = firstLine.HasValue
                            ? FormatLinkedCode(label, callEdge.Transform?.Location?.File, firstLine, null)
                            : label;
                        AppendIndented(builder, childIndent, linked);
                    }
                    AppendRepositoryFlow(builder, state, targetNode, childIndent + 1);
                }

                foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
                {
                    if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
                    {
                        continue;
                    }

                    var label = ExtractOperationLabel(dataEdge);
                    var baseLabel = $"{label} {entityNode.Name}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, dataEdge.Transform?.Location));
                    state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);

                    if (Utilities.IsEntityNode(entityNode) || Utilities.IsLikelyEntity(entityNode))
                    {
                        AppendEntityFlow(builder, state, entityNode, childIndent + 1, dataEdge.Kind);
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

                    var lifetime = serviceEdge.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                        ? lifetimeValue?.ToString()
                        : null;
                    var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                    var serviceMethodName = serviceEdge.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                        ? methodValue?.ToString()
                        : null;
                    var location = serviceEdge.Transform?.Location;
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
                        var repoImpl = TryResolveControlledRepository(state, endpoint, serviceNode)
                                       ?? TryResolveRepositoryPattern(state, endpoint, serviceNode)
                                       ?? collapseImpl
                                       ?? TryResolveSingleImplementation(state, endpoint, serviceNode);
                        if (repoImpl != null && repoImpl.Type is "app.repository" or "repository")
                        {
                            var alreadyPrinted = callEdges.Any(call => call.To == repoImpl.Id && string.Equals(GetCallMethod(call), serviceMethodName, StringComparison.OrdinalIgnoreCase));
                            if (!alreadyPrinted)
                            {
                                var repoMethodSuffix = string.IsNullOrWhiteSpace(serviceMethodName) ? string.Empty : $".{serviceMethodName}";
                                var repoLabel = $"calls {repoImpl.Name}{repoMethodSuffix}";
                                AppendIndented(builder, childIndent, FormatLinkedCode(repoLabel, location));
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
                    var baseLabel = $"uses_service {printedName}{suffix}";
                    var nextIndent = childIndent + 1;
                    if (string.IsNullOrWhiteSpace(serviceMethodName))
                    {
                        AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, location));
                    }
                    else
                    {
                        AppendIndented(builder, childIndent, baseLabel);
                        var methodLabel = $"method {serviceMethodName}";
                        AppendIndented(builder, childIndent + 1, FormatLinkedCode(methodLabel, location));
                        nextIndent = childIndent + 2;
                    }
                    if (collapse && collapseImpl != null)
                    {
                        // Expand implementation (show header for clarity now that collapsed uses_service line renamed)
                        AppendServiceImplementationFlow(builder, state, endpoint, collapseImpl, serviceMethodName, nextIndent, heuristic: !state.EdgesByFrom.ContainsKey(collapseImpl.Id), suppressHeader: false);
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
                    var locatorMethod = locatedEdge.Props is { } lprops && lprops.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                    var methodText = string.IsNullOrWhiteSpace(locatorMethod) ? string.Empty : $".{locatorMethod}";
                    var baseLabel = $"service_located {locatedNode.Name}{methodText}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, locatedEdge.Transform?.Location));
                }

                foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
                {
                    if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
                    {
                        continue;
                    }

                    var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var value)
                        ? value?.ToString()
                        : null;
                    var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
                    var baseLabel = $"uses_storage {storageNode.Name}{methodSuffix}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, storageEdge.Transform?.Location));
                    state.CurrentImpact?.RecordStorage(GetDisplayName(storageNode));
                }

                foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
                {
                    if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode))
                    {
                        continue;
                    }

                    var level = logEdge.Props is { } props && props.TryGetValue("level", out var levelValue)
                        ? levelValue?.ToString()
                        : null;
                    var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
                    var baseLabel = $"logs {loggerNode.Name}";
                    AppendIndented(builder, childIndent, $"{FormatLinkedCode(baseLabel, logEdge.Transform?.Location)}{levelText}");
                }

                foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
                {
                    if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode))
                    {
                        continue;
                    }

                    var validationMethod = validationEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
                        ? methodValue?.ToString()
                        : null;
                    var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
                    var baseLabel = $"validation {guardNode.Name}{methodText}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, validationEdge.Transform?.Location));
                }

                foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
                {
                    if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode)) continue;

                    // Skip duplicate sends/dispatch of same request at same line
                    var requestKey = $"{requestNode.Id}:{requestEdge.Transform?.Location?.Line}";
                    state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
                    if (!state.DedupRequests.Add(requestKey)) continue;

                    var responseType = requestEdge.Props is { } rprops && rprops.TryGetValue("response_type", out var rt) ? rt?.ToString() : null;
                    var handlerName = string.Empty;
                    if (state.EdgesByFrom.TryGetValue(requestNode.Id, out var downstream) && downstream.FirstOrDefault(e => e.Kind == "handled_by") is { } handled && state.NodesById.TryGetValue(handled.To, out var handlerNode))
                    {
                        handlerName = handlerNode.Name;
                    }
                    var handlerPart = string.IsNullOrWhiteSpace(handlerName) ? string.Empty : $" -> {handlerName}";
                    var responsePart = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" ({responseType})";
                    var synthetic = string.Equals(requestEdge.Source, "synthetic", StringComparison.OrdinalIgnoreCase) && requestEdge.Transform?.Type == "requestprocessor.dispatch";
                    var prefix = synthetic ? "dispatches" : "sends_request";
                    var baseLabel = $"{prefix} {requestNode.Name}";
                    AppendIndented(builder, childIndent, $"{FormatLinkedCode(baseLabel, requestEdge.Transform?.Location)}{handlerPart}{responsePart}");
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

                    var baseLabel = $"publishes_notification {notificationNode.Name}";
                    AppendIndented(builder, childIndent, FormatLinkedCode(baseLabel, notificationEdge.Transform?.Location));
                    state.CurrentImpact?.RecordNotification(GetDisplayName(notificationNode));
                    AppendNotificationFlow(builder, state, notificationNode, childIndent + 1);
                }
            }
            finally
            {
                state.EndpointStack.Remove(endpoint.Id);
            }
        }
    }
}
