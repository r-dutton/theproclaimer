using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Analyzers;

internal static class ClientLinker
{
    private static readonly string[] KnownHttpVerbs = { "GET", "POST", "PUT", "DELETE", "PATCH", "HEAD", "OPTIONS" };

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
                if (!edge.Props!.TryGetValue("route", out var routeVal))
                {
                    continue;
                }

                var routeValue = routeVal?.ToString();
                if (string.IsNullOrWhiteSpace(routeValue))
                {
                    continue;
                }

                var canonicalRoute = CanonicalizeRoute(routeValue);
                var existingVerb = edge.Props.TryGetValue("verb", out var verbVal)
                    ? NormalizeHttpVerb(verbVal?.ToString()) ?? string.Empty
                    : string.Empty;

                existingCallKeys.Add($"{edge.From}|{existingVerb}|{canonicalRoute}|{edge.To}");
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

                var clientMethod = props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                var targetService = props.TryGetValue("target_service", out var targetValue) ? targetValue?.ToString() : null;
                var rawRoute = props.TryGetValue("route", out var routeValue) ? routeValue?.ToString() : null;
                if (string.IsNullOrWhiteSpace(rawRoute))
                {
                    continue;
                }

                var canonicalRoute = CanonicalizeRoute(rawRoute!);
                if (!LooksSpecific(canonicalRoute))
                {
                    continue;
                }
                var normalizedVerb = NormalizeHttpVerb(props.TryGetValue("verb", out var verbValue) ? verbValue?.ToString() : null);

                var clientId = uses.To;
                if (!nodes.TryGetValue(clientId, out var clientNode) || clientNode.Type != "http.client")
                {
                    continue;
                }

                var hasExistingForVerbRoute = edges.Any(e => e.From == clientId && e.Kind == "calls" && e.Props is { } cp &&
                    (normalizedVerb is null || NormalizeHttpVerb(cp.TryGetValue("verb", out var ev) ? ev?.ToString() : null) == normalizedVerb) &&
                    (cp.TryGetValue("route", out var rv) && CanonicalizeRoute(rv?.ToString() ?? string.Empty) == canonicalRoute));
                if (hasExistingForVerbRoute)
                {
                    continue;
                }

                var matched = endpoints.Where(ep => EndpointMatches(ep, canonicalRoute, normalizedVerb)).ToList();
                if (matched.Count == 0)
                {
                    continue;
                }

                foreach (var ep in matched)
                {
                    var routeProp = ep.Props is { } epProps && epProps.TryGetValue("route", out var epRouteValue) ? epRouteValue?.ToString() : null;
                    var httpMethodProp = ep.Props is { } epProps2 && epProps2.TryGetValue("http_method", out var epVerbValue) ? epVerbValue?.ToString() : null;
                    var endpointVerb = NormalizeHttpVerb(httpMethodProp);
                    var effectiveVerb = normalizedVerb ?? endpointVerb;
                    var effectiveRoute = rawRoute ?? routeProp ?? string.Empty;
                    var key = $"{clientId}|{(normalizedVerb ?? string.Empty)}|{canonicalRoute}|{ep.Id}";
                    if (!existingCallKeys.Add(key))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(targetService) && (clientTargetServices.TryGetValue(clientNode.Fqdn, out var mappedService) || clientTargetServices.TryGetValue(clientNode.Name, out mappedService)))
                    {
                        targetService = mappedService;
                    }

                    var callProps = new Dictionary<string, object>
                    {
                        ["route"] = effectiveRoute
                    };
                    if (props.TryGetValue("query_params", out var queryValue) && queryValue is not null)
                    {
                        callProps["query_params"] = queryValue;
                    }
                    if (!string.IsNullOrWhiteSpace(effectiveVerb))
                    {
                        callProps["verb"] = effectiveVerb!;
                    }
                    if (!string.IsNullOrWhiteSpace(targetService))
                    {
                        callProps["target_service"] = targetService!;
                    }
                    if (!string.IsNullOrWhiteSpace(clientMethod))
                    {
                        callProps["client_method"] = clientMethod!;
                    }

                    if (uses.Transform?.Location is { } loc)
                    {
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

    private static bool EndpointMatches(GraphNode endpoint, string canonicalRoute, string? normalizedVerb)
    {
        if (endpoint.Props is not { }) return false;
        var endpointRoute = endpoint.Props.TryGetValue("route", out var r) ? r?.ToString() : null;
        var endpointCanonicalRoute = CanonicalizeRoute(endpointRoute ?? string.Empty);

        if (!RoutesMatchWithTokens(canonicalRoute, endpointCanonicalRoute))
        {
            return false;
        }

        var endpointVerb = NormalizeHttpVerb(endpoint.Props.TryGetValue("http_method", out var v) ? v?.ToString() : null);
        var verbMatches = normalizedVerb is null || endpointVerb is null || string.Equals(normalizedVerb, endpointVerb, StringComparison.Ordinal);
        return verbMatches;
    }

    private static bool RoutesMatchWithTokens(string a, string b)
    {
        // Quick path exact match
        if (string.Equals(a, b, StringComparison.Ordinal)) return true;

        var aSegs = SplitAndTokenize(a);
        var bSegs = SplitAndTokenize(b);
        return MatchSegmentsWithWildcard(aSegs, bSegs);
    }

    private static string[] SplitAndTokenize(string route)
    {
        if (string.IsNullOrWhiteSpace(route)) return Array.Empty<string>();
        var segs = route.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        for (int i = 0; i < segs.Length; i++)
        {
            var s = segs[i];
            if (s.Length > 1 && s[0] == '{' && s[^1] == '}')
            {
                segs[i] = "{}";
                continue;
            }
            if (s.Contains('*'))
            {
                segs[i] = "*";
                continue;
            }
            // leave concrete segments as-is
        }
        return segs;
    }

    private static bool MatchSegmentsWithWildcard(string[] a, string[] b)
    {
        int ia = 0, ib = 0;
        int starA = -1, starB = -1; // remember star position in a and corresponding b index

        while (ib < b.Length)
        {
            if (ia < a.Length && (a[ia] == b[ib] || a[ia] == "{}" || b[ib] == "{}"))
            {
                ia++; ib++;
                continue;
            }
            if (ia < a.Length && a[ia] == "*")
            {
                starA = ia;
                starB = ib;
                ia++; // '*' matches zero or more segments
                continue;
            }
            if (starA != -1)
            {
                // backtrack: let '*' consume one more segment in b
                ia = starA + 1;
                ib = ++starB;
                continue;
            }
            return false;
        }

        // consume remaining '*' in a
        while (ia < a.Length && a[ia] == "*") ia++;

        // match if we've consumed all of a
        return ia == a.Length;
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

    private static bool LooksSpecific(string canonicalRoute)
    {
        if (string.IsNullOrWhiteSpace(canonicalRoute))
        {
            return false;
        }

        foreach (var ch in canonicalRoute)
        {
            if (char.IsLetter(ch))
            {
                return true;
            }
        }

        return false;
    }

    private static string? NormalizeHttpVerb(string? verb)
    {
        if (string.IsNullOrWhiteSpace(verb))
        {
            return null;
        }

        var trimmed = verb.Trim();
        if (trimmed.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
        {
            trimmed = trimmed[..^5];
        }

        var upper = trimmed.ToUpperInvariant();
        foreach (var known in KnownHttpVerbs)
        {
            if (upper.Equals(known, StringComparison.Ordinal))
            {
                return known;
            }

            if (upper.StartsWith(known, StringComparison.Ordinal))
            {
                return known;
            }
        }

        return null;
    }
}
