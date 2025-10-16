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
    private sealed class FlowRenderState
    {
        public FlowRenderState(
            GraphDocument document,
            IReadOnlyDictionary<string, GraphNode> nodesById,
            IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
            IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> nodesByFqdn,
            IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> nodesByName,
            IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> mapLookup,
            FlowWorkspaceIndex? workspace,
            int? maxDepth)
        {
            Document = document;
            NodesById = nodesById;
            EdgesByFrom = edgesByFrom;
            NodesByFqdn = nodesByFqdn;
            NodesByName = nodesByName;
            MapLookup = mapLookup;
            Workspace = workspace;
            MaxDepth = maxDepth;
        }

        public GraphDocument Document { get; }
        public IReadOnlyDictionary<string, GraphNode> NodesById { get; }
        public IReadOnlyDictionary<string, List<GraphEdge>> EdgesByFrom { get; }
        public IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> NodesByFqdn { get; }
        public IReadOnlyDictionary<string, IReadOnlyList<GraphNode>> NodesByName { get; }
        public IReadOnlyDictionary<(string Source, string Destination), List<GraphNode>> MapLookup { get; }
        public FlowWorkspaceIndex? Workspace { get; }
        public HashSet<string> EndpointStack { get; } = new(StringComparer.Ordinal);
        public HashSet<string> HandlerStack { get; } = new(StringComparer.Ordinal);
        public HashSet<string> NotificationStack { get; } = new(StringComparer.Ordinal);
        public HashSet<string> TargetServiceVisited { get; } = new(StringComparer.Ordinal);
        public HashSet<string> ServiceStack { get; } = new(StringComparer.Ordinal);
        // Deduplication sets to suppress repeated request dispatch and handler expansion noise within a single flow render
        public HashSet<string>? DedupRequests { get; set; }
        public HashSet<string>? DedupHandlers { get; set; }
        public HashSet<string>? ExpandedImplementations { get; set; }
        public HashSet<string>? RenderedEndpoints { get; set; }
        public HashSet<string> RemoteLookupKeys { get; } = new(StringComparer.Ordinal);
        // Track mapping edges printed: key format FromNodeId::ToNodeId::Variable(optional)
        public HashSet<string> PrintedMappings { get; } = new(StringComparer.Ordinal);
        public int? MaxDepth { get; }
        // Reachability filter: if populated, only nodes whose Id is contained will be expanded/emitted.
        public HashSet<string>? AllowedIds { get; set; }
        // Prevent repeated HttpClient remote endpoint expansions (clientId::targetEndpointId::verb::route)
        public HashSet<string> HttpClientExpansionKeys { get; } = new(StringComparer.Ordinal);
        private readonly Stack<ImpactAccumulator> _impactStack = new();
        public ImpactAccumulator? CurrentImpact => _impactStack.Count > 0 ? _impactStack.Peek() : null;
        public void PushImpact(ImpactAccumulator impact) => _impactStack.Push(impact);
        public ImpactAccumulator? PopImpact() => _impactStack.Count > 0 ? _impactStack.Pop() : null;

        public IEnumerable<GraphNode> FindCandidateImplementations(GraphNode serviceNode)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            if (NodesByFqdn.TryGetValue(serviceNode.Fqdn ?? string.Empty, out var fqdnMatches))
            {
                foreach (var candidate in fqdnMatches)
                {
                    if (string.Equals(candidate.Id, serviceNode.Id, StringComparison.Ordinal))
                    {
                        continue;
                    }
                    if (seen.Add(candidate.Id))
                    {
                        yield return candidate;
                    }
                }
            }

            if (NodesByName.TryGetValue(serviceNode.Name ?? string.Empty, out var nameMatches))
            {
                foreach (var candidate in nameMatches)
                {
                    if (string.Equals(candidate.Id, serviceNode.Id, StringComparison.Ordinal))
                    {
                        continue;
                    }
                    if (seen.Add(candidate.Id))
                    {
                        yield return candidate;
                    }
                }
            }
        }

        public IEnumerable<GraphNode> FindHeuristicImplementations(GraphNode serviceNode)
        {
            var yielded = new HashSet<string>(StringComparer.Ordinal);
            var name = serviceNode.Name ?? string.Empty;
            if (name.StartsWith("I", StringComparison.Ordinal) && name.Length > 1)
            {
                var baseName = name[1..];
                if (NodesByName.TryGetValue(baseName, out var impls))
                {
                    foreach (var impl in impls)
                    {
                        if (impl.Id == serviceNode.Id) continue;
                        if (yielded.Add(impl.Id)) yield return impl;
                    }
                }
            }

            // Options generic parameter: IOptions<T>
            var fqdn = serviceNode.Fqdn ?? string.Empty;
            var optionsPrefix = "Microsoft.Extensions.Options.IOptions<";
            if (fqdn.StartsWith(optionsPrefix, StringComparison.Ordinal) && fqdn.EndsWith('>'))
            {
                var inner = fqdn.Substring(optionsPrefix.Length, fqdn.Length - optionsPrefix.Length - 1);
                var simpleInner = inner.Split('.').Last();
                if (NodesByName.TryGetValue(simpleInner, out var optNodes))
                {
                    foreach (var opt in optNodes)
                    {
                        if (yielded.Add(opt.Id)) yield return opt;
                    }
                }
            }

            // Generic interface fallback: strip generic arity / args and leading I
            var genericIndex = name.IndexOf('<');
            if (genericIndex > 0)
            {
                var genericBase = name[..genericIndex];
                if (genericBase.StartsWith("I", StringComparison.Ordinal) && genericBase.Length > 1)
                {
                    genericBase = genericBase[1..];
                }
                if (NodesByName.TryGetValue(genericBase, out var genericCandidates))
                {
                    foreach (var g in genericCandidates)
                    {
                        if (g.Id == serviceNode.Id) continue;
                        if (yielded.Add(g.Id)) yield return g;
                    }
                }
            }

            // Suffix normalization (Service/Provider)
            string Normalize(string s)
            {
                if (s.EndsWith("Service", StringComparison.Ordinal)) return s[..^7];
                if (s.EndsWith("Provider", StringComparison.Ordinal)) return s[..^8];
                return s;
            }
            var normalized = Normalize(name.TrimStart('I'));
            if (NodesByName.TryGetValue(normalized + "Service", out var serviceSuffixImpls))
            {
                foreach (var impl in serviceSuffixImpls)
                {
                    if (impl.Id == serviceNode.Id) continue;
                    if (yielded.Add(impl.Id)) yield return impl;
                }
            }
        }
    }

    private static void AppendImpactSummary(StringBuilder builder, ImpactAccumulator impact)
    {
        if (impact.IsEmpty)
        {
            return;
        }

        AppendIndented(builder, 1, "impact_summary");

        if (impact.RemoteEndpoints.Count > 0)
        {
            var scopeDetail = impact.RemoteScopes.Count > 0
                ? " scopes=" + string.Join(", ", impact.RemoteScopes
                    .OrderByDescending(kv => kv.Value)
                    .ThenBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                    .Select(kv => $"{kv.Key}:{kv.Value}"))
                : string.Empty;
            AppendIndented(builder, 2, $"remote_endpoints {impact.RemoteEndpoints.Count} (calls={impact.RemoteCallCount}){scopeDetail}");
            foreach (var remote in impact.RemoteEndpoints.Values
                         .OrderByDescending(r => r.Count)
                         .ThenBy(r => r.Label, StringComparer.OrdinalIgnoreCase)
                         .ThenBy(r => r.Route, StringComparer.OrdinalIgnoreCase)
                         .Take(5))
            {
                var hostText = string.IsNullOrWhiteSpace(remote.Host) ? string.Empty : $" host={remote.Host}";
                var callKinds = remote.CallKinds.Count > 0
                    ? " [" + string.Join('/', remote.CallKinds.OrderBy(k => k, StringComparer.OrdinalIgnoreCase)) + "]"
                    : string.Empty;
                AppendIndented(builder, 3, $"{remote.Verb} {remote.Route} -> {remote.Label} via {remote.Client}{callKinds} (x{remote.Count}){hostText}");
            }
            if (impact.RemoteEndpoints.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.RemoteEndpoints.Count - 5} more");
            }
            if (impact.MissingRemoteRoutes > 0)
            {
                AppendIndented(builder, 3, $"missing_route_annotations {impact.MissingRemoteRoutes}");
            }
        }

        if (impact.Repositories.Count > 0)
        {
            var totalWrites = impact.Repositories.Values.Sum(r => r.WriteCount);
            var totalReads = impact.Repositories.Values.Sum(r => r.ReadCount);
            AppendIndented(builder, 2, $"repositories {impact.Repositories.Count} (writes={totalWrites}, reads={totalReads})");
            foreach (var repo in impact.Repositories.Values
                         .OrderByDescending(r => r.TotalOperations)
                         .ThenBy(r => r.Name, StringComparer.OrdinalIgnoreCase)
                         .Take(5))
            {
                var entities = repo.Entities
                    .OrderBy(e => e, StringComparer.OrdinalIgnoreCase)
                    .Take(3)
                    .ToList();
                var entitySuffix = repo.Entities.Count > 0
                    ? " entities=" + string.Join(',', entities) + (repo.Entities.Count > entities.Count ? $" +{repo.Entities.Count - entities.Count}" : string.Empty)
                    : string.Empty;
                AppendIndented(builder, 3, $"{repo.Name} writes={repo.WriteCount} reads={repo.ReadCount}{entitySuffix}");
            }
            if (impact.Repositories.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Repositories.Count - 5} more");
            }
        }

        if (impact.Entities.Count > 0)
        {
            var totalEntityWrites = impact.Entities.Values.Sum(e => e.WriteCount);
            var totalEntityReads = impact.Entities.Values.Sum(e => e.ReadCount);
            AppendIndented(builder, 2, $"entities {impact.Entities.Count} (writes={totalEntityWrites}, reads={totalEntityReads})");
            foreach (var entity in impact.Entities.Values
                         .OrderByDescending(e => e.TotalOperations)
                         .ThenBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
                         .Take(5))
            {
                AppendIndented(builder, 3, $"{entity.Name} writes={entity.WriteCount} reads={entity.ReadCount}");
            }
            if (impact.Entities.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Entities.Count - 5} more");
            }
        }

        if (impact.Clients.Count > 0)
        {
            AppendIndented(builder, 2, $"clients {impact.Clients.Count}");
            foreach (var client in impact.Clients.OrderBy(c => c, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, client);
            }
            if (impact.Clients.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Clients.Count - 5} more");
            }
        }

        if (impact.Services.Count > 0)
        {
            AppendIndented(builder, 2, $"services {impact.Services.Count}");
            foreach (var service in impact.Services.OrderBy(s => s, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, service);
            }
            if (impact.Services.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Services.Count - 5} more");
            }
        }

        if (impact.PipelineBehaviors.Count > 0 || impact.GenericPipelineBehaviors > 0)
        {
            var genericSuffix = impact.GenericPipelineBehaviors > 0 ? $" generic={impact.GenericPipelineBehaviors}" : string.Empty;
            AppendIndented(builder, 2, $"pipeline_behaviors {impact.PipelineBehaviors.Count}{genericSuffix}");
            foreach (var behavior in impact.PipelineBehaviors.OrderBy(b => b, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, behavior);
            }
            if (impact.PipelineBehaviors.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.PipelineBehaviors.Count - 5} more");
            }
        }

        if (impact.Requests.Count > 0)
        {
            AppendIndented(builder, 2, $"requests {impact.Requests.Count}");
            foreach (var request in impact.Requests.OrderBy(r => r, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, request);
            }
            if (impact.Requests.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Requests.Count - 5} more");
            }
        }

        if (impact.Handlers.Count > 0)
        {
            AppendIndented(builder, 2, $"handlers {impact.Handlers.Count}");
            foreach (var handler in impact.Handlers.OrderBy(h => h, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, handler);
            }
            if (impact.Handlers.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Handlers.Count - 5} more");
            }
        }

        if (impact.Notifications.Count > 0)
        {
            AppendIndented(builder, 2, $"notifications {impact.Notifications.Count}");
            foreach (var notification in impact.Notifications.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, notification);
            }
            if (impact.Notifications.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Notifications.Count - 5} more");
            }
        }

        if (impact.Messages.Count > 0)
        {
            AppendIndented(builder, 2, $"messages {impact.Messages.Count}");
            foreach (var message in impact.Messages.OrderBy(m => m, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, message);
            }
            if (impact.Messages.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Messages.Count - 5} more");
            }
        }

        if (impact.Caches.Count > 0)
        {
            AppendIndented(builder, 2, $"caches {impact.Caches.Count}");
            foreach (var cache in impact.Caches.OrderBy(c => c, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, cache);
            }
            if (impact.Caches.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Caches.Count - 5} more");
            }
        }

        if (impact.Options.Count > 0)
        {
            AppendIndented(builder, 2, $"options {impact.Options.Count}");
            foreach (var option in impact.Options.OrderBy(o => o, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, option);
            }
            if (impact.Options.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Options.Count - 5} more");
            }
        }

        if (impact.Validators.Count > 0)
        {
            AppendIndented(builder, 2, $"validators {impact.Validators.Count}");
            foreach (var validator in impact.Validators.OrderBy(v => v, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, validator);
            }
            if (impact.Validators.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Validators.Count - 5} more");
            }
        }

        if (impact.Storages.Count > 0)
        {
            AppendIndented(builder, 2, $"storages {impact.Storages.Count}");
            foreach (var storage in impact.Storages.OrderBy(s => s, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, storage);
            }
            if (impact.Storages.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Storages.Count - 5} more");
            }
        }

        if (impact.Mappings.Count > 0)
        {
            AppendIndented(builder, 2, $"mappings {impact.Mappings.Count}");
            foreach (var mapping in impact.Mappings.OrderBy(m => m, StringComparer.OrdinalIgnoreCase).Take(5))
            {
                AppendIndented(builder, 3, mapping);
            }
            if (impact.Mappings.Count > 5)
            {
                AppendIndented(builder, 3, $"+{impact.Mappings.Count - 5} more");
            }
        }
    }

}
