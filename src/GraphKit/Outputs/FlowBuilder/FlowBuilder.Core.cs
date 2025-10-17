using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using GraphKit.Workspace;

using static GraphKit.Outputs.Utilities;
namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    public static readonly HashSet<string> WriteOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "writes_to",
        "inserts_into",
        "updates",
        "deletes_from",
        "upserts"
    };

    public static readonly HashSet<string> ReadOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "queries",
        "reads_from",
        "selects"
    };

    public static bool IsWriteOperationKind(string? operationKind)
        => !string.IsNullOrWhiteSpace(operationKind) && WriteOperationKinds.Contains(operationKind!);

    public static bool IsReadOperationKind(string? operationKind)
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
    public static bool IsMutationVerb(string verb)
        => !string.Equals(verb, "GET", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "HEAD", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "OPTIONS", StringComparison.OrdinalIgnoreCase);

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

    // Extract core entity name from possible service / repository contract names.
    private static string ExtractEntityName(string? serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName)) return string.Empty;
        var name = serviceName.Trim();

        // Remove interface prefix
        if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
        {
            name = name[1..];
        }

        // Strip generic part
        var genericIndex = name.IndexOf('<');
        if (genericIndex > 0)
        {
            name = name[..genericIndex];
        }

        // Remove common suffixes
        string[] suffixes = ["Repository", "Repo", "Service", "DataAccess", "DataStore", "Dal", "ControlledRepository"];
        foreach (var s in suffixes)
        {
            if (name.EndsWith(s, StringComparison.OrdinalIgnoreCase))
            {
                name = name[..^s.Length];
                break; // remove only one outermost suffix
            }
        }

        // Collapse remaining generic wrappers like RequestProcessorWrapper<Foo,Bar>
        if (name.Contains('`'))
        {
            var tickIndex = name.IndexOf('`');
            if (tickIndex > 0) name = name[..tickIndex];
        }

        return name;
    }

    // Basic pattern match: case-insensitive equality. Allows generic patterns (IRepository<Entity>) direct compare.
    private static bool MatchesPattern(string candidate, string pattern)
        => string.Equals(candidate, pattern, StringComparison.OrdinalIgnoreCase);

    // Reuse existing repository detection via Utilities.
    private static bool IsRepositoryType(GraphNode node)
        => Utilities.IsRepositoryNode(node);

    // Select a best repository implementation candidate given caller context.
    private static GraphNode SelectBestMatch(List<GraphNode> candidates, GraphNode caller)
    {
        if (candidates.Count == 1) return candidates[0];

        var sameSolution = candidates.Where(c => Utilities.IsWithinCallerSolution(caller, c)).ToList();
        if (sameSolution.Count == 1)
        {
            return sameSolution[0];
        }
        if (sameSolution.Count > 1)
        {
            candidates = sameSolution;
        }

        // Prefer same solution / assembly root then concrete file presence then outgoing edges (more behavior).
        var callerRoot = Utilities.GetAssemblyRoot(caller.Assembly);
        var sameRoot = candidates.Where(c => string.Equals(Utilities.GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
        if (sameRoot.Count == 1) return sameRoot[0];
        if (sameRoot.Count > 1) candidates = sameRoot;

        var withFile = candidates.Where(c => !string.IsNullOrWhiteSpace(c.FilePath) && !c.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase)).ToList();
        if (withFile.Count == 1) return withFile[0];
        if (withFile.Count > 1) candidates = withFile;

        // Prefer candidates that have outgoing edges (likely concrete)
        // Need access to edges; fallback to first ordered for stability if not available
        // We cannot access state here; treat count of edges by scanning global document via caller reference
        // (state not available; thus heuristic simplified)

        var ordered = candidates
            .OrderByDescending(c => !string.IsNullOrWhiteSpace(c.FilePath))
            .ThenBy(c => c.Fqdn ?? c.Name ?? c.Id, StringComparer.OrdinalIgnoreCase)
            .ToList();
        return ordered[0];
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

    public static void AppendControllerFlow(StringBuilder builder, FlowRenderState state, GraphNode controller)
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


    public static void AppendRepositoryFlow(
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
            if (Utilities.IsEntityNode(entityNode) || Utilities.IsLikelyEntity(entityNode))
            {
                AppendEntityFlow(builder, state, entityNode, indent + 1, write.Kind);
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

    public static void AppendEntityFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode entity,
        int indent,
        string? operationFilter = null)
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

        var candidateEdges = edges
            .Where(e => e.Kind is "writes_to" or "reads_from" or "queries" or "inserts_into" or "updates" or "deletes_from" or "upserts")
            .ToList();

        if (!string.IsNullOrWhiteSpace(operationFilter))
        {
            var filtered = candidateEdges
                .Where(e => string.Equals(e.Kind, operationFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (filtered.Count > 0)
            {
                candidateEdges = filtered;
            }
        }

        foreach (var tableEdge in candidateEdges)
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

        foreach (var mapEdge in edges.Where(e => e.Kind == "maps_to"))
        {
            AppendMappingEdge(builder, state, mapEdge, indent);
        }
    }

    public static void AppendConversion(
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

    public static void AppendAutomapperRegistrations(
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

    public static string ResolveProfileName(FlowRenderState state, GraphNode mapNode)
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

    public static IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> BuildMapLookup(GraphDocument document)
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


    public static string? ExtractProp(GraphEdge edge, string key)
    {
        if (edge.Props is not { } props) return null;
        return props.TryGetValue(key, out var value) ? value?.ToString() : null;
    }

    public static void AppendTargetServiceFlow(StringBuilder builder, FlowRenderState state, GraphEdge callEdge, int indent)
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
        var baseUrl = props.TryGetValue("base_url", out var baseValue) ? baseValue?.ToString() : null;

        // If target_service is present we use existing assembly mapping logic; otherwise attempt global match.
        var serviceName = props.TryGetValue("target_service", out var serviceValue) ? serviceValue?.ToString() : null;

        var host = ExtractHost(baseUrl, route);
        if (string.IsNullOrWhiteSpace(serviceName) && state.Workspace.TryResolveServiceByHost(host, out var hostService))
        {
            serviceName = hostService;
        }

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
                var hostSuffix = string.IsNullOrWhiteSpace(host) ? string.Empty : $" host={host}";
                AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}{hostSuffix} (see previous lookup)");
                return;
            }

            var globalCandidates = state.Document.Nodes
                .Where(n => n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                .ToList();
            var globalMatched = FilterEndpointsByRouteAndVerb(globalCandidates, route, verb);
            var lookupHost = string.IsNullOrWhiteSpace(host) ? string.Empty : $" host={host}";
            AppendIndented(builder, indent, $"remote_endpoint_lookup route={routeText} verb={verbText}{lookupHost}");
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

        var targetHeader = string.IsNullOrWhiteSpace(host)
            ? $"target_service {serviceName}"
            : $"target_service {serviceName} (host={host})";
        AppendIndented(builder, indent, targetHeader);

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

    public static GraphNode? TryResolveSingleImplementation(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
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


    private static GraphNode? TryResolveRepositoryPattern(
    FlowRenderState state,
    GraphNode caller,
    GraphNode serviceNode)
    {
        // Extract entity name from various patterns
        var entityName = ExtractEntityName(serviceNode.Name);
        if (string.IsNullOrEmpty(entityName)) return null;

        // Try multiple repository patterns
        var patterns = new[]
        {
            $"I{entityName}Repository",
            $"{entityName}Repository",
            $"I{entityName}Repo",
            $"{entityName}Repo",
            $"I{entityName}DataAccess",
            $"{entityName}DataAccess",
            $"I{entityName}Dal",
            $"{entityName}Dal",
            $"IRepository<{entityName}>",
            $"Repository<{entityName}>",
            $"IControlledRepository<{entityName}>",
            $"ControlledRepository<{entityName}>"
        };

        foreach (var pattern in patterns)
        {
            var candidates = new List<GraphNode>();
            if (state.NodesByName.TryGetValue(pattern, out var nameMatches))
            {
                candidates.AddRange(nameMatches);
            }

            if (state.NodesByFqdn.TryGetValue(pattern, out var fqdnMatches))
            {
                candidates.AddRange(fqdnMatches);
            }

            if (candidates.Count == 0 && pattern.Contains('<'))
            {
                // Attempt to match without generic adornment for interface names stored without generic metadata
                var simplePattern = pattern[..pattern.IndexOf('<')];
                if (state.NodesByName.TryGetValue(simplePattern, out var genericNameMatches))
                {
                    candidates.AddRange(genericNameMatches);
                }
            }

            if (candidates.Count == 0)
            {
                continue;
            }

            candidates = candidates
                .Where(IsRepositoryType)
                .Where(candidate => Utilities.IsWithinCallerSolution(caller, candidate))
                .DistinctBy(candidate => candidate.Id)
                .ToList();

            if (candidates.Count == 0)
            {
                continue;
            }

            return SelectBestMatch(candidates, caller);
        }

        return null;
    }


    public static GraphNode? TryResolveControlledRepository(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
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
            "I" + simpleArg + "Repository",
            "I" + simpleArg + "ControlledRepository",
            simpleArg + "Repo",
            "I" + simpleArg + "Repo",
            simpleArg + "DataAccess",
            "I" + simpleArg + "DataAccess"
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
                .DistinctBy(r => r.Id)
                .ToList();

            if (repositories.Count == 0)
            {
                continue;
            }

            var sameSolution = repositories
                .Where(r => IsWithinCallerSolution(caller, r))
                .ToList();

            if (sameSolution.Count > 0)
            {
                repositories = sameSolution;
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


    public static void AppendRequestProcessorFlow(
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


}
