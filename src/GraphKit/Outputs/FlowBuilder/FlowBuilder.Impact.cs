using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GraphKit.Graph;

using static GraphKit.Outputs.Utilities;

namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    public sealed class ImpactAccumulator
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
}
