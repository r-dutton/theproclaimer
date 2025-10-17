using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Analyzers;

internal static class ClientLinker
{
    /// <summary>
    /// Synthetic linking pass: for any uses_client edges that have verb/route metadata but no explicit calls edge
    /// emit a calls edge from the client to matched endpoints (controllers or minimal APIs). Skips empty routes.
    /// </summary>
    public static void EmitClientUseCallEdges(
        ConcurrentDictionary<string, GraphNode> nodes,
        ConcurrentBag<GraphEdge> edges,
        ConcurrentDictionary<string, string> clientTargetServices)
    {
        try
        {
            var existingCallKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var edge in edges.Where(e => e.Kind == "calls" && e.Props is { }))
            {
                if (edge.Props!.TryGetValue("verb", out var verbVal) && edge.Props.TryGetValue("route", out var routeVal))
                {
                    existingCallKeys.Add($"{edge.From}|{verbVal}|{routeVal}|{edge.To}");
                }
            }

            var endpoints = nodes.Values
                .Where(n => n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                .ToList();
            if (endpoints.Count == 0)
            {
                return;
            }

            foreach (var uses in edges.Where(e => e.Kind == "uses_client" && e.Props is { }))
            {
                if (uses.Props is not { } props)
                {
                    continue;
                }

                var verb = props.TryGetValue("verb", out var verbValue) ? verbValue?.ToString() : null;
                var route = props.TryGetValue("route", out var routeValue) ? routeValue?.ToString() : null;
                if (string.IsNullOrWhiteSpace(verb) && string.IsNullOrWhiteSpace(route))
                {
                    continue; // No metadata
                }

                // Skip empty or root-only routes to prevent explosion
                if (!string.IsNullOrWhiteSpace(route) && (route == "/" || route.Trim() == string.Empty))
                {
                    continue;
                }

                var clientId = uses.To;
                if (!nodes.TryGetValue(clientId, out var clientNode) || clientNode.Type != "http.client")
                {
                    continue;
                }

                var hasExistingForVerbRoute = edges.Any(e => e.From == clientId && e.Kind == "calls" && e.Props is { } cp &&
                    (string.IsNullOrWhiteSpace(verb) || (cp.TryGetValue("verb", out var ev) && string.Equals(ev?.ToString(), verb, StringComparison.OrdinalIgnoreCase))) &&
                    (string.IsNullOrWhiteSpace(route) || (cp.TryGetValue("route", out var rv) && CanonicalizeRoute(rv?.ToString() ?? string.Empty) == CanonicalizeRoute(route ?? string.Empty))));
                if (hasExistingForVerbRoute)
                {
                    continue;
                }

                var matched = endpoints.Where(ep => EndpointMatches(ep, route, verb)).ToList();
                if (matched.Count == 0)
                {
                    continue;
                }

                foreach (var ep in matched)
                {
                    var routeProp = ep.Props is { } epProps && epProps.TryGetValue("route", out var epRouteValue) ? epRouteValue?.ToString() : null;
                    var httpMethodProp = ep.Props is { } epProps2 && epProps2.TryGetValue("http_method", out var epVerbValue) ? epVerbValue?.ToString() : null;
                    var effectiveVerb = verb ?? httpMethodProp ?? string.Empty;
                    var effectiveRoute = route ?? routeProp ?? string.Empty;
                    var key = $"{clientId}|{effectiveVerb}|{effectiveRoute}|{ep.Id}";
                    if (!existingCallKeys.Add(key))
                    {
                        continue;
                    }

                    string? targetService = null;
                    if (clientTargetServices.TryGetValue(clientNode.Fqdn, out var mappedService) || clientTargetServices.TryGetValue(clientNode.Name, out mappedService))
                    {
                        targetService = mappedService;
                    }

                    var callProps = new Dictionary<string, object>
                    {
                        ["verb"] = effectiveVerb,
                        ["route"] = effectiveRoute
                    };
                    if (!string.IsNullOrWhiteSpace(targetService))
                    {
                        callProps["target_service"] = targetService!;
                    }

                    if (uses.Transform?.Location is { } loc)
                    {
                        if (props.TryGetValue("method", out var m) && m is not null)
                        {
                            callProps["client_method"] = m.ToString();
                        }
                        edges.Add(new GraphEdge
                        {
                            From = clientId,
                            To = ep.Id,
                            Kind = "calls",
                            Source = "synthetic",
                            Confidence = 0.7,
                            Transform = new GraphTransform
                            {
                                Type = "httpclient.request",
                                Location = new GraphLocation { File = loc.File, Line = loc.Line }
                            },
                            Props = callProps,
                            Evidence = uses.Evidence
                        });
                    }
                }
            }
        }
        catch
        {
            // Swallow synthetic pass errors so they don't break graph generation
        }
    }

    private static bool EndpointMatches(GraphNode endpoint, string? route, string? verb)
    {
        if (endpoint.Props is not { }) return false;
        var endpointRoute = endpoint.Props.TryGetValue("route", out var r) ? r?.ToString() : null;
        var endpointVerb = endpoint.Props.TryGetValue("http_method", out var v) ? v?.ToString() : null;
        var routeMatches = string.IsNullOrWhiteSpace(route) || (!string.IsNullOrWhiteSpace(endpointRoute) && CanonicalizeRoute(route) == CanonicalizeRoute(endpointRoute));
        var verbMatches = string.IsNullOrWhiteSpace(verb) || (!string.IsNullOrWhiteSpace(endpointVerb) && string.Equals(verb, endpointVerb, StringComparison.OrdinalIgnoreCase));
        return routeMatches && verbMatches;
    }

    private static string CanonicalizeRoute(string route)
    {
        if (string.IsNullOrWhiteSpace(route)) return string.Empty;
        var trimmed = route.Trim('/');
        while (trimmed.Contains("//", StringComparison.Ordinal))
        {
            trimmed = trimmed.Replace("//", "/", StringComparison.Ordinal);
        }
        return trimmed.ToLowerInvariant();
    }
}
