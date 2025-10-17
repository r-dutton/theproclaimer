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

        public static void AppendHttpClientUsage(
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
    }
}
