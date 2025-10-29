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
            var clientDisplay = GetDisplayName(clientNode);
            string? clientMethodHint = ExtractProp(clientEdge, "method");
            string? clientVerbHint = ExtractProp(clientEdge, "verb");
            string? clientRouteHint = ExtractProp(clientEdge, "route");
            string? clientTargetService = ExtractProp(clientEdge, "target_service");
            string? clientBaseUrl = ExtractProp(clientEdge, "base_url");
            string? clientConfigKey = ExtractProp(clientEdge, "configuration_key");

            clientMethodHint = string.IsNullOrWhiteSpace(clientMethodHint) ? null : clientMethodHint.Trim();
            clientVerbHint = string.IsNullOrWhiteSpace(clientVerbHint) ? null : clientVerbHint.Trim();
            clientRouteHint = string.IsNullOrWhiteSpace(clientRouteHint) ? null : clientRouteHint.Trim();
            clientTargetService = string.IsNullOrWhiteSpace(clientTargetService) ? null : clientTargetService.Trim();
            clientBaseUrl = string.IsNullOrWhiteSpace(clientBaseUrl) ? null : clientBaseUrl.Trim();
            clientConfigKey = string.IsNullOrWhiteSpace(clientConfigKey) ? null : clientConfigKey.Trim();

            var headerDetails = new List<string>();
            if (!string.IsNullOrWhiteSpace(clientVerbHint) && !string.IsNullOrWhiteSpace(clientRouteHint))
            {
                headerDetails.Add($"{clientVerbHint} {clientRouteHint}");
            }
            else if (!string.IsNullOrWhiteSpace(clientRouteHint))
            {
                headerDetails.Add(clientRouteHint);
            }

            if (!string.IsNullOrWhiteSpace(clientMethodHint))
            {
                headerDetails.Add($"method={clientMethodHint}");
            }

            if (!string.IsNullOrWhiteSpace(clientBaseUrl))
            {
                headerDetails.Add($"base={clientBaseUrl}");
            }

            if (!string.IsNullOrWhiteSpace(clientConfigKey))
            {
                headerDetails.Add($"config={clientConfigKey}");
            }

            if (!string.IsNullOrWhiteSpace(clientTargetService))
            {
                headerDetails.Add($"target={clientTargetService}");
            }

            var detailSuffix = headerDetails.Count > 0 ? $" ({string.Join(", ", headerDetails)})" : string.Empty;
            var baseLabel = $"uses_client {clientDisplay}{detailSuffix}";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, clientEdge.Transform?.Location));
            state.CurrentImpact?.RecordClient(clientDisplay);

            static bool IsHttpVerbCandidate(string? value)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }

                return value.Equals("GET", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("DELETE", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("PATCH", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("HEAD", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase) ||
                       value.Equals("TRACE", StringComparison.OrdinalIgnoreCase);
            }

            HashSet<string>? allowedClientMethods = null;
            HashSet<string>? allowedHttpVerbs = null;

            void RecordAllowedValue(string? candidate)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    return;
                }

                var normalized = candidate.Trim();
                if (normalized.Length == 0)
                {
                    return;
                }

                if (IsHttpVerbCandidate(normalized))
                {
                    allowedHttpVerbs ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    allowedHttpVerbs.Add(normalized);
                }
                else
                {
                    allowedClientMethods ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    allowedClientMethods.Add(normalized);
                }
            }

            if (clientEdge.Props is { } clientProps)
            {
                if (clientProps.TryGetValue("method", out var methodValue) && methodValue is not null)
                {
                    RecordAllowedValue(methodValue.ToString());
                }

                if (clientProps.TryGetValue("methods", out var methodsValue) && methodsValue is IEnumerable<object> methodList)
                {
                    foreach (var candidate in methodList)
                    {
                        RecordAllowedValue(candidate?.ToString());
                    }
                }
            }

            var directCallEdges = state.EdgesByFrom.TryGetValue(clientNode.Id, out var clientEdges)
                ? clientEdges.Where(e => e.Kind == "calls").ToList()
                : new List<GraphEdge>();

            // Candidate call edges (include direct plus implementation fallbacks)
            var allCallEdges = new List<GraphEdge>(directCallEdges);

            foreach (var implementation in state.FindCandidateImplementations(clientNode))
            {
                if (!state.EdgesByFrom.TryGetValue(implementation.Id, out var implEdges) || implEdges.Count == 0)
                {
                    continue;
                }

                foreach (var edge in implEdges)
                {
                    if (edge.Kind == "calls")
                    {
                        allCallEdges.Add(edge);
                    }
                }
            }

            if (allCallEdges.Count == 0)
            {
                // No observable downstream HTTP calls; rely on metadata fallbacks below.
                allCallEdges = new List<GraphEdge>();
            }

            // Restrict by allowed methods if provided
            if ((allowedClientMethods is { Count: > 0 }) || (allowedHttpVerbs is { Count: > 0 }))
            {
                allCallEdges = allCallEdges.Where(e =>
                {
                    if (e.Props is not { } p) return false;
                    bool MethodMatches(HashSet<string>? candidates)
                    {
                        if (candidates is not { Count: > 0 }) return true;
                        if (p.TryGetValue("client_method", out var mv) && mv is string ms && candidates.Contains(ms.Trim())) return true;
                        if (p.TryGetValue("method", out var mv2) && mv2 is string ms2 && candidates.Contains(ms2.Trim())) return true;
                        return false;
                    }

                    bool VerbMatches(HashSet<string>? candidates)
                    {
                        if (candidates is not { Count: > 0 }) return true;
                        if (p.TryGetValue("verb", out var verbValue) && verbValue is string vs && candidates.Contains(vs.Trim())) return true;
                        return false;
                    }

                    return MethodMatches(allowedClientMethods) && VerbMatches(allowedHttpVerbs);
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
                .Select(e =>
                {
                    var method = ExtractProp(e, "client_method") ?? ExtractProp(e, "method") ?? e.Props?.GetValueOrDefault("method")?.ToString();
                    method = string.IsNullOrWhiteSpace(method) ? clientMethodHint : method?.Trim();

                    var verb = ExtractProp(e, "verb");
                    verb = string.IsNullOrWhiteSpace(verb) ? clientVerbHint : verb?.Trim();

                    var route = ExtractProp(e, "route");
                    route = string.IsNullOrWhiteSpace(route) ? clientRouteHint : route?.Trim();

                    var targetService = ExtractProp(e, "target_service");
                    targetService = string.IsNullOrWhiteSpace(targetService) ? clientTargetService : targetService?.Trim();

                    return new
                    {
                        Edge = e,
                        Method = method ?? string.Empty,
                        Verb = verb ?? string.Empty,
                        Route = route ?? string.Empty,
                        TargetService = targetService ?? string.Empty
                    };
                })
                .GroupBy(x => (x.Method, x.Verb, x.Route, x.TargetService))
                .Select(g => g.First())
                .ToList();

            foreach (var call in distinctCalls)
            {
                var callEdge = call.Edge;
                var baseUrlValue = callEdge.Props is { } baseProps && baseProps.TryGetValue("base_url", out var baseObj)
                    ? baseObj?.ToString()
                    : clientBaseUrl;
                state.CurrentImpact?.RecordRemoteCall(clientDisplay, call.Verb, call.Route, baseUrlValue, call.TargetService);
            }

            const int ExplosionThreshold = 25;
            if (distinctCalls.Count > ExplosionThreshold)
            {
                AppendIndented(builder, indent + 1, $"calls {clientNode.Name} (distinct_methods={distinctCalls.Count}) [elided]");
                foreach (var sample in distinctCalls.Take(10))
                {
                    var detailParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(sample.Verb) && !string.IsNullOrWhiteSpace(sample.Route)) detailParts.Add($"{sample.Verb} {sample.Route}");
                    if (!string.IsNullOrWhiteSpace(sample.Method)) detailParts.Add(sample.Method);
                    if (!string.IsNullOrWhiteSpace(sample.TargetService)) detailParts.Add($"target={sample.TargetService}");
                    var detail = detailParts.Count > 0 ? " (" + string.Join(", ", detailParts) + ")" : string.Empty;
                    var sampleLabel = $"calls{detail}";
                    AppendIndented(builder, indent + 2, FormatLinkedCode(sampleLabel, sample.Edge.Transform?.Location));
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
                var baseUrl = callEdge.Props is { } p3 && p3.TryGetValue("base_url", out var b) ? b?.ToString() : clientBaseUrl;
                var configKey = callEdge.Props is { } p4 && p4.TryGetValue("configuration_key", out var c) ? c?.ToString() : clientConfigKey;
                var targetService = call.TargetService;
                var queryParams = callEdge.Props is { } p5 && p5.TryGetValue("query_params", out var qObj) && qObj is IEnumerable<object> rawParams
                    ? rawParams.Select(x => x?.ToString()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList()
                    : null;
                if (queryParams is null && clientEdge.Props is { } clientEdgeProps && clientEdgeProps.TryGetValue("query_params", out var clientQueryObj) && clientQueryObj is IEnumerable<object> clientParams)
                {
                    queryParams = clientParams
                        .Select(x => x?.ToString())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();
                }
                var details = new List<string>();
                if (!string.IsNullOrWhiteSpace(verb) && !string.IsNullOrWhiteSpace(route)) details.Add($"{verb} {route}");
                if (!string.IsNullOrWhiteSpace(call.Method)) details.Add($"method={call.Method}");
                if (!string.IsNullOrWhiteSpace(baseUrl)) details.Add($"base={baseUrl}");
                if (!string.IsNullOrWhiteSpace(configKey)) details.Add($"config={configKey}");
                if (!string.IsNullOrWhiteSpace(targetService)) details.Add($"target={targetService}");
                if (queryParams is { Count: > 0 }) details.Add($"query={string.Join('&', queryParams)}");
                var detailText = details.Count > 0 ? $" ({string.Join(", ", details)})" : string.Empty;
                var callLabel = $"calls {targetNode.Name}{detailText}";
                AppendIndented(builder, indent + 1, FormatLinkedCode(callLabel, callEdge.Transform?.Location));
                var expKey = clientNode.Id + "::" + (targetService ?? "*") + "::" + (verb ?? "*") + "::" + (route ?? "*");
                if (!state.HttpClientExpansionKeys.Add(expKey))
                {
                    AppendIndented(builder, indent + 2, "remote_endpoint_expansion_suppressed (see previous expansion)");
                    continue;
                }
                AppendTargetServiceFlow(builder, state, callEdge, indent + 2, string.IsNullOrWhiteSpace(call.TargetService) ? clientTargetService : call.TargetService);
            }

            // If we printed only the client usage and either there are no call edges or none include route/verb, annotate gap.
            var metadataEdges = directCallEdges.Count > 0 ? directCallEdges : allCallEdges;
            var anyCallEdges = metadataEdges.Count > 0;
            var anyCallWithMetadata = metadataEdges.Any(e => e.Props is { } cp && (cp.ContainsKey("route") || cp.ContainsKey("verb")));

            var metadataParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(clientVerbHint)) metadataParts.Add($"verb={clientVerbHint}");
            if (!string.IsNullOrWhiteSpace(clientRouteHint)) metadataParts.Add($"route={clientRouteHint}");
            if (!string.IsNullOrWhiteSpace(clientTargetService)) metadataParts.Add($"target={clientTargetService}");
            if (!string.IsNullOrWhiteSpace(clientBaseUrl)) metadataParts.Add($"base={clientBaseUrl}");
            if (!string.IsNullOrWhiteSpace(clientConfigKey)) metadataParts.Add($"config={clientConfigKey}");

            if (!anyCallEdges)
            {
                if (metadataParts.Count > 0)
                {
                    var metadataSuffix = $" [{string.Join(", ", metadataParts)}]";
                    AppendIndented(builder, indent + 1, $"remote_endpoint_summary (no downstream call edges captured){metadataSuffix}");
                }
                else
                {
                    AppendIndented(builder, indent + 1, "remote_endpoint_metadata_missing (no downstream call edges captured)");
                }
            }
            else if (!anyCallWithMetadata)
            {
                var metadataSuffix = metadataParts.Count > 0 ? $" [{string.Join(", ", metadataParts)}]" : string.Empty;
                AppendIndented(builder, indent + 1, $"remote_endpoint_metadata_missing (no route/verb captured for client calls){metadataSuffix}");
            }
        }
    }
}
