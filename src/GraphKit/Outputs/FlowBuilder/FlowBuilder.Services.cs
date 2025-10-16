using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GraphKit.Graph;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    private static void AppendServiceContractFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode caller,
        GraphNode serviceNode,
        string? methodName,
        int indent)
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }

        var stackKey = $"{serviceNode.Id}:{methodName ?? "*"}";
        if (!state.ServiceStack.Add(stackKey))
        {
            AppendIndented(builder, indent, "... (service recursion detected)");
            return;
        }

        try
        {
            if (serviceNode.Name is { } svcName && svcName.StartsWith("IControlledRepository<", StringComparison.Ordinal))
            {
                var repoImpl = TryResolveControlledRepository(state, caller, serviceNode);
                if (repoImpl != null && repoImpl.Type is "app.repository" or "repository")
                {
                    var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
                    AppendIndented(builder, indent, $"implementation {repoImpl.Fqdn}{methodSuffix}");
                    AppendRepositoryFlow(builder, state, repoImpl, indent + 1);
                    return;
                }
            }

            var candidateList = new List<GraphNode>();
            var seen = new HashSet<string>(StringComparer.Ordinal);

            // Special-case: ILogger<TDependency> where TDependency is an interface/service we already have a contract for.
            // When encountering ILogger<IFooService>, attempt to locate IFooService node(s) and treat their implementations as the logger's implementation details.
            // This prevents a dead-end placeholder under logging wrappers.
            if (serviceNode.Fqdn is { } fqdn && fqdn.StartsWith("Microsoft.Extensions.Logging.ILogger<", StringComparison.Ordinal) && fqdn.EndsWith('>'))
            {
                var inner = fqdn.Substring("Microsoft.Extensions.Logging.ILogger<".Length, fqdn.Length - "Microsoft.Extensions.Logging.ILogger<".Length - 1);
                var simpleInner = inner.Split('.').Last();
                // Prefer interface form (leading I) match first
                IEnumerable<GraphNode> innerCandidates = Array.Empty<GraphNode>();
                if (state.NodesByName.TryGetValue(simpleInner, out var nameMatches))
                {
                    innerCandidates = nameMatches;
                }
                // Also try full FQDN lookup
                if (state.NodesByFqdn.TryGetValue(inner, out var fqdnMatches))
                {
                    innerCandidates = innerCandidates.Concat(fqdnMatches);
                }
                foreach (var innerNode in innerCandidates.DistinctBy(n => n.Id))
                {
                    // Reuse its implementations (implemented_by) or heuristic match
                    if (state.EdgesByFrom.TryGetValue(innerNode.Id, out var innerEdges))
                    {
                        foreach (var implEdge in innerEdges.Where(e => e.Kind == "implemented_by"))
                        {
                            if (!state.NodesById.TryGetValue(implEdge.To, out var implNode)) continue;
                            if (!ShouldIncludeImplementation(caller, implNode)) continue;
                            if (seen.Add(implNode.Id)) candidateList.Add(implNode);
                        }
                    }
                    if (candidateList.Count == 0)
                    {
                        foreach (var candidate in state.FindCandidateImplementations(innerNode))
                        {
                            if (!ShouldIncludeImplementation(caller, candidate)) continue;
                            if (seen.Add(candidate.Id)) candidateList.Add(candidate);
                        }
                    }
                }
                // If we resolved something for the inner type, we can treat those as logger implementation context and still render below.
            }

            // Direct implemented_by edges first
            if (state.EdgesByFrom.TryGetValue(serviceNode.Id, out var serviceEdges))
            {
                foreach (var implEdge in serviceEdges.Where(e => e.Kind == "implemented_by"))
                {
                    if (!state.NodesById.TryGetValue(implEdge.To, out var implementation)) continue;
                    if (!ShouldIncludeImplementation(caller, implementation)) continue;
                    if (seen.Add(implementation.Id)) candidateList.Add(implementation);
                }
            }

            // Fallback: same-name/fqdn candidate implementations
            if (candidateList.Count == 0)
            {
                foreach (var candidate in state.FindCandidateImplementations(serviceNode))
                {
                    if (!ShouldIncludeImplementation(caller, candidate)) continue;
                    if (seen.Add(candidate.Id)) candidateList.Add(candidate);
                }
            }

            if (candidateList.Count > 0 && candidateList.All(c => string.Equals(c.Type, "cqrs.request", StringComparison.OrdinalIgnoreCase)))
            {
                if (TryResolveServiceContractAsRequest(builder, state, caller, serviceNode, methodName, indent))
                {
                    return;
                }
            }

            // Fallback: treat the service node itself as its own implementation if it has edges (self-contained concrete type)
            if (candidateList.Count == 0 && state.EdgesByFrom.TryGetValue(serviceNode.Id, out var directEdges) && directEdges.Count > 0)
            {
                if (seen.Add(serviceNode.Id)) candidateList.Add(serviceNode);
            }

            var callerRoot = GetAssemblyRoot(caller.Assembly);
            var sameRoot = candidateList.Where(c => string.Equals(GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
            if (sameRoot.Count > 0)
            {
                candidateList = sameRoot; // Prefer same-root implementations to reduce cross-solution noise
            }
            else
            {
                // As a secondary filter, if multiple implementations exist across roots, keep only those with a file (internal) to drop heuristic externals.
                var withFiles = candidateList.Where(c => !string.IsNullOrWhiteSpace(c.FilePath) && !c.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase)).ToList();
                if (withFiles.Count > 0)
                {
                    candidateList = withFiles;
                }
            }

            // If still nothing, attempt heuristic search (limited) to surface at least one plausible implementation.
            if (candidateList.Count == 0)
            {
                foreach (var candidate in state.FindHeuristicImplementations(serviceNode).Take(5))
                {
                    if (!ShouldIncludeImplementation(caller, candidate)) continue;
                    if (seen.Add(candidate.Id)) candidateList.Add(candidate);
                }
            }

            if (candidateList.Count == 0)
            {
                if (TryResolveServiceContractAsRequest(builder, state, caller, serviceNode, methodName, indent))
                {
                    return;
                }

                // Attempt collapse using single implementation resolution even if none directly enumerated yet
                var single = TryResolveSingleImplementation(state, caller, serviceNode);
                if (single is null)
                {
                    AppendIndented(builder, indent, "... (no implementation details available)");
                    return;
                }
                candidateList.Add(single);
            }

            // Collapse interface -> single implementation: replace service header with concrete name and suppress explicit implementation node
            if (!string.IsNullOrWhiteSpace(methodName) && serviceNode.Name is { } serviceName && serviceName.StartsWith("I", StringComparison.Ordinal) && candidateList.Count == 1 && candidateList[0].Type == "app.service")
            {
                var impl = candidateList[0];
                // Print concrete implementation header (no separate service header already; parent line printed method)
                AppendServiceImplementationFlow(builder, state, caller, impl, methodName, indent, heuristic: !state.EdgesByFrom.ContainsKey(impl.Id), suppressHeader: false);
                return;
            }

            {
                candidateList = candidateList
                    .GroupBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
                    .Select(g => g
                        .OrderByDescending(c => state.EdgesByFrom.ContainsKey(c.Id))
                        .ThenBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
                        .First())
                    .OrderBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var printed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var impl in candidateList)
                {
                    var key = impl.Fqdn + "::" + (methodName ?? "*");
                    if (!printed.Add(key)) continue;
                    // Refined heuristic detection: only mark as heuristic when no outgoing edges AND no concrete file path (likely synthetic/external)
                    var hasEdges = state.EdgesByFrom.ContainsKey(impl.Id);
                    var implHeuristic = !hasEdges && (string.IsNullOrWhiteSpace(impl.FilePath) || impl.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase));
                    AppendServiceImplementationFlow(builder, state, caller, impl, methodName, indent, heuristic: implHeuristic);
                }
            }
        }
        finally
        {
            state.ServiceStack.Remove(stackKey);
        }
    }

    private static bool TryResolveServiceContractAsRequest(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode caller,
        GraphNode serviceNode,
        string? methodName,
        int indent)
    {
        static void AddMatches(
            FlowRenderState state,
            GraphNode caller,
            string? key,
            HashSet<string> seen,
            List<GraphNode> candidates)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            if (state.NodesByName.TryGetValue(key!, out var nameMatches))
            {
                foreach (var candidate in nameMatches)
                {
                    if (!string.Equals(candidate.Type, "cqrs.request", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!IsWithinCallerSolution(caller, candidate))
                    {
                        continue;
                    }

                    if (seen.Add(candidate.Id))
                    {
                        candidates.Add(candidate);
                    }
                }
            }

            if (state.NodesByFqdn.TryGetValue(key!, out var fqdnMatches))
            {
                foreach (var candidate in fqdnMatches)
                {
                    if (!string.Equals(candidate.Type, "cqrs.request", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!IsWithinCallerSolution(caller, candidate))
                    {
                        continue;
                    }

                    if (seen.Add(candidate.Id))
                    {
                        candidates.Add(candidate);
                    }
                }
            }
        }

        var seen = new HashSet<string>(StringComparer.Ordinal);
        var candidates = new List<GraphNode>();

        AddMatches(state, caller, serviceNode.Name, seen, candidates);

        if (!string.IsNullOrWhiteSpace(serviceNode.Name))
        {
            var genericIndex = serviceNode.Name.IndexOf('<');
            if (genericIndex > 0)
            {
                AddMatches(state, caller, serviceNode.Name[..genericIndex], seen, candidates);
            }
        }

        if (!string.IsNullOrWhiteSpace(serviceNode.Fqdn))
        {
            AddMatches(state, caller, serviceNode.Fqdn, seen, candidates);
            var simple = serviceNode.Fqdn.Split('.').Last();
            if (!string.Equals(simple, serviceNode.Name, StringComparison.Ordinal))
            {
                AddMatches(state, caller, simple, seen, candidates);
            }
        }

        if (candidates.Count == 0)
        {
            return false;
        }

        var callerRoot = GetAssemblyRoot(caller.Assembly);
        var ordered = candidates
            .OrderBy(candidate => string.Equals(GetAssemblyRoot(candidate.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(candidate => candidate.Fqdn ?? candidate.Name ?? candidate.Id, StringComparer.OrdinalIgnoreCase)
            .ToList();

        const int MaxDisplayed = 3;
        var displayed = ordered.Take(MaxDisplayed).ToList();

        foreach (var request in displayed)
        {
            var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
            var requestDisplay = request.Fqdn ?? request.Name ?? request.Id;
            if (state.AllowedIds is { } allowed)
            {
                allowed.Add(request.Id);
                if (state.EdgesByFrom.TryGetValue(request.Id, out var requestEdges))
                {
                    foreach (var edge in requestEdges)
                    {
                        allowed.Add(edge.To);
                    }
                }
            }
            AppendIndented(builder, indent, $"resolves_request {requestDisplay}{methodSuffix}");
            AppendCommandFlow(builder, state, request, indent + 1);
        }

        if (ordered.Count > displayed.Count)
        {
            AppendIndented(builder, indent, $"+{ordered.Count - displayed.Count} additional_requests elided");
        }

        return true;
    }

    private static void AppendServiceImplementationFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode caller,
        GraphNode implementation,
        string? invokedMethod,
        int indent,
        bool heuristic = false,
        bool suppressHeader = false) // heuristic flag retained for future conditional formatting
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }

        state.ExpandedImplementations ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var identity = !string.IsNullOrWhiteSpace(implementation.Fqdn)
            ? implementation.Fqdn!
            : (!string.IsNullOrWhiteSpace(implementation.Name) ? implementation.Name! : implementation.Id);
        var expansionKey = identity + "::" + (invokedMethod ?? "*");
        var alreadyExpanded = state.ExpandedImplementations.Contains(expansionKey);

        var span = implementation.Span;
        var spanText = span is null ? string.Empty : $" [L{span.StartLine}-L{span.EndLine}]";
        var methodSuffix = string.IsNullOrWhiteSpace(invokedMethod) ? string.Empty : $".{invokedMethod}";
        // Only append heuristic suffix when flagged AND we lack concrete evidence (span or internal file or outgoing edges)
        var hasConcreteEvidence = implementation.Span is not null ||
                                   (!string.IsNullOrWhiteSpace(implementation.FilePath) && !implementation.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase)) ||
                                   state.EdgesByFrom.ContainsKey(implementation.Id);
        var heuristicSuffix = heuristic && !hasConcreteEvidence ? " [heuristic]" : string.Empty;
        if (!suppressHeader)
        {
            if (alreadyExpanded)
            {
                AppendIndented(builder, indent, $"implementation {implementation.Fqdn}{methodSuffix} (see previous expansion){heuristicSuffix}");
            }
            else
            {
                AppendIndented(builder, indent, $"implementation {implementation.Fqdn}{methodSuffix}{spanText}{heuristicSuffix}");
            }
        }
        else if (alreadyExpanded)
        {
            AppendIndented(builder, indent, $"implementation {implementation.Fqdn}{methodSuffix} (see previous expansion){heuristicSuffix}");
        }

        if (alreadyExpanded)
        {
            return; // Do not re-expand internals to save memory and avoid recursion loops
        }
        state.ExpandedImplementations.Add(expansionKey);

        var childIndent = suppressHeader ? indent : indent + 1;

        // If a specific method was invoked on an interface/service, attempt to expand only edges whose method prop matches invokedMethod
        if (!string.IsNullOrWhiteSpace(invokedMethod) && state.EdgesByFrom.TryGetValue(implementation.Id, out var methodImplEdges))
        {
            var filtered = methodImplEdges.Where(e => e.Props is { } p && p.TryGetValue("method", out var mv) && string.Equals(mv?.ToString(), invokedMethod, StringComparison.OrdinalIgnoreCase)).ToList();
            if (filtered.Count > 0)
            {
                // Narrow expansion strictly to edges tagged with the invoked method
                AppendGenericServiceNode(builder, state, implementation, invokedMethod, childIndent, suppressSelfHeuristic: true);
                return; // Avoid double-expansion via switch below
            }
            else
            {
                // Fallback: no tagged edges for the method; allow a broader expansion (untagged internal edges) so we don't show an empty block
                AppendGenericServiceNode(builder, state, implementation, null, childIndent, suppressSelfHeuristic: true);
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(implementation.Fqdn) &&
            string.Equals(invokedMethod, "ProcessAsync", StringComparison.OrdinalIgnoreCase) &&
            (implementation.Fqdn.EndsWith(".RequestProcessor", StringComparison.Ordinal) ||
             implementation.Fqdn.EndsWith(".SimpleRequestProcessor", StringComparison.Ordinal)))
        {
            AppendRequestProcessorFlow(builder, state, caller, childIndent);
            return;
        }

        switch (implementation.Type)
        {
            case "cqrs.handler":
                AppendHandlerFlow(builder, state, implementation, childIndent);
                break;
            case "cqrs.request":
                AppendCommandFlow(builder, state, implementation, childIndent);
                break;
            case "app.repository":
            case "repository":
                AppendRepositoryFlow(builder, state, implementation, childIndent);
                break;
            case "app.service":
                // Special handling: treat pipeline behaviors as transparent wrappers that dispatch the same request chain
                if (!string.IsNullOrWhiteSpace(implementation.Fqdn) && implementation.Fqdn.StartsWith("MediatR.IPipelineBehavior<", StringComparison.Ordinal))
                {
                    // Expand internal uses_service edges but avoid recursive noise.
                    if (state.EdgesByFrom.TryGetValue(implementation.Id, out var implEdges))
                    {
                        foreach (var innerService in implEdges.Where(e => e.Kind == "uses_service"))
                        {
                            if (!state.NodesById.TryGetValue(innerService.To, out var innerNode)) continue;
                            if (IsInfrastructureNoiseService(innerNode)) continue;
                            AppendIndented(builder, childIndent, $"uses_service {innerNode.Name}");
                            AppendServiceContractFlow(builder, state, implementation, innerNode, innerService.Props is { } sp && sp.TryGetValue("method", out var mv) ? mv?.ToString() : null, childIndent + 1);
                        }
                    }
                    break;
                }
                goto default;
            default:
                AppendGenericServiceNode(builder, state, implementation, invokedMethod, childIndent, suppressSelfHeuristic: true);
                break;
        }
    }

}
