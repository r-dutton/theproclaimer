using GraphKit.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphKit.Outputs
{
    public static partial class Utilities
    {
        public static string? GetNodeProp(GraphNode node, string key)
            => node.Props is { } props && props.TryGetValue(key, out var value) ? value?.ToString() : null;

        public static string GetSimpleType(string? type)
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

        public static bool IsRepositoryNode(GraphNode node)
        {
            var type = node.Type ?? string.Empty;
            if (type.Equals("app.repository", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("repository", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("data.repository", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("dataaccess", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("data.access", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("app.unit_of_work", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("infrastructure.repository", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("data.gateway", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (node.Tags.Any(tag => tag.Equals("repository", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("unit_of_work", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("data_access", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("data_access_layer", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            static bool ContainsRepositoryKeyword(string? value)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }

                return value.Contains("Repository", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("ControlledRepository", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("RepositoryBase", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("RepoBase", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("RepositoryImpl", StringComparison.OrdinalIgnoreCase) ||
                       value.EndsWith("Repo", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("DataAccess", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("DataStore", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("DataGateway", StringComparison.OrdinalIgnoreCase) ||
                       value.EndsWith("Gateway", StringComparison.OrdinalIgnoreCase) ||
                       (value.EndsWith("Dal", StringComparison.OrdinalIgnoreCase) && value.Length > 3) ||
                       value.Contains("UnitOfWork", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("Persistence", StringComparison.OrdinalIgnoreCase) ||
                       value.Contains("IRepository<", StringComparison.Ordinal) ||
                       value.Contains("Repository<", StringComparison.Ordinal);
            }

            var candidates = new[]
            {
                node.Name,
                node.Fqdn,
                GetSimpleType(node.Name),
                GetSimpleType(node.Fqdn)
            };

            return candidates.Any(ContainsRepositoryKeyword);
        }

        public static bool IsEntityNode(GraphNode node)
        {
            var type = node.Type ?? string.Empty;
            if (type.Equals("ef.entity", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("domain.entity", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("domain.aggregate", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("aggregate", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("entity", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (node.Tags.Any(tag => tag.Equals("entity", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("aggregate", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        public static bool IsLikelyEntity(GraphNode node)
        {
            if (IsEntityNode(node))
            {
                return true;
            }

            if (node.Tags.Any(tag => tag.Equals("dto", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("viewmodel", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("model", StringComparison.OrdinalIgnoreCase) ||
                                      tag.Equals("record", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            var name = node.Name ?? string.Empty;
            if (name.EndsWith("Dto", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("Record", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("Model", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("Aggregate", StringComparison.OrdinalIgnoreCase) ||
                name.EndsWith("Entity", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var fqdn = node.Fqdn ?? string.Empty;
            if (fqdn.Contains(".Dto", StringComparison.OrdinalIgnoreCase) ||
                fqdn.Contains(".ViewModel", StringComparison.OrdinalIgnoreCase) ||
                fqdn.Contains(".Model", StringComparison.OrdinalIgnoreCase) ||
                fqdn.Contains(".Aggregate", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static void AppendIndented(StringBuilder builder, int indent, string text)
        {
            builder.Append(' ', indent * 2);
            builder.AppendLine($"└─ {text}");
        }

        public static string? GetCallMethod(GraphEdge edge)
        {
            return edge.Props is { } props && props.TryGetValue("method", out var methodValue)
                ? methodValue?.ToString()
                : null;
        }

        public static bool IsInfrastructureNoiseService(GraphNode serviceNode)
        {
            var name = serviceNode.Name ?? string.Empty;
            var fqdn = serviceNode.Fqdn ?? string.Empty;

            if (name.StartsWith("ILogger<", StringComparison.Ordinal) || fqdn.StartsWith("Microsoft.Extensions.Logging.ILogger<", StringComparison.Ordinal))
                return true;
            if (string.Equals(name, "ILogger", StringComparison.Ordinal) || fqdn.Contains("Microsoft.Extensions.Logging.ILogger", StringComparison.Ordinal))
                return true;
            if (string.Equals(name, "IMapper", StringComparison.Ordinal) || fqdn.Contains("AutoMapper.IMapper", StringComparison.Ordinal))
                return true;
            // Additional pseudo / cross-cutting service suppressions (these typically do not add domain semantics in flow output)
            if (name is "ITimeProvider" or "IDateTimeProvider" or "IDateTime" or "IClock" || fqdn.Contains("TimeProvider", StringComparison.Ordinal))
                return true;
            if (name is "IGuidGenerator" or "ICorrelationIdAccessor" or "ICorrelationContextAccessor")
                return true;
            if (name is "ICurrentUserService" or "ICurrentUser" or "IUserContext" or "IUserAccessor")
                return true;
            if (name is "ITelemetryClient" or "ITelemetry" || fqdn.Contains("TelemetryClient", StringComparison.Ordinal))
                return true;
            if (name is "ITracer" || fqdn.Contains("Tracing", StringComparison.Ordinal))
                return true;
            if (name is "IDistributedCache" || fqdn.Contains("Microsoft.Extensions.Caching.Distributed.IDistributedCache", StringComparison.Ordinal))
                return true;
            if (string.Equals(name, "IHttpContextAccessor", StringComparison.Ordinal) || fqdn.Contains("Microsoft.AspNetCore.Http.IHttpContextAccessor", StringComparison.Ordinal))
                return true;
            return false;
        }


        public static bool EdgeMatchesMethod(string? invokedMethod, GraphEdge edge)
        {
            if (string.IsNullOrWhiteSpace(invokedMethod))
            {
                return true;
            }

            if (edge.Props is not { } props)
            {
                return false;
            }

            if (props.TryGetValue("method", out var methodValue) && methodValue is string methodName && !string.IsNullOrWhiteSpace(methodName))
            {
                return string.Equals(methodName.Trim(), invokedMethod.Trim(), StringComparison.OrdinalIgnoreCase);
            }

            if (props.TryGetValue("client_method", out var clientMethodValue) && clientMethodValue is string clientMethod && !string.IsNullOrWhiteSpace(clientMethod))
            {
                return string.Equals(clientMethod.Trim(), invokedMethod.Trim(), StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }



        public static HashSet<string> CollectReachable(string rootId, Dictionary<string, List<GraphEdge>> edgesByFrom)
        {
            var visited = new HashSet<string>(StringComparer.Ordinal) { rootId };
            var queue = new Queue<string>();
            queue.Enqueue(rootId);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!edgesByFrom.TryGetValue(current, out var list)) continue;
                foreach (var e in list)
                {
                    if (visited.Add(e.To)) queue.Enqueue(e.To);
                }
            }
            return visited;
        }

        public static HashSet<string> CollectInbound(string rootId, Dictionary<string, List<GraphEdge>> edgesByTo)
        {
            var visited = new HashSet<string>(StringComparer.Ordinal) { rootId };
            var queue = new Queue<string>();
            queue.Enqueue(rootId);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!edgesByTo.TryGetValue(current, out var incoming)) continue;
                foreach (var e in incoming)
                {
                    if (visited.Add(e.From)) queue.Enqueue(e.From);
                }
            }
            return visited;
        }

        public static string BuildCallDedupKey(GraphEdge edge, GraphNode target, string? method)
        {
            var methodKey = string.IsNullOrWhiteSpace(method) ? "*" : method.Trim();
            var location = edge.Transform?.Location;
            var lineKey = location?.Line.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "*";
            var fileKey = location?.File ?? string.Empty;
            var ilRange = edge.Transform?.IlRange;
            var ilStart = ilRange?.StartOffset.HasValue == true
                ? ilRange.StartOffset.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                : "*";
            var ilEnd = ilRange?.EndOffset.HasValue == true
                ? ilRange.EndOffset.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                : "*";
            return $"{edge.From}->{target.Id}::{edge.Source}::{methodKey}::{fileKey}::{lineKey}::{ilStart}-{ilEnd}";
        }


        public static void AppendCommandFlow(
            StringBuilder builder,
            FlowBuilder.FlowRenderState state,
            GraphNode command,
            int indent)
        {
            if (!state.EdgesByFrom.TryGetValue(command.Id, out var edges))
            {
                return;
            }

            state.CurrentImpact?.RecordRequest(GetDisplayName(command));

            // Pipeline behaviors / request processors (processed_by edges)
            foreach (var pipelineEdge in edges.Where(e => e.Kind == "processed_by"))
            {
                if (!state.NodesById.TryGetValue(pipelineEdge.To, out var behaviorNode))
                {
                    continue;
                }
                if (state.AllowedIds != null && !state.AllowedIds.Contains(behaviorNode.Id)) continue;

                var stage = behaviorNode.Props is { } bProps && bProps.TryGetValue("stage", out var stageVal)
                    ? stageVal?.ToString()
                    : null;
                var responseType = pipelineEdge.Props is { } peProps && peProps.TryGetValue("response_type", out var respVal)
                    ? respVal?.ToString()
                    : null;
                var stageText = string.IsNullOrWhiteSpace(stage) ? string.Empty : $" [{stage}]";
                var responseText = string.IsNullOrWhiteSpace(responseType) ? string.Empty : $" (response={responseType})";
                AppendIndented(builder, indent, $"processed_by {behaviorNode.Name}{stageText}{responseText}");
                state.CurrentImpact?.RecordPipelineBehavior(GetDisplayName(behaviorNode));
            }

            // If no concrete pipeline edges, attempt to list generic pipeline behaviors (nodes tagged generic_request)
            if (!edges.Any(e => e.Kind == "processed_by"))
            {
                var genericBehaviors = state.Document.Nodes
                    .Where(n => n.Type == "cqrs.pipeline_behavior" && n.Props is { } p && p.TryGetValue("generic_request", out var gr) && gr is bool b && b)
                    .ToList();
                if (genericBehaviors.Count > 0)
                {
                    AppendIndented(builder, indent, $"generic_pipeline_behaviors {genericBehaviors.Count}");
                    state.CurrentImpact?.RecordGenericPipelineBehaviors(genericBehaviors.Count);
                    foreach (var gb in genericBehaviors.OrderBy(n => n.Name, StringComparer.OrdinalIgnoreCase).Take(5))
                    {
                        AppendIndented(builder, indent + 1, $"{gb.Name}");
                    }
                    if (genericBehaviors.Count > 5)
                    {
                        AppendIndented(builder, indent + 1, $"+{genericBehaviors.Count - 5} more");
                    }
                }
            }

            var handlerEdges = edges.Where(e => e.Kind == "handled_by")
                .GroupBy(e => e.To, StringComparer.Ordinal)
                .Select(g => g.First());
            foreach (var handlerEdge in handlerEdges)
            {
                if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode)) continue;
                if (state.AllowedIds != null && !state.AllowedIds.Contains(handlerNode.Id)) continue;
                var span = handlerNode.Span;
                var handlerKey = $"{handlerNode.Id}:{span?.StartLine}:{span?.EndLine}";
                state.DedupHandlers ??= new HashSet<string>(StringComparer.Ordinal);
                if (!state.DedupHandlers.Add(handlerKey)) continue;
                AppendIndented(builder, indent, $"handled_by {handlerNode.Fqdn}.Handle [L{span?.StartLine}–L{span?.EndLine}]");
                state.CurrentImpact?.RecordHandler(GetDisplayName(handlerNode));
                FlowBuilder.AppendHandlerFlow(builder, state, handlerNode, indent + 1);
            }
        }

        public static string GetDisplayName(GraphNode node)
        {
            if (!string.IsNullOrWhiteSpace(node.Name))
            {
                return node.Name!;
            }

            if (!string.IsNullOrWhiteSpace(node.Fqdn))
            {
                return node.Fqdn!;
            }

            return node.Id;
        }

        public static string ExtractOperationLabel(GraphEdge edge)
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


        public static void AppendNotificationFlow(
            StringBuilder builder,
            FlowBuilder.FlowRenderState state,
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


        public static void AppendPublisherFlow(
            StringBuilder builder,
            FlowBuilder.FlowRenderState state,
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

        public static void AppendMappingEdge(
            StringBuilder builder,
            FlowBuilder.FlowRenderState state,
            GraphEdge edge,
            int indent,
            string label = "maps_to",
            string? annotation = null,
            bool includeAutomapper = true)
        {
            if (!state.NodesById.TryGetValue(edge.To, out var destination))
            {
                return;
            }
            if (state.AllowedIds != null && !state.AllowedIds.Contains(destination.Id))
            {
                return; // not in reachability scope
            }

            var lineText = edge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
            var variable = edge.Props is { } props && props.TryGetValue("variable", out var value)
                ? value?.ToString()
                : null;
            var dedupKey = edge.From + "::" + edge.To + "::" + (variable ?? string.Empty) + "::" + label + "::" + (annotation ?? string.Empty);
            if (!state.PrintedMappings.Add(dedupKey))
            {
                return; // suppress exact duplicate mapping emission within same flow render context
            }
            var variableText = string.IsNullOrWhiteSpace(variable) ? string.Empty : $" (var {variable})";
            var annotationText = string.IsNullOrWhiteSpace(annotation) ? string.Empty : $" [{annotation}]";
            AppendIndented(builder, indent, $"{label} {destination.Name}{variableText}{lineText}{annotationText}");
            state.CurrentImpact?.RecordMapping(GetDisplayName(destination));

            if (includeAutomapper)
            {
                FlowBuilder.AppendAutomapperRegistrations(builder, state, edge, indent + 1);
            }

            if (!state.EdgesByFrom.TryGetValue(destination.Id, out var downstreamEdges))
            {
                return;
            }
            // Prevent expansion of downstream if outside allowed set
            if (state.AllowedIds != null && !state.AllowedIds.Contains(destination.Id)) return;

            foreach (var convertEdge in downstreamEdges.Where(e => e.Kind == "converts_to"))
            {
                FlowBuilder.AppendConversion(builder, state, convertEdge, indent + 1);
            }

            if (destination.Type == "cqrs.request")
            {
                AppendCommandFlow(builder, state, destination, indent + 1);
            }
        }

        public static void AppendNotificationHandlerFlow(
            StringBuilder builder,
            FlowBuilder.FlowRenderState state,
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
                    FlowBuilder.AppendRepositoryFlow(builder, state, target, indent + 1);
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

                    FlowBuilder.AppendServiceContractFlow(builder, state, handler, serviceNode, serviceMethodName, nextIndent);
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


        public static string GetSolutionRoot(string? project)
        {
            if (string.IsNullOrWhiteSpace(project))
            {
                return string.Empty;
            }

            var normalized = project.Replace('\\', '/');
            var separatorIndex = normalized.IndexOf('/');
            return separatorIndex > 0 ? normalized[..separatorIndex] : normalized;
        }

        public static bool IsWithinCallerSolution(GraphNode caller, GraphNode candidate)
        {
            var callerSolution = GetSolutionRoot(caller.Project);
            var candidateSolution = GetSolutionRoot(candidate.Project);

            if (!string.IsNullOrWhiteSpace(callerSolution) && !string.IsNullOrWhiteSpace(candidateSolution))
            {
                return string.Equals(callerSolution, candidateSolution, StringComparison.OrdinalIgnoreCase);
            }

            var callerRoot = GetAssemblyRoot(caller.Assembly);
            var candidateRoot = GetAssemblyRoot(candidate.Assembly);
            if (!string.IsNullOrWhiteSpace(callerRoot) && !string.IsNullOrWhiteSpace(candidateRoot))
            {
                return string.Equals(callerRoot, candidateRoot, StringComparison.OrdinalIgnoreCase);
            }

            return true;
        }

        public static bool ShouldIncludeImplementation(GraphNode caller, GraphNode implementation)
        {
            if (string.Equals(implementation.Type, "cqrs.request", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (string.Equals(caller.Id, implementation.Id, StringComparison.Ordinal))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(implementation.Assembly))
            {
                return true;
            }

            if (implementation.Assembly.IndexOf(".Tests", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return false;
            }

            // Heuristic inclusion rules (after solution filtering):
            // 1. Always include if the implementation has a concrete source file (internal code).
            // 2. Include if assembly roots match (likely same bounded context / solution segment).
            // 3. Exclude otherwise (likely external / framework / heuristic duplicate) to avoid noisy expansions.

            var callerRoot = GetAssemblyRoot(caller.Assembly);
            var implRoot = GetAssemblyRoot(implementation.Assembly);
            var hasFile = !string.IsNullOrWhiteSpace(implementation.FilePath) && !implementation.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase);
            if (!IsWithinCallerSolution(caller, implementation))
            {
                return false;
            }

            if (hasFile)
            {
                return true;
            }

            if (string.Equals(callerRoot, implRoot, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static string GetAssemblyRoot(string assembly)
        {
            if (string.IsNullOrWhiteSpace(assembly))
            {
                return string.Empty;
            }

            var separatorIndex = assembly.IndexOf('.');
            return separatorIndex > 0 ? assembly[..separatorIndex] : assembly;
        }

        public static string? CanonicalizeRoute(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return null;
            }

            var trimmed = route.Trim();

            if (Uri.TryCreate(trimmed, UriKind.Absolute, out var absolute))
            {
                trimmed = absolute.AbsolutePath;
            }
            else if (trimmed.Contains("://", StringComparison.Ordinal))
            {
                var parts = trimmed.Split(new[] { "://" }, 2, StringSplitOptions.None);
                var remainder = parts.Length == 2 ? parts[1] : trimmed;
                var slashIndex = remainder.IndexOf('/');
                trimmed = slashIndex >= 0 ? remainder[slashIndex..] : "/";
            }

            var questionIndex = trimmed.IndexOf('?', StringComparison.Ordinal);
            if (questionIndex >= 0)
            {
                trimmed = trimmed[..questionIndex];
            }

            trimmed = trimmed.Replace('\\', '/');
            while (trimmed.Contains("//", StringComparison.Ordinal))
            {
                trimmed = trimmed.Replace("//", "/", StringComparison.Ordinal);
            }

            trimmed = trimmed.Trim();
            if (trimmed.Length == 0)
            {
                return "/";
            }

            if (!trimmed.StartsWith("/", StringComparison.Ordinal))
            {
                trimmed = "/" + trimmed;
            }

            trimmed = Uri.UnescapeDataString(trimmed);

            var segments = trimmed.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var cleaned = new List<string>(segments.Length);
            for (var i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                if (segment.StartsWith("{", StringComparison.Ordinal) && segment.EndsWith("}", StringComparison.Ordinal))
                {
                    var colonIndex = segment.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        segment = segment[..colonIndex] + "}";
                    }
                }

                var lowered = segment.ToLowerInvariant();
                if (IsVersionSegment(lowered) && (cleaned.Count == 0 || (cleaned.Count == 1 && cleaned[0] == "api")))
                {
                    continue;
                }

                cleaned.Add(lowered);
            }

            if (cleaned.Count == 0)
            {
                return "/";
            }

            return "/" + string.Join('/', cleaned);
        }

        public static bool IsVersionSegment(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                return false;
            }

            if (!segment.StartsWith('v'))
            {
                return false;
            }

            var payload = segment[1..];
            if (payload.StartsWith("{", StringComparison.Ordinal))
            {
                return true;
            }

            var hasDigit = false;
            foreach (var ch in payload)
            {
                if (char.IsDigit(ch))
                {
                    hasDigit = true;
                    continue;
                }

                if (ch is '.' or '_' or '-')
                {
                    continue;
                }

                return false;
            }

            return hasDigit;
        }

        public static string BuildPublisherDetails(GraphNode publisher)
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
    }
}
