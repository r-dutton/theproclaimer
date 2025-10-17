using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GraphKit.Graph;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    private sealed class ImpactAccumulator
    {
        public ImpactAccumulator(string callerRoot)
        {
            CallerRoot = callerRoot ?? string.Empty;
        }

        public string CallerRoot { get; }
        internal Dictionary<string, RepositoryImpact> Repositories { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal Dictionary<string, EntityImpact> Entities { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal Dictionary<string, RemoteEndpointImpact> RemoteEndpoints { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> RemoteServices { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal Dictionary<string, int> RemoteScopes { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal int RemoteCallCount { get; private set; }
        internal int MissingRemoteRoutes { get; private set; }
        internal HashSet<string> Services { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Clients { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> PipelineBehaviors { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal int GenericPipelineBehaviors { get; private set; }
        internal HashSet<string> Requests { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Handlers { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Notifications { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Messages { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Caches { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Options { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Validators { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Storages { get; } = new(StringComparer.OrdinalIgnoreCase);
        internal HashSet<string> Mappings { get; } = new(StringComparer.OrdinalIgnoreCase);

        public bool IsEmpty =>
            RemoteCallCount == 0 &&
            Repositories.Count == 0 &&
            Entities.Count == 0 &&
            PipelineBehaviors.Count == 0 &&
            GenericPipelineBehaviors == 0 &&
            Services.Count == 0 &&
            Clients.Count == 0 &&
            Notifications.Count == 0 &&
            Messages.Count == 0 &&
            Caches.Count == 0 &&
            Options.Count == 0 &&
            Validators.Count == 0 &&
            Requests.Count == 0 &&
            Handlers.Count == 0 &&
            Storages.Count == 0 &&
            Mappings.Count == 0;

        public void RecordRemoteCall(string clientName, string? verb, string? route, string? baseUrl, string? targetService)
        {
            var normalizedClient = string.IsNullOrWhiteSpace(clientName) ? "client" : clientName;
            RecordClient(normalizedClient);

            var canonicalRoute = CanonicalizeRoute(route);
            if (canonicalRoute is null)
            {
                MissingRemoteRoutes++;
                canonicalRoute = "(missing route)";
            }

            var host = ExtractHost(baseUrl, route);
            var label = !string.IsNullOrWhiteSpace(targetService)
                ? targetService!.Trim()
                : !string.IsNullOrWhiteSpace(host)
                    ? host!
                    : normalizedClient;
            RemoteServices.Add(label);

            var verbNormalized = string.IsNullOrWhiteSpace(verb) ? "GET" : verb!.Trim().ToUpperInvariant();
            var key = $"{label}::{verbNormalized}::{canonicalRoute}";
            if (!RemoteEndpoints.TryGetValue(key, out var remote))
            {
                var scope = DetermineRemoteScope(host, label, CallerRoot);
                remote = new RemoteEndpointImpact(normalizedClient, verbNormalized, canonicalRoute, label, scope, host);
                RemoteEndpoints[key] = remote;
            }

            remote.Increment(IsMutationVerb(verbNormalized));
            RemoteCallCount++;
            RemoteScopes.TryGetValue(remote.Scope, out var count);
            RemoteScopes[remote.Scope] = count + 1;
        }

        public void RecordRepositoryOperation(string repositoryName, string? operationKind, string? entityName)
        {
            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                return;
            }

            if (!Repositories.TryGetValue(repositoryName, out var repo))
            {
                repo = new RepositoryImpact(repositoryName);
                Repositories[repositoryName] = repo;
            }

            repo.RecordOperation(operationKind);

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                repo.Entities.Add(entityName!);
                RecordEntityOperation(entityName!, operationKind);
            }
        }

        public void RecordEntityOperation(string entityName, string? operationKind)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return;
            }

            if (!Entities.TryGetValue(entityName, out var entity))
            {
                entity = new EntityImpact(entityName);
                Entities[entityName] = entity;
            }

            entity.RecordOperation(operationKind);
        }

        public void RecordServiceUsage(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return;
            }

            Services.Add(serviceName);
        }

        public void RecordPipelineBehavior(string behaviorName)
        {
            if (string.IsNullOrWhiteSpace(behaviorName))
            {
                return;
            }

            PipelineBehaviors.Add(behaviorName);
        }

        public void RecordClient(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
            {
                return;
            }

            Clients.Add(clientName);
        }

        public void RecordGenericPipelineBehaviors(int count)
        {
            if (count > GenericPipelineBehaviors)
            {
                GenericPipelineBehaviors = count;
            }
        }

        public void RecordRequest(string requestName)
        {
            if (string.IsNullOrWhiteSpace(requestName))
            {
                return;
            }

            Requests.Add(requestName);
        }

        public void RecordHandler(string handlerName)
        {
            if (string.IsNullOrWhiteSpace(handlerName))
            {
                return;
            }

            Handlers.Add(handlerName);
        }

        public void RecordNotification(string notificationName)
        {
            if (string.IsNullOrWhiteSpace(notificationName))
            {
                return;
            }

            Notifications.Add(notificationName);
        }

        public void RecordMessage(string messageName)
        {
            if (string.IsNullOrWhiteSpace(messageName))
            {
                return;
            }

            Messages.Add(messageName);
        }

        public void RecordCache(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName))
            {
                return;
            }

            Caches.Add(cacheName);
        }

        public void RecordOption(string optionName)
        {
            if (string.IsNullOrWhiteSpace(optionName))
            {
                return;
            }

            Options.Add(optionName);
        }

        public void RecordValidator(string validatorName)
        {
            if (string.IsNullOrWhiteSpace(validatorName))
            {
                return;
            }

            Validators.Add(validatorName);
        }

        public void RecordStorage(string storageName)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                return;
            }

            Storages.Add(storageName);
        }

        public void RecordMapping(string mappingName)
        {
            if (string.IsNullOrWhiteSpace(mappingName))
            {
                return;
            }

            Mappings.Add(mappingName);
        }

        internal sealed class RepositoryImpact
        {
            public RepositoryImpact(string name)
            {
                Name = name;
            }

            public string Name { get; }
            public int WriteCount { get; private set; }
            public int ReadCount { get; private set; }
            public HashSet<string> Entities { get; } = new(StringComparer.OrdinalIgnoreCase);
            public int TotalOperations => WriteCount + ReadCount;

            public void RecordOperation(string? operationKind)
            {
                if (IsWriteOperationKind(operationKind))
                {
                    WriteCount++;
                }
                else if (IsReadOperationKind(operationKind))
                {
                    ReadCount++;
                }
                else
                {
                    ReadCount++;
                }
            }
        }

        internal sealed class EntityImpact
        {
            public EntityImpact(string name)
            {
                Name = name;
            }

            public string Name { get; }
            public int WriteCount { get; private set; }
            public int ReadCount { get; private set; }
            public int TotalOperations => WriteCount + ReadCount;

            public void RecordOperation(string? operationKind)
            {
                if (IsWriteOperationKind(operationKind))
                {
                    WriteCount++;
                }
                else if (IsReadOperationKind(operationKind))
                {
                    ReadCount++;
                }
                else
                {
                    ReadCount++;
                }
            }
        }

        internal sealed class RemoteEndpointImpact
        {
            public RemoteEndpointImpact(string client, string verb, string route, string label, string scope, string? host)
            {
                Client = client;
                Verb = verb;
                Route = route;
                Label = label;
                Scope = scope;
                Host = host;
            }

            public string Client { get; }
            public string Verb { get; }
            public string Route { get; }
            public string Label { get; }
            public string Scope { get; }
            public string? Host { get; }
            public int Count { get; private set; }
            public HashSet<string> CallKinds { get; } = new(StringComparer.OrdinalIgnoreCase);

            public void Increment(bool isMutation)
            {
                Count++;
                CallKinds.Add(isMutation ? "command" : "query");
            }
        }
    }

    private static string BuildAuthorizationAnnotation(GraphNode endpoint)
    {
        if (endpoint.Props is not { } props)
        {
            return string.Empty;
        }

        var parts = new List<string>();
        if (props.TryGetValue("authorization", out var authObj) && authObj is IEnumerable<object> authList)
        {
            var policies = new List<string>();
            foreach (var a in authList)
            {
                if (a is IDictionary<string, object> dict && dict.TryGetValue("policy", out var policyVal))
                {
                    var policy = policyVal?.ToString();
                    if (!string.IsNullOrWhiteSpace(policy))
                    {
                        policies.Add(policy!);
                    }
                }
            }
            if (policies.Count > 0)
            {
                parts.Add($"auth={string.Join(',', policies)}");
            }
        }

        if (props.TryGetValue("allow_anonymous", out var anonObj) && anonObj is bool anon && anon)
        {
            parts.Add("AllowAnonymous");
        }

        return parts.Count > 0 ? " [" + string.Join(' ', parts) + "]" : string.Empty;
    }

    private static string? GetNodeProp(GraphNode node, string key)
        => node.Props is { } props && props.TryGetValue(key, out var value) ? value?.ToString() : null;

    private static string GetSimpleType(string? type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return string.Empty;
        }

        var simple = type.Replace("?", string.Empty, StringComparison.Ordinal);
        var genericIndex = simple.IndexOf('<');
        if (genericIndex >= 0)
        {
            simple = simple[..genericIndex];
        }

        var lastDot = simple.LastIndexOf('.');
        if (lastDot >= 0)
        {
            return simple[(lastDot + 1)..];
        }

        return simple;
    }

    private static void AppendIndented(StringBuilder builder, int indent, string text)
    {
        builder.Append(' ', indent * 2);
        builder.AppendLine($"└─ {text}");
    }

    private static void AppendNotificationFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode notification,
        int indent)
    {
        if (!state.NotificationStack.Add(notification.Id))
        {
            return;
        }

        try
        {
            if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
            {
                AppendIndented(builder, indent, "... (max depth reached)");
                return;
            }

            if (!state.EdgesByFrom.TryGetValue(notification.Id, out var edges))
            {
                return;
            }

            foreach (var handlerEdge in edges.Where(e => e.Kind == "handled_by"))
            {
                if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode))
                {
                    continue;
                }

                var span = handlerNode.Span;
                AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
                state.CurrentImpact?.RecordHandler(GetDisplayName(handlerNode));
                AppendNotificationHandlerFlow(builder, state, handlerNode, indent + 1);
            }
        }
        finally
        {
            state.NotificationStack.Remove(notification.Id);
        }
    }

    private static void AppendNotificationHandlerFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode handler,
        int indent)
    {
        if (!state.HandlerStack.Add(handler.Id))
        {
            return;
        }

        try
        {
            if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
            {
                AppendIndented(builder, indent, "... (max depth reached)");
                return;
            }

            if (!state.EdgesByFrom.TryGetValue(handler.Id, out var edges))
            {
                return;
            }

            // Group repository calls in notification handler
            var notifCallEdges = edges.Where(e => e.Kind == "calls").ToList();
            for (int i = 0; i < notifCallEdges.Count; i++)
            {
                var call = notifCallEdges[i];
                if (!state.NodesById.TryGetValue(call.To, out var target)) continue;
                var isRepo = target.Type == "app.repository" || target.Type == "repository";
                if (!isRepo)
                {
                    var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                    continue;
                }
                var methods = new List<string>();
                int? firstLine = call.Transform?.Location?.Line;
                int j = i;
                while (j < notifCallEdges.Count)
                {
                    var ej = notifCallEdges[j];
                    if (ej.To != call.To) break;
                    var m = ej.Props is { } p && p.TryGetValue("method", out var mv) ? mv?.ToString() : null;
                    if (!string.IsNullOrWhiteSpace(m)) methods.Add(m!);
                    if (!firstLine.HasValue && ej.Transform?.Location?.Line is int ln) firstLine = ln;
                    j++;
                }
                i = j - 1;
                var uniqueMethods = methods.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                if (uniqueMethods.Count <= 1)
                {
                    var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
                    var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
                    var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
                }
                else
                {
                    var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                    var lineTextGroup = firstLine.HasValue ? $" [L{firstLine}]" : string.Empty;
                    AppendIndented(builder, indent, $"calls {target.Name}{methodsPart}{lineTextGroup}");
                }
                AppendRepositoryFlow(builder, state, target, indent + 1);
            }

            foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
            {
                AppendMappingEdge(builder, state, mapping, indent);
            }

            foreach (var service in edges.Where(e => e.Kind == "uses_service"))
            {
                if (!state.NodesById.TryGetValue(service.To, out var serviceNode))
                {
                    continue;
                }

                if (IsInfrastructureNoiseService(serviceNode))
                {
                    continue;
                }

                var lineText = service.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                    ? lifetimeValue?.ToString()
                    : null;
                var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                var serviceMethodName = service.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                    ? methodValue?.ToString()
                    : null;
                var serviceLineText = string.IsNullOrWhiteSpace(serviceMethodName) ? lineText : string.Empty;
                AppendIndented(builder, indent, $"uses_service {serviceNode.Name}{suffix}{serviceLineText}");

                var nextIndent = indent + 1;
                if (!string.IsNullOrWhiteSpace(serviceMethodName))
                {
                    AppendIndented(builder, indent + 1, $"method {serviceMethodName}{lineText}");
                    nextIndent = indent + 2;
                }

                AppendServiceContractFlow(builder, state, handler, serviceNode, serviceMethodName, nextIndent);
            }

            foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
            {
                if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode))
                {
                    continue;
                }

                var lineText = requestEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"sends_request {requestNode.Name}{lineText}");
                AppendCommandFlow(builder, state, requestNode, indent + 1);
            }

            foreach (var publish in edges.Where(e => e.Kind == "publishes_notification"))
            {
                if (!state.NodesById.TryGetValue(publish.To, out var notificationNode))
                {
                    continue;
                }

                var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
                AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
                AppendNotificationFlow(builder, state, notificationNode, indent + 1);
            }
        }
        finally
        {
            state.HandlerStack.Remove(handler.Id);
        }
    }

    private static void AppendPublisherFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode publisher,
        int indent)
    {
        if (state.MaxDepth.HasValue && indent >= state.MaxDepth.Value)
        {
            AppendIndented(builder, indent, "... (max depth reached)");
            return;
        }
        if (!state.EdgesByFrom.TryGetValue(publisher.Id, out var edges))
        {
            return;
        }

        foreach (var produced in edges.Where(e => e.Kind == "produces_event"))
        {
            if (!state.NodesById.TryGetValue(produced.To, out var contract))
            {
                continue;
            }

            var lineText = produced.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            AppendIndented(builder, indent, $"produces_event {contract.Name}{lineText}");
        }
    }

    private static string BuildPublisherDetails(GraphNode publisher)
    {
        if (publisher.Props is not { Count: > 0 })
        {
            return string.Empty;
        }

        var details = new List<string>();

        if (publisher.Props.TryGetValue("queue", out var queueValue) &&
            queueValue is string { Length: > 0 } queue)
        {
            details.Add($"queue={queue}");
        }

        if (publisher.Props.TryGetValue("subject", out var subjectValue) &&
            subjectValue is string { Length: > 0 } subject)
        {
            details.Add($"subject={subject}");
        }

        return details.Count > 0
            ? $" ({string.Join(", ", details)})"
            : string.Empty;
    }

    private static string ExtractOperationLabel(GraphEdge edge)
    {
        if (edge.Props is { } props && props.TryGetValue("operation", out var value) && value is not null)
        {
            var text = value.ToString();
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text!;
            }
        }

        return edge.Kind switch
        {
            "inserts_into" => "insert",
            "updates" => "update",
            "deletes_from" => "delete",
            "upserts" => "upsert",
            "writes_to" => "writes_to",
            "queries" => "queries",
            "reads_from" => "reads_from",
            _ => edge.Kind
        };
    }
}
