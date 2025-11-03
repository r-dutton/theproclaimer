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
                PrintEfTouches(w, idx, ep.Id, 1, repoRoot);
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
            foreach (var e in idx.Out(fromId, EdgeKinds.UsesService))
            {
                var targetNode = idx.Node(e.ToId);
                var serviceLabel = FirstNonEmpty(
                    Str(e, "service_type"),
                    Str(targetNode, "service_type"),
                    Str(targetNode, "name"),
                    Str(targetNode, "fqdn"),
                    targetNode?.Type) ?? "service";
                if (IsRequestProcessorService(serviceLabel))
                {
                    continue;
                }
                var link = SourceLink(e, repoRoot);
                Indent(w, indent);
                w.WriteLine($"- [uses_service {serviceLabel}]({link})");
                if (e.Props.TryGetValue("method", out var method) && method is string m && !string.IsNullOrWhiteSpace(m))
                {
                    Indent(w, indent + 1);
                    w.WriteLine($"- [method {m}]({link})");
                }
                var contract = Str(e, "invoked_method");
                if (!string.IsNullOrWhiteSpace(contract) &&
                    !string.Equals(contract, Str(e, "method"), StringComparison.OrdinalIgnoreCase))
                {
                    Indent(w, indent + 1);
                    w.WriteLine($"- [contract {contract}]({link})");
                }
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

            foreach (var e in idx.Out(fromId, EdgeKinds.SendsRequest))
            {
                var serviceName = FirstNonEmpty(Str(e, "service"));
                if (IsRequestProcessorService(serviceName))
                {
                    RenderRequestProcessorInvocation(w, idx, fromId, e, indent, repoRoot, visited, preferredAssembly, preferredProject);
                    continue;
                }

                var req = Str(e, "request_type");
                var resp = Str(e, "response_type");
                var link = SourceLink(e, repoRoot);

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

                var behaviors = idx.PipelineBehaviorsForRequest(e.ToId);
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

                RenderHandlerBlock(w, idx, e.ToId, indent + 1, repoRoot, visited, preferredAssembly, preferredProject);
            }

            visited.Remove(fromId);
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

            Indent(w, indent);
            w.WriteLine($"- [uses_service RequestProcessor]({serviceLink})");

            if (!string.IsNullOrWhiteSpace(methodName))
            {
                Indent(w, indent + 1);
                w.WriteLine($"- [method {methodName}]({serviceLink})");
            }

            if (!string.IsNullOrWhiteSpace(contractMethod) &&
                !string.Equals(contractMethod, methodName, StringComparison.OrdinalIgnoreCase))
            {
                Indent(w, indent + 1);
                w.WriteLine($"- [contract {contractMethod}]({serviceLink})");
            }

            var executionMethod = FirstNonEmpty(contractMethod, Str(dispatchEdge, "invocation"), methodName, "ProcessAsync")!;
            RenderRequestProcessorDispatch(w, idx, dispatchEdge, indent + 2, executionMethod, repoRoot, visited, preferredAssembly, preferredProject);
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
            var hlink = SourceLink(handlerEdge, repoRoot);
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

                foreach (var call in idx.Out(toNode!.Id, EdgeKinds.Calls))
                {
                    var ep = idx.Node(call.ToId);
                    var callMethod = FirstNonEmpty(Str(usesClient, PropKeys.ClientMethod), Str(call, "method"));
                    var clink = SourceLink(call, repoRoot);
                    var descriptionParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(verb)) descriptionParts.Add(verb);
                    if (!string.IsNullOrWhiteSpace(route)) descriptionParts.Add(route);
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
                    if (!string.IsNullOrEmpty(target))
                    {
                        Indent(w, indent + 2);
                        w.WriteLine($"- target_service {target}");
                        var link2 = SourceLink(ep!, repoRoot);
                        Indent(w, indent + 3);
                        var authValue = FirstNonEmpty(Str(ep!, "auth")) ?? "user";
                        w.WriteLine($"- [[web] {verb} {route}  ({ControllerActionName(ep)})]({link2}) status=200 [auth={authValue}]");
                    }
                }
            }
        }

        private static void PrintHttpCallsTree(StringWriter w, Index idx, string fromId, int indent, string repoRoot)
        {
            PrintHttpFromHandler(w, idx, fromId, indent, repoRoot);
        }

        private static void PrintEfTouches(StringWriter w, Index idx, string fromId, int indent, string repoRoot)
        {
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
            }

            foreach (var service in idx.Out(fromId, EdgeKinds.UsesService))
            {
                var serviceNode = idx.Node(service.ToId);
                var serviceLabel = FirstNonEmpty(
                    Str(service, "service_type"),
                    Str(serviceNode, "service_type"),
                    Str(serviceNode, "name"),
                    Str(serviceNode, "fqdn"),
                    serviceNode?.Type) ?? "service";
                if (IsRequestProcessorService(serviceLabel))
                {
                    continue;
                }
                var details = FirstNonEmpty(Str(service, "method"));
                var link = SourceLink(service, repoRoot);
                Indent(w, indent);
                w.WriteLine(string.IsNullOrWhiteSpace(details)
                    ? $"- uses_service {serviceLabel}"
                    : $"- uses_service {serviceLabel} (method={details})");
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
                var abs = Path.GetFullPath(absolutePath).Replace('\\', '/');
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

            public (EdgeFact Edge, NodeFact HandlerNode, string HandlerDisplay)? FindHandlerForRequest(string requestNodeId)
            {
                foreach (var edge in _out.SelectMany(g => g).Where(e => e.Kind == EdgeKinds.HandledBy))
                {
                    if (edge.FromId == requestNodeId)
                    {
                        var handler = Node(edge.ToId);
                        if (handler != null)
                        {
                            var display = FirstNonEmpty(
                                Str(handler, "handler"),
                                Str(handler, "name"),
                                Str(handler, "fqdn"),
                                handler.Type) ?? handler.Type;
                            return (edge, handler, display);
                        }
                    }
                }
                return null;
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
