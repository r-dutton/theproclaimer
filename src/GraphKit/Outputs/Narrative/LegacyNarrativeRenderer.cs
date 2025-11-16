using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GraphKit.Constants;
using GraphKit.Facts;

namespace GraphKit.Outputs.Narrative
{
    public static class LegacyNarrativeRenderer
    {
        public sealed record EndpointNarrative(
            NodeFact Endpoint,
            string ControllerDisplay,
            string ActionName,
            string Text,
            string? Route,
            string Verb,
            string? Auth);

        public static void Render(FactBag bag, string repoRoot, string outPath)
        {
            var entries = Collect(bag, repoRoot);
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(outPath))!);
            using var writer = new StringWriter();
            foreach (var entry in entries)
            {
                writer.Write(entry.Text);
                if (!entry.Text.EndsWith(Environment.NewLine, StringComparison.Ordinal))
                {
                    writer.WriteLine();
                }

                writer.WriteLine();
            }

            var content = writer.ToString().TrimEnd();
            File.WriteAllText(outPath, string.IsNullOrEmpty(content) ? string.Empty : content + Environment.NewLine);
        }

        public static IReadOnlyList<EndpointNarrative> Collect(FactBag bag, string repoRoot)
        {
            var idx = new Index(bag);
            var entries = new List<EndpointNarrative>();

            foreach (var ep in idx.EndpointNodes())
            {
                using var w = new StringWriter();
                var controllerDisplay = FirstNonEmpty(
                    Str(ep, "controller_display"),
                    Str(ep, "fqdn"),
                    Str(ep, "name"),
                    ep.Type) ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(controllerDisplay))
                {
                    w.WriteLine($"## {controllerDisplay}");
                    w.WriteLine();
                }

                PrintEndpointHeader(w, ep, repoRoot);
                PrintUsesServiceTree(w, idx, ep.Id, 1, repoRoot);
                PrintSendsRequestTree(w, idx, ep.Id, 1, repoRoot, new HashSet<string>(StringComparer.OrdinalIgnoreCase));
                PrintHttpCallsTree(w, idx, ep.Id, 1, repoRoot);
                PrintEfTouches(w, idx, ep.Id, 1, repoRoot, includeServiceEdges: false);
                var text = w.ToString().TrimEnd();
                if (!string.IsNullOrEmpty(text))
                {
                    text += Environment.NewLine;
                }

                var actionName = FirstNonEmpty(Str(ep, "name"), Str(ep, "fqdn"), ep.Type) ?? ep.Id;
                var route = FirstNonEmpty(Str(ep, PropKeys.Route), Str(ep, "route"));
                var verb = FirstNonEmpty(Str(ep, PropKeys.Verb), Str(ep, "http_method")) ?? string.Empty;
                var auth = FirstNonEmpty(Str(ep, "auth"));

                entries.Add(new EndpointNarrative(ep, controllerDisplay, actionName, text, route, verb, auth));
            }

            return entries
                .OrderBy(e => e.ControllerDisplay, StringComparer.OrdinalIgnoreCase)
                .ThenBy(e => e.Route, StringComparer.OrdinalIgnoreCase)
                .ThenBy(e => e.Verb, StringComparer.OrdinalIgnoreCase)
                .ThenBy(e => e.ActionName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static void PrintEndpointHeader(StringWriter w, NodeFact ep, string repoRoot)
        {
            var verb = FirstNonEmpty(Str(ep, PropKeys.Verb), Str(ep, "http_method")) ?? string.Empty;
            var route = FirstNonEmpty(Str(ep, PropKeys.Route), Str(ep, "route")) ?? string.Empty;
            var ctrl = FirstNonEmpty(Str(ep, "controller_display"), Str(ep, "fqdn"), Str(ep, "name")) ?? ep.Type;
            var auth = FirstNonEmpty(Str(ep, "auth")) ?? "user";
            var status = "200";
            if (ep.Props.TryGetValue("status_codes", out var statusObj))
            {
                if (statusObj is IEnumerable<object?> statusEnumerable)
                {
                    var codes = statusEnumerable
                        .Select(code => code?.ToString())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToArray();
                    if (codes.Length > 0)
                    {
                        status = string.Join(",", codes);
                    }
                }
                else if (statusObj is int singleStatus)
                {
                    status = singleStatus.ToString();
                }
            }
            var link = SourceLink(ep, repoRoot);
            w.WriteLine($"[[web] {verb} {route}  ({ctrl})]({link}) status={status} [auth={auth}]");
        }

        private static void PrintUsesServiceTree(StringWriter w, Index idx, string fromId, int indent, string repoRoot)
        {
            var visited = new HashSet<(string From, string To)>();
            var serviceStack = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var grouped = idx.Out(fromId, EdgeKinds.UsesService)
                .GroupBy(edge => ServiceGroupKey(idx, edge), StringComparer.OrdinalIgnoreCase);

            foreach (var group in grouped)
            {
                RenderServiceUsage(w, idx, group.ToList(), indent, repoRoot, visited, serviceStack);
            }
        }

        private static string ServiceGroupKey(Index idx, EdgeFact edge)
        {
            var node = idx.Node(edge.ToId);
            return FirstNonEmpty(
                Str(edge, "service_type"),
                Str(node, "fqdn"),
                Str(node, "name"),
                edge.ToId) ?? edge.ToId;
        }

        private static void RenderServiceUsage(
            StringWriter w,
            Index idx,
            IReadOnlyList<EdgeFact> edges,
            int indent,
            string repoRoot,
            HashSet<(string From, string To)> visitedEdges,
            HashSet<string> serviceStack)
        {
            if (edges.Count == 0)
            {
                return;
            }

            var primary = edges[0];
            var sourceId = primary.FromId;
            var targetNode = idx.Node(primary.ToId);
            var targetFqdn = FirstNonEmpty(Str(targetNode, "fqdn"));
            var sourceNode = idx.Node(sourceId);
            var sourceFqdn = FirstNonEmpty(Str(sourceNode, "fqdn"));
            var groupingKey = ServiceGroupKey(idx, primary);

            if (!string.IsNullOrWhiteSpace(sourceId) &&
                !visitedEdges.Add((sourceId, groupingKey)))
            {
                return;
            }

            var serviceLabel = FirstNonEmpty(
                Str(primary, "service_type"),
                Str(targetNode, "service_type"),
                Str(targetNode, "name"),
                Str(targetNode, "fqdn"),
                targetNode?.Type) ?? "service";

            if (IsRequestProcessorService(serviceLabel))
            {
                return;
            }

            if (targetNode is not null &&
                !string.IsNullOrWhiteSpace(sourceId) &&
                string.Equals(targetNode.Id, sourceId, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(targetFqdn) &&
                !string.IsNullOrWhiteSpace(sourceFqdn) &&
                string.Equals(targetFqdn, sourceFqdn, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var link = SourceLink(primary, repoRoot);
            Indent(w, indent);
            w.WriteLine($"- [uses_service {serviceLabel}]({link})");

            var methodValues = edges
                .Select(e => FirstNonEmpty(Str(e, "method")))
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var summarizeInfrastructure = ShouldSummarizeInfrastructure(targetNode);

            foreach (var method in methodValues)
            {
                Indent(w, indent + 1);
                w.WriteLine($"- [method {method}]({link})");
            }

            var contractValues = edges
                .Select(e => (Contract: Str(e, "invoked_method"), Method: FirstNonEmpty(Str(e, "method"))))
                .Where(pair =>
                    !string.IsNullOrWhiteSpace(pair.Contract) &&
                    !string.Equals(pair.Contract, pair.Method, StringComparison.OrdinalIgnoreCase))
                .Select(pair => pair.Contract!)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var contract in contractValues)
            {
                Indent(w, indent + 1);
                w.WriteLine($"- [contract {contract}]({link})");
            }

            if (summarizeInfrastructure)
            {
                Indent(w, indent + 1);
                w.WriteLine("- (infrastructure service; additional details suppressed)");
                return;
            }

            var branchStack = new HashSet<string>(serviceStack, StringComparer.OrdinalIgnoreCase);
            var branchVisited = new HashSet<(string From, string To)>(visitedEdges);
            var expandedNode = targetNode;
            if (expandedNode is not null &&
                string.Equals(expandedNode.Type, "app.service_contract", StringComparison.OrdinalIgnoreCase))
            {
                expandedNode = idx.ServiceImplementationForContract(expandedNode) ?? expandedNode;
            }

            RenderServiceDetails(w, idx, expandedNode, indent + 1, repoRoot, branchVisited, branchStack);
        }

        private static void RenderRepositoryEntity(
            StringWriter w,
            Index idx,
            string controllerId,
            string entityId,
            int indent,
            string repoRoot,
            HashSet<string> renderedEntityIds)
        {
            if (!renderedEntityIds.Add(entityId))
            {
                return;
            }

            var entity = idx.Node(entityId);
            if (entity is null)
            {
                return;
            }

            foreach (var read in idx.Out(controllerId, EdgeKinds.Queries).Where(edge => edge.ToId == entityId))
            {
                var entityLabel = FirstNonEmpty(Str(entity, "name"), Str(entity, "fqdn"), entity?.Type) ?? "entity";
                var operation = FirstNonEmpty(Str(read, "operation"));
                var link = SourceLink(read, repoRoot);
                Indent(w, indent);
                var label = string.IsNullOrWhiteSpace(operation) ? entityLabel : $"{operation} {entityLabel}";
                w.WriteLine($"- [{label}]({link})");

                var tableEdges = idx.Out(entityId, EdgeKinds.ReadsFrom).ToList();
                if (tableEdges.Count == 0)
                {
                    var tableName = FirstNonEmpty(Str(entity, "table"));
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        var tableLink = SourceLink(entity, repoRoot);
                        Indent(w, indent + 1);
                        w.WriteLine($"- [reads_from {tableName}]({tableLink})");
                    }
                }

                foreach (var tableEdge in tableEdges)
                {
                    var tableNode = idx.Node(tableEdge.ToId);
                    var tableLabel = FirstNonEmpty(Str(tableNode, "table"), Str(tableNode, "name"), Str(tableNode, "fqdn"), tableNode?.Type) ?? "table";
                    var tableLink = SourceLink(tableEdge, repoRoot);
                    Indent(w, indent + 1);
                    w.WriteLine($"- [reads_from {tableLabel}]({tableLink})");
                }
            }

            foreach (var kind in new[] { "writes_to", "updates", "inserts_into", "deletes_from", "upserts" })
            {
                foreach (var edge in idx.Out(controllerId, kind).Where(e => e.ToId == entityId))
                {
                    var entityLabel = FirstNonEmpty(Str(entity, "name"), Str(entity, "fqdn"), entity?.Type) ?? "entity";
                    var link = SourceLink(edge, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [{kind} {entityLabel}]({link})");

                    foreach (var tableEdge in idx.Out(entityId, EdgeKinds.ReadsFrom))
                    {
                        var tableNode = idx.Node(tableEdge.ToId);
                        var tableLabel = FirstNonEmpty(Str(tableNode, "table"), Str(tableNode, "name"), Str(tableNode, "fqdn"), tableNode?.Type) ?? "table";
                        var tableLink = SourceLink(tableEdge, repoRoot);
                        Indent(w, indent + 1);
                        w.WriteLine($"- [{kind} {tableLabel}]({tableLink})");
                    }
                }

                if (w.GetStringBuilder().Length == initialLength)
                {
                    PrintServiceSurface(w, idx, serviceNode.Id, indent, repoRoot);
                }
            }
            finally
            {
                serviceStack.Remove(serviceNode.Id);
            }
        }

        private static bool MatchesCallUsage(EdgeFact call, int usageLine, string? usageRoute, string? usageVerb, string? usageMethod)
        {
            var callLine = ParseLineNumber(Str(call, "line"));
            if (usageLine > 0 && callLine > 0 && callLine != usageLine)
            {
                return false;
            }

            var callVerb = FirstNonEmpty(Str(call, PropKeys.Verb));
            if (!string.IsNullOrWhiteSpace(usageVerb) &&
                !string.IsNullOrWhiteSpace(callVerb) &&
                !string.Equals(usageVerb, callVerb, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var callRoute = FirstNonEmpty(Str(call, PropKeys.Route));
            if (!string.IsNullOrWhiteSpace(usageRoute) &&
                !string.IsNullOrWhiteSpace(callRoute) &&
                !RoutesMatchWithTokens(usageRoute, callRoute))
            {
                return false;
            }

            var callMethod = FirstNonEmpty(Str(call, "method"));
            if (!string.IsNullOrWhiteSpace(usageMethod) &&
                !string.IsNullOrWhiteSpace(callMethod) &&
                !string.Equals(usageMethod, callMethod, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static void RenderServiceDetails(
            StringWriter w,
            Index idx,
            NodeFact? serviceNode,
            int indent,
            string repoRoot,
            HashSet<(string From, string To)> visitedEdges,
            HashSet<string> serviceStack)
        {
            if (serviceNode is null)
            {
                return;
            }

            if (!serviceStack.Add(serviceNode.Id))
            {
                Indent(w, indent);
                w.WriteLine("- ... (service recursion detected)");
                PrintServiceSurface(w, idx, serviceNode.Id, indent + 1, repoRoot);
                return;
            }

            try
            {
                var initialLength = w.GetStringBuilder().Length;
                PrintEfTouches(w, idx, serviceNode.Id, indent, repoRoot, serviceStack);

                foreach (var storageEdge in idx.Out(serviceNode.Id, EdgeKinds.UsesStorage))
                {
                    var storageNode = idx.Node(storageEdge.ToId);
                    var storageLabel = FirstNonEmpty(
                        Str(storageEdge, "service_type"),
                        Str(storageNode, "service_type"),
                        Str(storageNode, "name"),
                        Str(storageNode, "fqdn"),
                        storageNode?.Type) ?? "storage";
                    var storageLink = SourceLink(storageEdge, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [uses_service {storageLabel}]({storageLink})");
                }

                foreach (var callEdge in idx.Out(serviceNode.Id, EdgeKinds.Calls))
                {
                    var callNode = idx.Node(callEdge.ToId);
                    var callLabel = FirstNonEmpty(
                        Str(callEdge, "name"),
                        Str(callNode, "name"),
                        Str(callNode, "fqdn"),
                        callNode?.Type) ?? "call";
                    var callLink = SourceLink(callEdge, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [calls {callLabel}]({callLink})");
                }

                var nestedGroups = idx.Out(serviceNode.Id, EdgeKinds.UsesService)
                    .GroupBy(edge => ServiceGroupKey(idx, edge), StringComparer.OrdinalIgnoreCase);
                foreach (var group in nestedGroups)
                {
                    var primary = group.First();
                    var key = (serviceNode.Id, ServiceGroupKey(idx, primary));
                    if (!visitedEdges.Add(key))
                    {
                        continue;
                    }

                    RenderServiceUsage(w, idx, group.ToList(), indent, repoRoot, visitedEdges, serviceStack);
                }

                if (w.GetStringBuilder().Length == initialLength)
                {
                    PrintServiceSurface(w, idx, serviceNode.Id, indent, repoRoot);
                }
            }
            finally
            {
                serviceStack.Remove(serviceNode.Id);
            }
        }

        private static void PrintSendsRequestTree(StringWriter w, Index idx, string fromId, int indent, string repoRoot, HashSet<string> visited)
        {
            if (!visited.Add(fromId))
            {
                return;
            }

            var callerNode = idx.Node(fromId);
            var preferredAssembly = Str(callerNode, "assembly");
            var preferredProject = Str(callerNode, "project");

            var requestGroups = idx.Out(fromId, EdgeKinds.SendsRequest)
                .GroupBy(edge =>
                {
                    var requestType = Str(edge, "request_type");
                    var requestKey = FirstNonEmpty(
                        Short(requestType),
                        requestType,
                        edge.ToId,
                        Str(edge, "invocation")) ?? Guid.NewGuid().ToString("N");

                    return requestKey;
                }, StringComparer.OrdinalIgnoreCase);

            foreach (var group in requestGroups)
            {
                var processorEdge = group.FirstOrDefault(IsRequestProcessorEdge);
                var edge = processorEdge ?? group.First();

                if (processorEdge is not null)
                {
                    RenderRequestProcessorInvocation(w, idx, fromId, processorEdge, indent, repoRoot, visited, preferredAssembly, preferredProject);
                    continue;
                }

                RenderStandardDispatch(w, idx, edge, indent, repoRoot, visited, preferredAssembly, preferredProject);
            }

            visited.Remove(fromId);
        }

        private static bool IsRequestProcessorEdge(EdgeFact edge)
        {
            if (edge is null)
            {
                return false;
            }

            var serviceName = FirstNonEmpty(Str(edge, "service"));
            var invocationName = FirstNonEmpty(Str(edge, "invocation"));

            return IsRequestProcessorService(serviceName) ||
                   (!string.IsNullOrWhiteSpace(invocationName) &&
                    (invocationName.Equals("Process", StringComparison.OrdinalIgnoreCase) ||
                     invocationName.Equals("ProcessAsync", StringComparison.OrdinalIgnoreCase)));
        }

        private static void RenderStandardDispatch(
            StringWriter w,
            Index idx,
            EdgeFact dispatchEdge,
            int indent,
            string repoRoot,
            HashSet<string> visited,
            string? preferredAssembly,
            string? preferredProject)
        {
            var req = Str(dispatchEdge, "request_type");
            var resp = Str(dispatchEdge, "response_type");
            var link = SourceLink(dispatchEdge, repoRoot);

            var requestLabel = Short(req);
            if (string.IsNullOrWhiteSpace(requestLabel))
            {
                requestLabel = FirstNonEmpty(req) ?? "request";
            }

            string? responseLabel = null;
            if (!string.IsNullOrWhiteSpace(resp))
            {
                responseLabel = Short(resp);
                if (string.IsNullOrWhiteSpace(responseLabel))
                {
                    responseLabel = resp.Split('.').Last();
                }
            }

            var descriptor = responseLabel is { Length: > 0 }
                ? $"{requestLabel} : {responseLabel}"
                : requestLabel;

            Indent(w, indent);
            w.WriteLine($"- [dispatches {descriptor}]({link})");

            var behaviors = idx.PipelineBehaviorsForRequest(dispatchEdge.ToId);
            if (behaviors.Count > 0)
            {
                Indent(w, indent + 1);
                w.WriteLine($"- generic_pipeline_behaviors {behaviors.Count}");
                foreach (var b in behaviors)
                {
                    Indent(w, indent + 2);
                    w.WriteLine($"- {b}");
                }
            }

            RenderHandlerBlock(w, idx, dispatchEdge.ToId, indent + 1, repoRoot, visited, preferredAssembly, preferredProject);
        }

        private static void RenderRequestProcessorInvocation(StringWriter w, Index idx, string fromId, EdgeFact dispatchEdge, int indent, string repoRoot, HashSet<string> visited, string? preferredAssembly, string? preferredProject)
        {
            var serviceEdge = FindMatchingRequestProcessorServiceEdge(idx, fromId, dispatchEdge);
            var serviceLink = serviceEdge is not null ? SourceLink(serviceEdge, repoRoot) : SourceLink(dispatchEdge, repoRoot);

            var methodName = FirstNonEmpty(
                serviceEdge is not null ? Str(serviceEdge, "method") : null,
                serviceEdge is not null ? Str(serviceEdge, "invoked_method") : null,
                Str(dispatchEdge, "invocation"),
                "ProcessAsync");

            var contractMethod = FirstNonEmpty(serviceEdge is not null ? Str(serviceEdge, "invoked_method") : null);

            var executionMethod = FirstNonEmpty(contractMethod, Str(dispatchEdge, "invocation"), methodName, "ProcessAsync")!;
            Indent(w, indent);
            w.WriteLine($"- [request_processor {executionMethod}]({serviceLink})");

            RenderRequestProcessorDispatch(w, idx, dispatchEdge, indent + 1, executionMethod, repoRoot, visited, preferredAssembly, preferredProject);
        }

        private static void RenderRequestProcessorDispatch(StringWriter w, Index idx, EdgeFact dispatchEdge, int indent, string executionMethod, string repoRoot, HashSet<string> visited, string? preferredAssembly, string? preferredProject)
        {
            var req = Str(dispatchEdge, "request_type");
            var resp = Str(dispatchEdge, "response_type");

            var requestLabel = Short(req);
            if (string.IsNullOrWhiteSpace(requestLabel))
            {
                requestLabel = string.IsNullOrWhiteSpace(req) ? "request" : req!.Split('.').Last();
            }

            var responseLabel = Short(resp);
            if (string.IsNullOrWhiteSpace(responseLabel))
            {
                responseLabel = string.IsNullOrWhiteSpace(resp) ? "void" : resp!.Split('.').Last();
            }

            Indent(w, indent);
            w.WriteLine($"- implementation RequestProcessor.{executionMethod} [heuristic]");
            Indent(w, indent + 1);
            w.WriteLine($"- constructs RequestProcessorWrapper<{requestLabel},{responseLabel}>");
            Indent(w, indent + 1);
            w.WriteLine($"- resolves IPipelineBehavior<{requestLabel},{responseLabel}> chain");
            Indent(w, indent + 1);
            w.WriteLine($"- invokes IAsyncRequestHandler<{requestLabel},{responseLabel}>.Handle");

            var link = SourceLink(dispatchEdge, repoRoot);
            Indent(w, indent + 1);
            w.WriteLine($"- [dispatches {requestLabel} : {responseLabel}]({link})");

            var behaviors = idx.PipelineBehaviorsForRequest(dispatchEdge.ToId);
            if (behaviors.Count > 0)
            {
                Indent(w, indent + 2);
                w.WriteLine($"- generic_pipeline_behaviors {behaviors.Count}");
                foreach (var b in behaviors)
            {
                Indent(w, indent + 3);
                w.WriteLine($"- {b}");
            }
            }

            RenderHandlerBlock(w, idx, dispatchEdge.ToId, indent + 2, repoRoot, visited, preferredAssembly, preferredProject);
        }

        private static void RenderHandlerBlock(StringWriter w, Index idx, string requestNodeId, int indent, string repoRoot, HashSet<string> visited, string? preferredAssembly, string? preferredProject)
        {
            var handlerInfo = idx.FindHandlerForRequest(requestNodeId); // to add: preferredAssembly, preferredProject
            if (handlerInfo is null)
            {
                return;
            }

            var (handlerEdge, handlerNode, handlerDisplay) = handlerInfo.Value;
            var hlink = SourceLink(handlerNode, repoRoot);
            Indent(w, indent);
            w.WriteLine($"- [handled_by {handlerDisplay}]({hlink})");

            PrintHttpFromHandler(w, idx, handlerNode.Id, indent + 1, repoRoot);
            PrintEfTouches(w, idx, handlerNode.Id, indent + 1, repoRoot);
            PrintSendsRequestTree(w, idx, handlerNode.Id, indent + 1, repoRoot, visited);
        }

        private static EdgeFact? FindMatchingRequestProcessorServiceEdge(Index idx, string fromId, EdgeFact dispatchEdge)
        {
            var invocation = Str(dispatchEdge, "invocation");
            var dispatchLine = ParseLineNumber(Str(dispatchEdge, "line"));

            foreach (var serviceEdge in idx.Out(fromId, EdgeKinds.UsesService))
            {
                var targetNode = idx.Node(serviceEdge.ToId);
                var serviceLabel = FirstNonEmpty(
                    Str(serviceEdge, "service_type"),
                    Str(targetNode, "service_type"),
                    Str(targetNode, "name"),
                    Str(targetNode, "fqdn"),
                    targetNode?.Type);

                if (!IsRequestProcessorService(serviceLabel))
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(invocation))
                {
                    var serviceInvocation = FirstNonEmpty(Str(serviceEdge, "method"), Str(serviceEdge, "invoked_method"));
                    if (!string.IsNullOrWhiteSpace(serviceInvocation) &&
                        string.Equals(serviceInvocation, invocation, StringComparison.OrdinalIgnoreCase))
                    {
                        return serviceEdge;
                    }
                }

                var serviceLine = ParseLineNumber(Str(serviceEdge, "start_line"));
                if (serviceLine <= 0)
                {
                    serviceLine = ParseLineNumber(Str(serviceEdge, "line"));
                }

                if (dispatchLine > 0 && serviceLine == dispatchLine)
                {
                    return serviceEdge;
                }
            }

            return null;
        }

        private static void PrintHttpFromHandler(StringWriter w, Index idx, string fromId, int indent, string repoRoot)
        {
            foreach (var usesClient in idx.Out(fromId, EdgeKinds.UsesClient))
            {
                var method = FirstNonEmpty(Str(usesClient, PropKeys.ClientMethod));
                var target = FirstNonEmpty(Str(usesClient, "target_service"));
                var toNode = idx.Node(usesClient.ToId);
                var verb = FirstNonEmpty(Str(toNode, PropKeys.Verb), Str(usesClient, PropKeys.Verb), Str(toNode, "http_method")) ?? string.Empty;
                var route = FirstNonEmpty(Str(toNode, PropKeys.Route), Str(usesClient, PropKeys.Route)) ?? string.Empty;
                var clientLabel = FirstNonEmpty(Str(toNode, "name"), Str(toNode, "fqdn"), toNode?.Type) ?? "client";
                var link = SourceLink(usesClient, repoRoot);
                var clientMeta = new List<string>();
                if (!string.IsNullOrWhiteSpace(method)) clientMeta.Add($"method={method}");
                if (!string.IsNullOrWhiteSpace(target)) clientMeta.Add($"target={target}");
                var clientSuffix = clientMeta.Count > 0 ? $" ({string.Join(", ", clientMeta)})" : string.Empty;
                Indent(w, indent);
                w.WriteLine($"- [uses_client {clientLabel}{clientSuffix}]({link})");

                var usageLine = ParseLineNumber(Str(usesClient, "line"));
                if (usageLine <= 0)
                {
                    usageLine = ParseLineNumber(Str(usesClient, "start_line"));
                }
                if (usageLine <= 0)
                {
                    usageLine = ParseLineNumber(Str(usesClient, "end_line"));
                }

                var callEdges = idx.Out(toNode!.Id, EdgeKinds.Calls)
                    .Where(call => MatchesCallUsage(call, usageLine, route, verb, method))
                    .ToList();
                var printedAnyCall = callEdges.Count > 0;
                foreach (var call in callEdges)
                {
                    var ep = idx.Node(call.ToId);
                    var callMethod = FirstNonEmpty(Str(usesClient, PropKeys.ClientMethod), Str(call, "method"));
                    var clink = SourceLink(call, repoRoot);
                    var callVerb = FirstNonEmpty(Str(call, PropKeys.Verb), Str(ep, PropKeys.Verb), Str(ep, "http_method"), verb);
                    var callRoute = FirstNonEmpty(Str(call, PropKeys.Route), Str(ep, PropKeys.Route), Str(ep, "route"), route);
                    var descriptionParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(callVerb)) descriptionParts.Add(callVerb!);
                    if (!string.IsNullOrWhiteSpace(callRoute)) descriptionParts.Add(callRoute!);
                    var descriptor = string.Join(" ", descriptionParts).Trim();
                    var metaParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(callMethod)) metaParts.Add($"method={callMethod}");
                    if (!string.IsNullOrWhiteSpace(target)) metaParts.Add($"target={target}");
                    var segments = new List<string>();
                    if (!string.IsNullOrWhiteSpace(descriptor)) segments.Add(descriptor);
                    if (metaParts.Count > 0) segments.Add(string.Join(", ", metaParts));
                    var inner = segments.Count > 0 ? string.Join(", ", segments) : string.Empty;
                    Indent(w, indent + 1);
                    var callBody = string.IsNullOrWhiteSpace(inner) ? string.Empty : inner;
                    var callLine = string.IsNullOrWhiteSpace(callBody)
                        ? $"- [calls {ControllerActionName(ep)}]({clink})"
                        : $"- [calls {ControllerActionName(ep)} ({callBody})]({clink})";
                    w.WriteLine(callLine);
                    printedAnyCall = true;
                    if (!string.IsNullOrEmpty(target))
                    {
                        Indent(w, indent + 2);
                        w.WriteLine($"- target_service {target}");
                        var link2 = SourceLink(ep!, repoRoot);
                        Indent(w, indent + 3);
                        var authValue = FirstNonEmpty(Str(ep!, "auth")) ?? "user";
                        var nestedVerb = string.IsNullOrWhiteSpace(callVerb) ? verb : callVerb!;
                        var nestedRoute = string.IsNullOrWhiteSpace(callRoute) ? route : callRoute!;
                        w.WriteLine($"- [[web] {nestedVerb} {nestedRoute}  ({ControllerActionName(ep)})]({link2}) status=200 [auth={authValue}]");
                    }
                }

                // Fallback: synthesize calls when facts don't contain explicit call edges
                if (!printedAnyCall && HasLiteralSegments(route) && !string.IsNullOrWhiteSpace(target))
                {
                    foreach (var ep in idx.EndpointNodes())
                    {
                        var epVerb = FirstNonEmpty(Str(ep, PropKeys.Verb), Str(ep, "http_method"));
                        var epRoute = FirstNonEmpty(Str(ep, PropKeys.Route), Str(ep, "route"));
                        if (!RoutesMatchWithTokens(route, epRoute))
                        {
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(verb) && !string.IsNullOrWhiteSpace(epVerb) && !string.Equals(verb, epVerb, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        var clink2 = SourceLink(ep, repoRoot);
                        var descParts = new List<string>();
                        if (!string.IsNullOrWhiteSpace(verb)) descParts.Add(verb);
                        if (!string.IsNullOrWhiteSpace(route)) descParts.Add(route);
                        var metaParts2 = new List<string>();
                        if (!string.IsNullOrWhiteSpace(method)) metaParts2.Add($"method={method}");
                        if (!string.IsNullOrWhiteSpace(target)) metaParts2.Add($"target={target}");
                        var segments2 = new List<string>();
                        if (descParts.Count > 0) segments2.Add(string.Join(" ", descParts));
                        if (metaParts2.Count > 0) segments2.Add(string.Join(", ", metaParts2));
                        var inner2 = segments2.Count > 0 ? string.Join(", ", segments2) : string.Empty;
                        Indent(w, indent + 1);
                        var callBody2 = string.IsNullOrWhiteSpace(inner2) ? string.Empty : inner2;
                        var callLine2 = string.IsNullOrWhiteSpace(callBody2)
                            ? $"- [calls {ControllerActionName(ep)}]({clink2})"
                            : $"- [calls {ControllerActionName(ep)} ({callBody2})]({clink2})";
                        w.WriteLine(callLine2);
                        if (!string.IsNullOrEmpty(target))
                        {
                            Indent(w, indent + 2);
                            w.WriteLine($"- target_service {target}");
                            Indent(w, indent + 3);
                            var authValue2 = FirstNonEmpty(Str(ep, "auth")) ?? "user";
                            w.WriteLine($"- [[web] {epVerb} {epRoute}  ({ControllerActionName(ep)})]({clink2}) status=200 [auth={authValue2}]");
                        }
                    }
                }
            }
        }

        private static void PrintHttpCallsTree(StringWriter w, Index idx, string fromId, int indent, string repoRoot)
        {
            PrintHttpFromHandler(w, idx, fromId, indent, repoRoot);
        }

        private static void PrintEfTouches(StringWriter w, Index idx, string fromId, int indent, string repoRoot, HashSet<string>? serviceStack = null, bool includeServiceEdges = true)
        {
            serviceStack ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var renderedEntityIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var currentNode = idx.Node(fromId);
            var trackCurrent = currentNode is not null && IsServiceLike(currentNode.Type);
            var addedCurrent = trackCurrent && serviceStack.Add(fromId);

            try
            {
                var callEdges = idx.Out(fromId, EdgeKinds.Calls).ToList();

                // Repository-specific rendering
                foreach (var repoGroup in callEdges
                             .Where(edge => string.Equals(idx.Node(edge.ToId)?.Type, NodeTypes.AppRepository, StringComparison.OrdinalIgnoreCase))
                             .GroupBy(edge => edge.ToId, StringComparer.OrdinalIgnoreCase))
                {
                    var repoNode = idx.Node(repoGroup.Key);
                    var repoLabel = FirstNonEmpty(Str(repoNode, "name"), Str(repoNode, "fqdn"), repoNode?.Type) ?? "repository";
                    var repoLink = SourceLink(repoGroup.First(), repoRoot);

                    Indent(w, indent);
                    w.WriteLine($"- [repository {repoLabel}]({repoLink})");

                    foreach (var repoCall in repoGroup)
                    {
                        var invocation = FirstNonEmpty(Str(repoCall, "invoked_method"), Str(repoCall, "method"));
                        var operation = FirstNonEmpty(Str(repoCall, "operation"));
                        var methodLabel = string.IsNullOrWhiteSpace(operation)
                            ? invocation
                            : $"{operation} {invocation}".Trim();

                        if (!string.IsNullOrWhiteSpace(methodLabel))
                        {
                            Indent(w, indent + 1);
                            w.WriteLine($"- [method {methodLabel}]({repoLink})");
                        }
                    }

                    var linkedEntityId = repoGroup
                        .Select(edge => Str(edge, "entity_id"))
                        .FirstOrDefault(val => !string.IsNullOrWhiteSpace(val));

                    if (!string.IsNullOrWhiteSpace(linkedEntityId))
                    {
                        RenderRepositoryEntity(w, idx, fromId, linkedEntityId!, indent + 1, repoRoot, renderedEntityIds);
                    }
                }

                foreach (var call in callEdges.Where(edge => !string.Equals(idx.Node(edge.ToId)?.Type, NodeTypes.AppRepository, StringComparison.OrdinalIgnoreCase)))
                {
                    var target = idx.Node(call.ToId);
                    var targetLabel = FirstNonEmpty(Str(target, "name"), Str(target, "fqdn"), target?.Type) ?? "target";
                    var method = FirstNonEmpty(Str(call, "method"));
                    var operation = FirstNonEmpty(Str(call, "operation"));
                    var label = targetLabel;
                    if (!string.IsNullOrWhiteSpace(method))
                    {
                        label = $"{targetLabel}.{method}";
                    }
                    if (!string.IsNullOrWhiteSpace(operation))
                    {
                        label = $"{operation} {label}";
                    }
                    var link = SourceLink(call, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [calls {label}]({link})");
                }

                var serviceVisited = new HashSet<(string From, string To)>();
                if (includeServiceEdges)
                {
                    foreach (var service in idx.Out(fromId, EdgeKinds.UsesService))
                    {
                        var branchVisited = new HashSet<(string From, string To)>(serviceVisited);
                        var branchStack = new HashSet<string>(serviceStack, StringComparer.OrdinalIgnoreCase);
                        RenderServiceUsage(
                            w,
                            idx,
                            new[] { service },
                            indent,
                            repoRoot,
                            branchVisited,
                            branchStack);
                    }
                }

                // Entity reads (queries)
                foreach (var read in idx.Out(fromId, EdgeKinds.Queries))
                {
                    if (renderedEntityIds.Contains(read.ToId))
                    {
                        continue;
                    }

                    var entity = idx.Node(read.ToId);
                    var entityLabel = FirstNonEmpty(Str(entity, "name"), Str(entity, "fqdn"), entity?.Type) ?? "entity";
                    var operation = FirstNonEmpty(Str(read, "operation"));
                    var link = SourceLink(read, repoRoot);
                    Indent(w, indent);
                    var label = string.IsNullOrWhiteSpace(operation)
                        ? entityLabel
                        : $"{operation} {entityLabel}";
                    w.WriteLine($"- [{label}]({link})");

                    foreach (var tableEdge in idx.Out(read.ToId, EdgeKinds.ReadsFrom))
                    {
                        var tableNode = idx.Node(tableEdge.ToId);
                        var tableLabel = FirstNonEmpty(Str(tableNode, "table"), Str(tableNode, "name"), Str(tableNode, "fqdn"), tableNode?.Type) ?? "table";
                        var tableLink = SourceLink(tableEdge, repoRoot);
                        Indent(w, indent + 1);
                        w.WriteLine($"- [reads_from {tableLabel}]({tableLink})");
                    }
                }

                // Entity writes/updates/deletes/upserts
                foreach (var kind in new[] { "writes_to", "updates", "inserts_into", "deletes_from", "upserts" })
                {
                    foreach (var edge in idx.Out(fromId, kind))
                    {
                        if (renderedEntityIds.Contains(edge.ToId))
                        {
                            continue;
                        }

                        var entity = idx.Node(edge.ToId);
                        if (entity is null) continue;
                        var entityLabel = FirstNonEmpty(Str(entity, "name"), Str(entity, "fqdn"), entity?.Type) ?? "entity";
                        var link = SourceLink(edge, repoRoot);
                        Indent(w, indent);
                        w.WriteLine($"- [{kind} {entityLabel}]({link})");

                        var tableEdges = idx.Out(entity.Id, EdgeKinds.ReadsFrom).ToList();
                        if (tableEdges.Count > 0)
                        {
                            foreach (var tableEdge in tableEdges)
                            {
                                var tableNode = idx.Node(tableEdge.ToId);
                                var tableLabel = FirstNonEmpty(Str(tableNode, "table"), Str(tableNode, "name"), Str(tableNode, "fqdn"), tableNode?.Type) ?? "table";
                                var tableLink = SourceLink(tableEdge, repoRoot);
                                Indent(w, indent + 1);
                                w.WriteLine($"- [{kind} {tableLabel}]({tableLink})");
                            }
                        }
                        else
                        {
                            var tableName = FirstNonEmpty(Str(entity, "table"));
                            if (!string.IsNullOrWhiteSpace(tableName))
                            {
                                var tableLink = SourceLink(entity, repoRoot);
                                Indent(w, indent + 1);
                                w.WriteLine($"- [{kind} {tableName}]({tableLink})");
                            }
                        }
                    }
                }

                foreach (var use in idx.Out(fromId, EdgeKinds.UsesStorage))
                {
                    var storageNode = idx.Node(use.ToId);
                    var serviceType = FirstNonEmpty(Str(use, "service_type"), Str(storageNode, "service_type"), Str(storageNode, "name"), Str(storageNode, "fqdn"), storageNode?.Type) ?? "storage";
                    var link = SourceLink(use, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- uses_service {serviceType}");
                }

                foreach (var map in idx.Out(fromId, EdgeKinds.MapsTo))
                {
                    var dest = FirstNonEmpty(Str(map, "destination_type"), Str(idx.Node(map.ToId), "name"));
                    var link = SourceLink(map, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [maps_to {Short(dest)}]({link})");
                }

                foreach (var publish in idx.Out(fromId, EdgeKinds.Publishes))
                {
                    var messageType = FirstNonEmpty(Str(publish, "message_type"), Str(idx.Node(publish.ToId), "name"), Str(idx.Node(publish.ToId), "fqdn")) ?? "message";
                    var link = SourceLink(publish, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [publishes {Short(messageType)}]({link})");
                }

                foreach (var domainEvent in idx.Out(fromId, EdgeKinds.PublishesDomainEvent))
                {
                    var domainType = FirstNonEmpty(
                        Str(domainEvent, "event_type"),
                        Str(idx.Node(domainEvent.ToId), "name"),
                        Str(idx.Node(domainEvent.ToId), "fqdn")) ?? "domain_event";
                    var link = SourceLink(domainEvent, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [publishes_domain_event {Short(domainType)}]({link})");

                    RenderDomainEventHandlers(w, idx, domainEvent.ToId, indent + 1, repoRoot, serviceStack, serviceVisited);
                }

                foreach (var notif in idx.Out(fromId, EdgeKinds.PublishesNotification))
                {
                    var notificationType = FirstNonEmpty(Str(notif, "notification_type"), Str(idx.Node(notif.ToId), "name"), Str(idx.Node(notif.ToId), "fqdn")) ?? "notification";
                    var link = SourceLink(notif, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [publishes_notification {Short(notificationType)}]({link})");
                }

                foreach (var cache in idx.Out(fromId, EdgeKinds.UsesCache))
                {
                    var cacheLabel = FirstNonEmpty(Str(cache, "method"), Str(cache, "operation"), "cache");
                    var link = SourceLink(cache, repoRoot);
                    Indent(w, indent);
                    w.WriteLine($"- [uses_cache {cacheLabel}]({link})");
                }
            }
            finally
            {
                if (addedCurrent)
                {
                    serviceStack.Remove(fromId);
                }
            }
        }

        // Basic token-aware route matcher (brace tokens and catch-all segments)
        private static bool RoutesMatchWithTokens(string? a, string? b)
        {
            var aNorm = CanonicalizeRoute(a ?? string.Empty);
            var bNorm = CanonicalizeRoute(b ?? string.Empty);
            if (string.Equals(aNorm, bNorm, StringComparison.Ordinal)) return true;
            var aSegs = SplitAndTokenize(aNorm);
            var bSegs = SplitAndTokenize(bNorm);
            return MatchSegmentsWithWildcard(aSegs, bSegs);
        }

        private static bool HasLiteralSegments(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return false;
            }

            foreach (var ch in route)
            {
                if (char.IsLetter(ch))
                {
                    return true;
                }
            }

            return false;
        }

        private static string CanonicalizeRoute(string route)
        {
            if (string.IsNullOrWhiteSpace(route)) return string.Empty;
            var trimmed = route.Trim();
            if (trimmed.StartsWith("/", StringComparison.Ordinal)) trimmed = trimmed.TrimStart('/');
            if (trimmed.EndsWith("/", StringComparison.Ordinal)) trimmed = trimmed.TrimEnd('/');
            while (trimmed.Contains("//", StringComparison.Ordinal))
            {
                trimmed = trimmed.Replace("//", "/", StringComparison.Ordinal);
            }
            return trimmed.ToLowerInvariant();
        }

        private static string[] SplitAndTokenize(string route)
        {
            if (string.IsNullOrWhiteSpace(route)) return Array.Empty<string>();
            var segs = route.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < segs.Length; i++)
            {
                var s = segs[i];
                if (s.Length > 1 && s[0] == '{' && s[^1] == '}') segs[i] = "{}";
                else if (s.Contains('*')) segs[i] = "*";
            }
            return segs;
        }

        private static bool MatchSegmentsWithWildcard(string[] a, string[] b)
        {
            int ia = 0, ib = 0;
            int starA = -1, starB = -1;
            while (ib < b.Length)
            {
                if (ia < a.Length && (a[ia] == b[ib] || a[ia] == "{}" || b[ib] == "{}")) { ia++; ib++; continue; }
                if (ia < a.Length && a[ia] == "*") { starA = ia; starB = ib; ia++; continue; }
                if (starA != -1) { ia = starA + 1; ib = ++starB; continue; }
                return false;
            }
            while (ia < a.Length && a[ia] == "*") ia++;
            return ia == a.Length;
        }

        private static void PrintServiceSurface(
            StringWriter w,
            Index idx,
            string fromId,
            int indent,
            string repoRoot)
        {
            var emitted = false;
            foreach (var call in idx.Out(fromId, EdgeKinds.Calls))
            {
                var target = idx.Node(call.ToId);
                var targetLabel = FirstNonEmpty(Str(target, "name"), Str(target, "fqdn"), target?.Type) ?? "target";
                var method = FirstNonEmpty(Str(call, "method"));
                var operation = FirstNonEmpty(Str(call, "operation"));
                var label = targetLabel;
                if (!string.IsNullOrWhiteSpace(method))
                {
                    label = $"{targetLabel}.{method}";
                }
                if (!string.IsNullOrWhiteSpace(operation))
                {
                    label = $"{operation} {label}";
                }
                var link = SourceLink(call, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [calls {label}]({link})");
                emitted = true;
            }

            foreach (var read in idx.Out(fromId, EdgeKinds.Queries))
            {
                var entity = idx.Node(read.ToId);
                var entityLabel = FirstNonEmpty(Str(entity, "name"), Str(entity, "fqdn"), entity?.Type) ?? "entity";
                var operation = FirstNonEmpty(Str(read, "operation"));
                var link = SourceLink(read, repoRoot);
                Indent(w, indent);
                var label = string.IsNullOrWhiteSpace(operation)
                    ? entityLabel
                    : $"{operation} {entityLabel}";
                w.WriteLine($"- [{label}]({link})");
                emitted = true;

                foreach (var tableEdge in idx.Out(read.ToId, EdgeKinds.ReadsFrom))
                {
                    var tableNode = idx.Node(tableEdge.ToId);
                    var tableLabel = FirstNonEmpty(Str(tableNode, "table"), Str(tableNode, "name"), Str(tableNode, "fqdn"), tableNode?.Type) ?? "table";
                    var tableLink = SourceLink(tableEdge, repoRoot);
                    Indent(w, indent + 1);
                    w.WriteLine($"- [reads_from {tableLabel}]({tableLink})");
                }
            }

            foreach (var use in idx.Out(fromId, EdgeKinds.UsesStorage))
            {
                var storageNode = idx.Node(use.ToId);
                var serviceType = FirstNonEmpty(Str(use, "service_type"), Str(storageNode, "service_type"), Str(storageNode, "name"), Str(storageNode, "fqdn"), storageNode?.Type) ?? "storage";
                var link = SourceLink(use, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [uses_service {serviceType}]({link})");
                emitted = true;
            }

            foreach (var map in idx.Out(fromId, EdgeKinds.MapsTo))
            {
                var dest = FirstNonEmpty(Str(map, "destination_type"), Str(idx.Node(map.ToId), "name"));
                var link = SourceLink(map, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [maps_to {Short(dest)}]({link})");
                emitted = true;
            }

            foreach (var publish in idx.Out(fromId, EdgeKinds.Publishes))
            {
                var messageType = FirstNonEmpty(Str(publish, "message_type"), Str(idx.Node(publish.ToId), "name"), Str(idx.Node(publish.ToId), "fqdn")) ?? "message";
                var link = SourceLink(publish, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [publishes {Short(messageType)}]({link})");
                emitted = true;
            }

            foreach (var domainEvent in idx.Out(fromId, EdgeKinds.PublishesDomainEvent))
            {
                var domainType = FirstNonEmpty(
                    Str(domainEvent, "event_type"),
                    Str(idx.Node(domainEvent.ToId), "name"),
                    Str(idx.Node(domainEvent.ToId), "fqdn")) ?? "domain_event";
                var link = SourceLink(domainEvent, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [publishes_domain_event {Short(domainType)}]({link})");
                emitted = true;
            }

            foreach (var notif in idx.Out(fromId, EdgeKinds.PublishesNotification))
            {
                var notificationType = FirstNonEmpty(Str(notif, "notification_type"), Str(idx.Node(notif.ToId), "name"), Str(idx.Node(notif.ToId), "fqdn")) ?? "notification";
                var link = SourceLink(notif, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [publishes_notification {Short(notificationType)}]({link})");
                emitted = true;
            }

            foreach (var cache in idx.Out(fromId, EdgeKinds.UsesCache))
            {
                var cacheLabel = FirstNonEmpty(Str(cache, "method"), Str(cache, "operation"), "cache");
                var link = SourceLink(cache, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [uses_cache {cacheLabel}]({link})");
                emitted = true;
            }

            // If nothing emitted, we omit any marker to keep
            // narrative output focused on actual edges.
        }

        private static void ExpandServiceUsage(
            StringWriter w,
            Index idx,
            NodeFact? serviceNode,
            int indent,
            string repoRoot,
            HashSet<string> serviceStack,
            HashSet<(string From, string To)> visitedEdges)
        {
            if (serviceNode is null)
            {
                return;
            }

            var targetNode = serviceNode;
            if (string.Equals(serviceNode.Type, "app.service_contract", StringComparison.OrdinalIgnoreCase))
            {
                var implementation = idx.ServiceImplementationForContract(serviceNode);
                if (implementation is not null)
                {
                    targetNode = implementation;
                }
            }

            var implementations = idx.Out(targetNode.Id, "implemented_by").ToList();
            foreach (var implementationEdge in implementations)
            {
                var implementationNode = idx.Node(implementationEdge.ToId);
                var implementationLabel = FirstNonEmpty(
                    Str(implementationEdge, "implementation_type"),
                    Str(implementationNode, "fqdn"),
                    Str(implementationNode, "name"),
                    implementationNode?.Type) ?? "implementation";
                var implementationLink = SourceLink(implementationEdge, repoRoot);
                // Suppress noisy implementation entries that have no meaningful surface
                if (implementationNode is not null && HasServiceEdges(idx, implementationNode.Id))
                {
                    Indent(w, indent);
                    w.WriteLine($"- [implementation {implementationLabel}]({implementationLink})");
                    var implStack = new HashSet<string>(serviceStack, StringComparer.OrdinalIgnoreCase);
                    var implVisited = new HashSet<(string From, string To)>(visitedEdges);
                    RenderServiceDetails(w, idx, implementationNode, indent + 1, repoRoot, implVisited, implStack);
                }
            }

            if (implementations.Count == 0)
            {
                var branchStack = new HashSet<string>(serviceStack, StringComparer.OrdinalIgnoreCase);
                var branchVisited = new HashSet<(string From, string To)>(visitedEdges);

                if (targetNode is not null && serviceNode is not null &&
                    string.Equals(targetNode.Id, serviceNode.Id, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                RenderServiceDetails(w, idx, targetNode, indent, repoRoot, branchVisited, branchStack);
                // Only show the "no additional ..." marker when the service node truly has no content
                if (targetNode is not null && !HasServiceEdges(idx, targetNode.Id))
                {
                    // Avoid adding noise when the entry already included other signal (kept conservative)
                }
            }
        }

        private static void RenderDomainEventHandlers(
            StringWriter w,
            Index idx,
            string domainEventId,
            int indent,
            string repoRoot,
            HashSet<string> serviceStack,
            HashSet<(string From, string To)> visitedEdges)
        {
            foreach (var handlerEdge in idx.Out(domainEventId, EdgeKinds.HandledBy))
            {
                var key = (domainEventId, handlerEdge.ToId);
                if (!visitedEdges.Add(key))
                {
                    continue;
                }

                var handlerNode = idx.Node(handlerEdge.ToId);
                if (handlerNode is null)
                {
                    continue;
                }

                var handlerLabel = FirstNonEmpty(
                    Str(handlerEdge, "handler"),
                    Str(handlerNode, "name"),
                    Str(handlerNode, "fqdn"),
                    handlerNode.Type) ?? "handler";

                var link = SourceLink(handlerEdge, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [handled_by {handlerLabel}]({link})");

                var handlerStack = new HashSet<string>(serviceStack, StringComparer.OrdinalIgnoreCase);
                var handlerVisited = new HashSet<(string From, string To)>(visitedEdges);
                RenderServiceDetails(w, idx, handlerNode, indent + 1, repoRoot, handlerVisited, handlerStack);
                PrintHttpFromHandler(w, idx, handlerNode.Id, indent + 1, repoRoot);
                PrintSendsRequestTree(
                    w,
                    idx,
                    handlerNode.Id,
                    indent + 1,
                    repoRoot,
                    new HashSet<string>(StringComparer.OrdinalIgnoreCase));
            }
        }

        private static bool IsServiceLike(string? nodeType)
        {
            return string.Equals(nodeType, "app.service", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(nodeType, "app.service_contract", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(nodeType, "cqrs.handler", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(nodeType, "cqrs.notification_handler", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(nodeType, "domain.event_handler", StringComparison.OrdinalIgnoreCase);
        }

        private static bool HasServiceEdges(Index idx, string nodeId)
        {
            return idx.Out(nodeId, EdgeKinds.Calls).Any() ||
                   idx.Out(nodeId, EdgeKinds.UsesService).Any() ||
                   idx.Out(nodeId, EdgeKinds.UsesClient).Any() ||
                   idx.Out(nodeId, EdgeKinds.Queries).Any() ||
                   idx.Out(nodeId, EdgeKinds.UsesStorage).Any() ||
                   idx.Out(nodeId, EdgeKinds.MapsTo).Any() ||
                   idx.Out(nodeId, EdgeKinds.Publishes).Any() ||
                   idx.Out(nodeId, EdgeKinds.PublishesDomainEvent).Any() ||
                   idx.Out(nodeId, EdgeKinds.PublishesNotification).Any() ||
                   idx.Out(nodeId, EdgeKinds.UsesCache).Any();
        }

        private static int ParseLineNumber(string? value)
            => int.TryParse(value, out var line) ? line : -1;

        private static bool IsRequestProcessorService(string? serviceLabel)
            => !string.IsNullOrWhiteSpace(serviceLabel) &&
               serviceLabel.IndexOf("RequestProcessor", StringComparison.OrdinalIgnoreCase) >= 0;

        private static void Indent(StringWriter w, int n) => w.Write(new string(' ', n * 2));
        private static string Str(NodeFact? n, string k) => n is null ? "" : Str(n.Props, k);
        private static string Str(EdgeFact e, string k) => Str(e.Props, k);
        private static string Str(IReadOnlyDictionary<string, object?> p, string k)
            => p.TryGetValue(k, out var v) && v != null ? v.ToString()! : "";

        private static string? FirstNonEmpty(params string?[] values)
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }

        private static string Short(string? s) => string.IsNullOrEmpty(s) ? "" : s!.Split('.').Last();

        private static string ControllerActionName(NodeFact? ep)
        {
            var disp = Str(ep, "controller_display");
            return string.IsNullOrEmpty(disp) ? (ep?.Type ?? "endpoint") : disp;
        }

        private static string SourceLink(NodeFact n, string repoRoot)
            => SourceLink(n.Props, repoRoot);
        private static string SourceLink(EdgeFact e, string repoRoot)
            => SourceLink(e.Props, repoRoot);

        private static bool ShouldSummarizeInfrastructure(NodeFact? serviceNode)
        {
            if (serviceNode is null)
            {
                return false;
            }

            if (!serviceNode.Props.TryGetValue("tags", out var raw) || raw is null)
            {
                return false;
            }

            IEnumerable<string> tags = raw switch
            {
                string s => s.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                IEnumerable<object?> seq => seq.Select(o => o?.ToString() ?? string.Empty),
                _ => new[] { raw.ToString() ?? string.Empty }
            };

            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                {
                    continue;
                }

                if (string.Equals(tag, "infra", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(tag, "cache", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(tag, "logging", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string SourceLink(IReadOnlyDictionary<string, object?> props, string repoRoot)
        {
            var file = FirstNonEmpty(Str(props, "file"), Str(props, "file_path"));
            if (string.IsNullOrWhiteSpace(file))
            {
                return "#";
            }

            string? start = FirstNonEmpty(Str(props, "start_line"));
            string? end = FirstNonEmpty(Str(props, "end_line"));

            if ((string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end)) && props.TryGetValue("span", out var spanObj))
            {
                if (spanObj is IReadOnlyDictionary<string, object?> spanDictRo)
                {
                    start = start ?? Str(spanDictRo, "start_line");
                    end = end ?? Str(spanDictRo, "end_line");
                }
                else if (spanObj is IDictionary<string, object?> spanDict)
                {
                    if (string.IsNullOrWhiteSpace(start) && spanDict.TryGetValue("start_line", out var sv) && sv is not null)
                    {
                        start = sv.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(end) && spanDict.TryGetValue("end_line", out var ev) && ev is not null)
                    {
                        end = ev.ToString();
                    }
                }
            }

            var rel = MakeRelative(file!, repoRoot);
            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            {
                return $"./{rel}";
            }

            return $"./{rel}#L{start}-L{end}";
        }

        private static string MakeRelative(string absolutePath, string repoRoot)
        {
            try
            {
                var root = Path.GetFullPath(repoRoot).Replace('\\', '/');
                var candidate = Path.IsPathRooted(absolutePath)
                    ? absolutePath
                    : Path.Combine(repoRoot, absolutePath);
                var abs = Path.GetFullPath(candidate).Replace('\\', '/');
                return abs.StartsWith(root, StringComparison.OrdinalIgnoreCase)
                    ? abs.Substring(root.Length).TrimStart('/')
                    : abs;
            }
            catch
            {
                return absolutePath;
            }
        }

        private sealed class Index
        {
            private readonly Dictionary<string, NodeFact> _nodes;
            private readonly ILookup<string, EdgeFact> _out;
            private readonly Dictionary<string, List<string>> _pipelineBehaviors;
            private static readonly IReadOnlyList<string> EmptyBehaviors = Array.Empty<string>();

            public Index(FactBag bag)
            {
                _nodes = bag.Nodes.ToDictionary(n => n.Id);
                _out = bag.Edges.ToLookup(e => e.FromId);
                _pipelineBehaviors = BuildPipelineBehaviorIndex(bag.Edges);
            }

            public NodeFact? Node(string id) => _nodes.TryGetValue(id, out var n) ? n : null;
            public IEnumerable<EdgeFact> Out(string id, string kind) => _out[id].Where(e => e.Kind == kind);

            public IEnumerable<NodeFact> EndpointNodes()
                => _nodes.Values.Where(n => n.Type == NodeTypes.EndpointController || n.Type == NodeTypes.EndpointMinimalApi);

            public NodeFact? ServiceImplementationForContract(NodeFact contractNode)
            {
                var contractFqdn = Str(contractNode, "fqdn");
                if (string.IsNullOrWhiteSpace(contractFqdn))
                {
                    return null;
                }

                return _nodes.Values.FirstOrDefault(n =>
                    string.Equals(n.Type, "app.service", StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(Str(n, "fqdn"), contractFqdn, StringComparison.OrdinalIgnoreCase));
            }

            public (EdgeFact Edge, NodeFact HandlerNode, string HandlerDisplay)? FindHandlerForRequest(string requestNodeId)
            {
                (EdgeFact Edge, NodeFact HandlerNode, string HandlerDisplay)? fallback = null;
                foreach (var edge in _out.SelectMany(g => g).Where(e => e.Kind == EdgeKinds.HandledBy))
                {
                    if (edge.FromId != requestNodeId)
                    {
                        continue;
                    }

                    var handler = Node(edge.ToId);
                    if (handler is null)
                    {
                        continue;
                    }

                    var display = FirstNonEmpty(
                        Str(handler, "handler"),
                        Str(handler, "name"),
                        Str(handler, "fqdn"),
                        handler.Type) ?? handler.Type;

                    var candidate = (edge, handler, display);
                    if (HasServiceEdges(this, handler.Id))
                    {
                        return candidate;
                    }

                    fallback ??= candidate;
                }

                return fallback;
            }

            public IReadOnlyList<string> PipelineBehaviorsForRequest(string requestNodeId)
            {
                return _pipelineBehaviors.TryGetValue(requestNodeId, out var list)
                    ? list
                    : EmptyBehaviors;
            }

            private static Dictionary<string, List<string>> BuildPipelineBehaviorIndex(IEnumerable<EdgeFact> edges)
            {
                var result = new Dictionary<string, List<string>>(StringComparer.Ordinal);
                foreach (var edge in edges)
                {
                    if (!string.Equals(edge.Kind, EdgeKinds.SendsRequest, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    if (!edge.Props.TryGetValue("pipeline_behaviors", out var raw) || raw is null)
                    {
                        continue;
                    }

                    foreach (var label in ExtractPipelineLabels(raw))
                    {
                        if (string.IsNullOrWhiteSpace(label))
                        {
                            continue;
                        }

                        if (!result.TryGetValue(edge.ToId, out var list))
                        {
                            list = new List<string>();
                            result[edge.ToId] = list;
                        }

                        if (!list.Any(existing => string.Equals(existing, label, StringComparison.OrdinalIgnoreCase)))
                        {
                            list.Add(label);
                        }
                    }
                }

                return result;
            }

            private static IEnumerable<string> ExtractPipelineLabels(object? value)
            {
                switch (value)
                {
                    case null:
                        yield break;
                    case string s:
                        foreach (var item in SplitPipelineString(s))
                        {
                            yield return item;
                        }
                        yield break;
                    case JsonElement element:
                        switch (element.ValueKind)
                        {
                            case JsonValueKind.Array:
                                foreach (var child in element.EnumerateArray())
                                {
                                    foreach (var label in ExtractPipelineLabels(child))
                                    {
                                        yield return label;
                                    }
                                }
                                yield break;
                            case JsonValueKind.String:
                                foreach (var item in SplitPipelineString(element.GetString()))
                                {
                                    yield return item;
                                }
                                yield break;
                            default:
                                var text = element.ToString();
                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    foreach (var item in SplitPipelineString(text))
                                    {
                                        yield return item;
                                    }
                                }
                                yield break;
                        }
                    case IEnumerable enumerable when value is not string:
                        foreach (var item in enumerable)
                        {
                            foreach (var label in ExtractPipelineLabels(item))
                            {
                                yield return label;
                            }
                        }
                        yield break;
                    default:
                        foreach (var item in SplitPipelineString(value.ToString()))
                        {
                            yield return item;
                        }
                        yield break;
                }
            }

            private static IEnumerable<string> SplitPipelineString(string? raw)
            {
                if (string.IsNullOrWhiteSpace(raw))
                {
                    yield break;
                }

                foreach (var part in raw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        yield return part;
                    }
                }
            }
        }
    }
}
