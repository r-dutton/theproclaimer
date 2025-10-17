using GraphKit.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphKit.Outputs
{
    public static partial class Utilities
    {

        public static class Nodes
        {
            public static void AppendGenericServiceNode(StringBuilder builder,
        FlowBuilder.FlowRenderState state,
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
                    FlowBuilder.AppendRepositoryFlow(builder, state, targetNode, indent + 1);
                }

                foreach (var clientEdge in edges.Where(e => e.Kind == "uses_client"))
                {
                    if (invokedMethod == null && !EdgeMatchesMethod(invokedMethod, clientEdge)) continue;

                    if (!state.NodesById.TryGetValue(clientEdge.To, out var clientNode))
                    {
                        continue;
                    }

                    FlowBuilder.AppendHttpClientUsage(builder, state, clientEdge, clientNode, indent);
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

                    FlowBuilder.AppendServiceContractFlow(builder, state, node, serviceNode, serviceMethodName, nextIndent);
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
        }
    }
}
