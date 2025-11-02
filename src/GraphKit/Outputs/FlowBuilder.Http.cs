using GraphKit.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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

            var isLikelyLocal = allCallEdges.Count == 0 &&
                                string.IsNullOrWhiteSpace(clientRouteHint) &&
                                string.IsNullOrWhiteSpace(clientTargetService) &&
                                string.IsNullOrWhiteSpace(clientBaseUrl);

            var baseLabel = $"uses_client {clientDisplay}{detailSuffix}";
            if (isLikelyLocal)
            {
                var serviceLabel = $"uses_service {clientDisplay}";
                AppendIndented(builder, indent, FormatLinkedCode(serviceLabel, clientEdge.Transform?.Location));
                return;
            }

            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, clientEdge.Transform?.Location));
            state.CurrentImpact?.RecordClient(clientDisplay);

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

        public static string DetermineRemoteScope(string? host, string label, string callerRoot)
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

        public static bool IsPrivate172(string host)
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

        public static string? ExtractHost(string? baseUrl, string? route)
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

        public static string? ExtractProp(GraphEdge edge, string key)
        {
            if (edge.Props is not { } props)
            {
                return null;
            }

            return props.TryGetValue(key, out var value) ? value?.ToString() : null;
        }

        public static void AppendTargetServiceFlow(StringBuilder builder, FlowRenderState state, GraphEdge callEdge, int indent, string? fallbackTargetService = null)
        {
            if (state.Workspace is null)
            {
                return;
            }

            if (callEdge.Props is not { } props)
            {
                return;
            }

            string? GetProp(string key) => props.TryGetValue(key, out var value) ? value?.ToString() : null;

            var route = GetProp("route") ?? GetProp("relative_path");
            var verb = GetProp("verb") ?? GetProp("method");
            var baseUrl = GetProp("base_url");
            var canonicalRoute = CanonicalizeRoute(route);
            var routeForMatching = !string.IsNullOrWhiteSpace(canonicalRoute) && !canonicalRoute.Contains('*', StringComparison.Ordinal)
                ? route
                : null;

            var serviceName = props.TryGetValue("target_service", out var serviceValue) ? serviceValue?.ToString() : null;

            if (string.IsNullOrWhiteSpace(serviceName) && !string.IsNullOrWhiteSpace(fallbackTargetService))
            {
                serviceName = fallbackTargetService;
            }

            var host = ExtractHost(baseUrl, route);
            if (string.IsNullOrWhiteSpace(serviceName) && state.Workspace.TryResolveServiceByHost(host, out var hostService))
            {
                serviceName = hostService;
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                var routeText = string.IsNullOrWhiteSpace(route) ? "<unknown>" : route;
                var verbText = string.IsNullOrWhiteSpace(verb) ? "<unknown>" : verb;
                var hostSuffix = string.IsNullOrWhiteSpace(host) ? string.Empty : $" host={host}";
                var lookupKey = $"{callEdge.From}::{routeText}::{verbText}::{host}";
                if (!state.RemoteLookupKeys.Add(lookupKey))
                {
                    AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}{hostSuffix} (see previous lookup)");
                    return;
                }

                var reason = string.IsNullOrWhiteSpace(route)
                    ? "route metadata missing"
                    : "target_service metadata missing";
                AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}{hostSuffix} ({reason})");

                var globalCandidates = state.Document.Nodes
                    .Where(n => n.Type is "endpoint.controller" or "endpoint.minimal_api")
                    .ToList();
                var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, routeForMatching, verb);
                if (globalMatched.Count > 0)
                {
                    AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}{hostSuffix}");
                    foreach (var endpoint in globalMatched)
                    {
                        AppendEndpointFlow(builder, state, endpoint, indent + 1);
                    }
                }
                else
                {
                    AppendIndented(builder, indent + 1, $"unmatched_endpoint route={routeText} verb={verbText}");
                }
                return;
            }

            if (!state.Workspace.TryGetAssemblies(serviceName, out var assemblies) || assemblies.Count == 0)
            {
                AppendIndented(builder, indent, $"target_service {serviceName}");

                if (routeForMatching is null)
                {
                    var reason = string.IsNullOrWhiteSpace(route)
                        ? "route metadata missing"
                        : "route pattern ambiguous";
                    AppendIndented(builder, indent + 1, $"fallback_global_endpoint_match suppressed ({reason})");
                    return;
                }

                var globalCandidates = state.Document.Nodes
                    .Where(n => n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                    .ToList();
                var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, routeForMatching, verb);
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

            var keyRoute = routeForMatching ?? route ?? string.Empty;
            var key = $"{callEdge.From}->{serviceName}:{keyRoute}:{verb}";
            if (!state.TargetServiceVisited.Add(key))
            {
                AppendIndented(builder, indent, $"target_service {serviceName} (see previous expansion)");
                return;
            }

            var candidates = state.Document.Nodes
                .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                            && assemblySet.Contains(n.Assembly))
                .ToList();

            var targetHeader = string.IsNullOrWhiteSpace(host)
                ? $"target_service {serviceName}"
                : $"target_service {serviceName} (host={host})";
            AppendIndented(builder, indent, targetHeader);

            if (routeForMatching is null)
            {
                var reason = string.IsNullOrWhiteSpace(route)
                    ? "route metadata missing"
                    : "route pattern ambiguous";
                AppendIndented(builder, indent + 1, $"remote_endpoint_lookup suppressed ({reason})");
                return;
            }

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

            var matched = FilterEndpointsByRouteAndVerb(candidates, routeForMatching, verb);
            if (matched.Count == 0)
            {
                var globalCandidates = state.Document.Nodes
                    .Where(n => (n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api"))
                    .ToList();
                var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, routeForMatching, verb);
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

            if (state.AllowedIds is { } allowed)
            {
                foreach (var endpoint in matched)
                {
                    allowed.Add(endpoint.Id);
                }
            }

            foreach (var endpoint in matched)
            {
                AppendEndpointFlow(builder, state, endpoint, indent + 1);
            }
        }

        public static IReadOnlyList<GraphNode> FilterEndpointsByRouteAndVerb(
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
    }
}
