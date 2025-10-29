using GraphKit.Graph;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace GraphKit.Outputs
{
    public static partial class Utilities
    {
        public static string? GetNodeProp(GraphNode node, string key)
            => node.Props is { } props && props.TryGetValue(key, out var value) ? ToStringValue(value) : null;

        private static string? BuildCodeLinkTarget(string? filePath, int? startLine = null, int? endLine = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }

            if (filePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var normalized = filePath.Replace('\\', '/');
            if (!normalized.StartsWith("./") && !normalized.StartsWith("../"))
            {
                normalized = "./" + normalized.TrimStart('/');
            }

            string anchor = string.Empty;
            if (startLine.HasValue)
            {
                anchor = endLine.HasValue && endLine.Value > startLine.Value
                    ? $"#L{startLine}-L{endLine}"
                    : $"#L{startLine}";
            }

            return $"{normalized}{anchor}";
        }

        public static string FormatCodeLink(string? filePath, int? startLine = null, int? endLine = null)
        {
            var target = BuildCodeLinkTarget(filePath, startLine, endLine);
            return target is null ? string.Empty : $" [View Code]({target})";
        }

        public static string FormatCodeLink(GraphLocation? location)
            => location is null ? string.Empty : FormatCodeLink(location.File, location.Line);

        public static string FormatCodeLink(GraphNode node)
        {
            if (node is null)
            {
                return string.Empty;
            }

            var span = node.Span;
            if (span is null)
            {
                return FormatCodeLink(node.FilePath);
            }

            var endLine = span.EndLine > span.StartLine ? span.EndLine : (int?)null;
            return FormatCodeLink(node.FilePath, span.StartLine, endLine);
        }

        public static string FormatLinkedCode(string label, string? filePath, int? startLine = null, int? endLine = null)
        {
            var trimmedLabel = string.IsNullOrWhiteSpace(label) ? "View Code" : label;
            var target = BuildCodeLinkTarget(filePath, startLine, endLine);
            return target is null ? trimmedLabel : $"[{trimmedLabel}]({target})";
        }

        public static string FormatLinkedCode(string label, GraphLocation? location)
            => location is null ? (string.IsNullOrWhiteSpace(label) ? string.Empty : label) : FormatLinkedCode(label, location.File, location.Line);

        public static string FormatLinkedCode(string label, GraphNode node)
        {
            if (node is null)
            {
                return string.IsNullOrWhiteSpace(label) ? string.Empty : label;
            }

            var span = node.Span;
            if (span is null)
            {
                return FormatLinkedCode(label, node.FilePath);
            }

            var endLine = span.EndLine > span.StartLine ? span.EndLine : (int?)null;
            return FormatLinkedCode(label, node.FilePath, span.StartLine, endLine);
        }

        private static string? ToStringValue(object? value)
        {
            switch (value)
            {
                case null:
                    return null;
                case string text:
                    return text;
                case JsonElement element:
                    if (element.ValueKind == JsonValueKind.Null)
                    {
                        return null;
                    }

                    if (element.ValueKind == JsonValueKind.String)
                    {
                        return element.GetString();
                    }

                    return element.ToString();
                default:
                    return value.ToString();
            }
        }

        private static string NormalizeMethodName(string value)
        {
            var trimmed = value.Trim();
            if (trimmed.Length == 0)
            {
                return string.Empty;
            }

            if (trimmed.EndsWith("Async", StringComparison.OrdinalIgnoreCase) && trimmed.Length > 5)
            {
                trimmed = trimmed[..^5];
            }

            return trimmed;
        }

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
            builder.AppendLine($"- {text}");
        }

        public static string? GetCallMethod(GraphEdge edge)
        {
            return edge.Props is { } props && props.TryGetValue("method", out var methodValue)
                ? ToStringValue(methodValue)
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

            var normalizedInvoked = NormalizeMethodName(invokedMethod);
            if (string.IsNullOrWhiteSpace(normalizedInvoked))
            {
                return true;
            }

            if (edge.Props is not { Count: > 0 } props)
            {
                // No metadata to filter on; allow expansion to avoid hiding relevant edges.
                return true;
            }

            static bool IsHttpVerb(string candidate)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    return false;
                }

                var trimmed = candidate.Trim();
                if (trimmed.Length is < 3 or > 10)
                {
                    return false;
                }

                return trimmed.Equals("GET", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("DELETE", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("PATCH", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("HEAD", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("TRACE", StringComparison.OrdinalIgnoreCase) ||
                       trimmed.Equals("CONNECT", StringComparison.OrdinalIgnoreCase);
            }

            var sawCandidates = false;

            bool Matches(string key, object? value)
            {
                var matched = false;

                foreach (var candidate in EnumerateMethodCandidates(value))
                {
                    if (string.IsNullOrWhiteSpace(candidate))
                    {
                        continue;
                    }

                    var trimmed = candidate.Trim();
                    if (trimmed.Length == 0)
                    {
                        continue;
                    }

                    if (key.Equals("method", StringComparison.OrdinalIgnoreCase) && IsHttpVerb(trimmed))
                    {
                        sawCandidates = true;
                        continue;
                    }

                    var normalizedCandidate = NormalizeMethodName(trimmed);
                    if (string.IsNullOrWhiteSpace(normalizedCandidate))
                    {
                        continue;
                    }

                    sawCandidates = true;

                    if (string.Equals(normalizedCandidate, normalizedInvoked, StringComparison.OrdinalIgnoreCase) ||
                        normalizedCandidate.EndsWith("." + normalizedInvoked, StringComparison.OrdinalIgnoreCase))
                    {
                        matched = true;
                        break;
                    }
                }

                return matched;
            }

            static bool KeyEquals(string candidate, string key)
                => string.Equals(candidate, key, StringComparison.OrdinalIgnoreCase);

            static bool IsPreferredKey(string key)
                => KeyEquals(key, "owner_method") ||
                   KeyEquals(key, "handler_method") ||
                   KeyEquals(key, "declaring_method") ||
                   KeyEquals(key, "service_method") ||
                   KeyEquals(key, "invoked_method") ||
                   KeyEquals(key, "caller_method") ||
                   KeyEquals(key, "method");

            foreach (var preferred in props)
            {
                if (preferred.Key is null)
                {
                    continue;
                }

                if (!IsPreferredKey(preferred.Key))
                {
                    continue;
                }

                if (Matches(preferred.Key, preferred.Value))
                {
                    return true;
                }
            }

            foreach (var kvp in props)
            {
                if (kvp.Key is null)
                {
                    continue;
                }

                if (IsPreferredKey(kvp.Key))
                {
                    continue;
                }

                if (kvp.Key.IndexOf("method", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }

                if (Matches(kvp.Key, kvp.Value))
                {
                    return true;
                }
            }

            return !sawCandidates;
        }

        private static IEnumerable<string> EnumerateMethodCandidates(object? value)
        {
            if (value is null)
            {
                yield break;
            }

            switch (value)
            {
                case string text when !string.IsNullOrWhiteSpace(text):
                    yield return text;
                    yield break;

                case JsonElement element:
                    switch (element.ValueKind)
                    {
                        case JsonValueKind.Array:
                            foreach (var child in element.EnumerateArray())
                            {
                                foreach (var candidate in EnumerateMethodCandidates((object)child))
                                {
                                    yield return candidate;
                                }
                            }
                            yield break;
                        case JsonValueKind.String:
                            var str = element.GetString();
                            if (!string.IsNullOrWhiteSpace(str))
                            {
                                yield return str;
                            }
                            yield break;
                        case JsonValueKind.Null:
                        case JsonValueKind.Undefined:
                            yield break;
                        default:
                            var fallback = element.ToString();
                            if (!string.IsNullOrWhiteSpace(fallback))
                            {
                                yield return fallback;
                            }
                            yield break;
                    }

                case IEnumerable<string> stringEnumerable:
                    foreach (var entry in stringEnumerable)
                    {
                        if (!string.IsNullOrWhiteSpace(entry))
                        {
                            yield return entry;
                        }
                    }
                    yield break;

                default:
                    if (value is System.Collections.IDictionary)
                    {
                        var dictString = value.ToString();
                        if (!string.IsNullOrWhiteSpace(dictString))
                        {
                            yield return dictString;
                        }
                        yield break;
                    }

                    if (value is System.Collections.IEnumerable enumerable && value is not string)
                    {
                        foreach (var item in enumerable)
                        {
                            foreach (var candidate in EnumerateMethodCandidates(item))
                            {
                                yield return candidate;
                            }
                        }
                        yield break;
                    }

                    var fallbackText = value.ToString();
                    if (!string.IsNullOrWhiteSpace(fallbackText))
                    {
                        yield return fallbackText;
                    }
                    break;
            }
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
                if (!state.IsAllowedNode(behaviorNode.Id)) continue;

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
                    var distinctBehaviors = genericBehaviors
                        .GroupBy(n => GetDisplayName(n), StringComparer.OrdinalIgnoreCase)
                        .Select(g => g.First())
                        .OrderBy(n => GetDisplayName(n), StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    AppendIndented(builder, indent, $"generic_pipeline_behaviors {distinctBehaviors.Count}");
                    state.CurrentImpact?.RecordGenericPipelineBehaviors(distinctBehaviors.Count);
                    foreach (var gb in distinctBehaviors.Take(5))
                    {
                        AppendIndented(builder, indent + 1, GetDisplayName(gb));
                    }
                    if (distinctBehaviors.Count > 5)
                    {
                        AppendIndented(builder, indent + 1, $"+{distinctBehaviors.Count - 5} more");
                    }
                }
            }

            var handlerEdges = edges.Where(e => e.Kind == "handled_by")
                .GroupBy(e => e.To, StringComparer.Ordinal)
                .Select(g => g.First());
            var expandedHandlers = new HashSet<string>(StringComparer.Ordinal);
            foreach (var handlerEdge in handlerEdges)
            {
                if (!state.NodesById.TryGetValue(handlerEdge.To, out var handlerNode)) continue;
                if (!state.IsAllowedNode(handlerNode.Id)) continue;
                var span = handlerNode.Span;
                var handlerKey = $"{handlerNode.Id}:{span?.StartLine}:{span?.EndLine}";
                if (!expandedHandlers.Add(handlerKey))
                {
                    continue;
                }
                var label = $"handled_by {handlerNode.Fqdn}.Handle";
                var linkedLabel = FormatLinkedCode(label, handlerNode);
                AppendIndented(builder, indent, linkedLabel);
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

                    var label = $"handled_by {handlerNode.Fqdn}.Handle";
                    AppendIndented(builder, indent, FormatLinkedCode(label, handlerNode));
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

                var label = $"produces_event {contract.Name}";
                AppendIndented(builder, indent, FormatLinkedCode(label, produced.Transform?.Location));
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
            if (!state.IsAllowedNode(destination.Id))
            {
                return; // not in reachability scope
            }

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
            var destinationLabel = destination.Name;
            if (string.Equals(label, "returns", StringComparison.OrdinalIgnoreCase) && edge.Props is { } returnProps && returnProps.TryGetValue("response_type", out var responseValue))
            {
                var responseType = responseValue?.ToString();
                if (!string.IsNullOrWhiteSpace(responseType))
                {
                    destinationLabel = responseType;
                }
            }
            var baseLabel = $"{label} {destinationLabel}{variableText}";
            AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, edge.Transform?.Location)}{annotationText}");
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
            if (!state.IsAllowedNode(destination.Id)) return;

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
                        var label = $"calls {target.Name}{serviceMethodText}";
                        AppendIndented(builder, indent, FormatLinkedCode(label, call.Transform?.Location));
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
                        var label = $"calls {target.Name}{serviceMethodText}";
                        AppendIndented(builder, indent, FormatLinkedCode(label, call.Transform?.Location));
                    }
                    else
                    {
                        var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
                        var label = $"calls {target.Name}{methodsPart}";
                        var linked = firstLine.HasValue
                            ? FormatLinkedCode(label, call.Transform?.Location?.File, firstLine, null)
                            : label;
                        AppendIndented(builder, indent, linked);
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

                    var lifetime = service.Props is { } props && props.TryGetValue("lifetime", out var lifetimeValue)
                        ? lifetimeValue?.ToString()
                        : null;
                    var suffix = string.IsNullOrWhiteSpace(lifetime) ? string.Empty : $" ({lifetime})";
                    var serviceMethodName = service.Props is { } serviceProps && serviceProps.TryGetValue("method", out var methodValue)
                        ? methodValue?.ToString()
                        : null;
                    var baseLabel = $"uses_service {serviceNode.Name}{suffix}";
                    var nextIndent = indent + 1;
                    if (string.IsNullOrWhiteSpace(serviceMethodName))
                    {
                        AppendIndented(builder, indent, FormatLinkedCode(baseLabel, service.Transform?.Location));
                    }
                    else
                    {
                        AppendIndented(builder, indent, baseLabel);
                        var methodLabel = $"method {serviceMethodName}";
                        AppendIndented(builder, indent + 1, FormatLinkedCode(methodLabel, service.Transform?.Location));
                        nextIndent = indent + 2;
                    }

                    FlowBuilder.AppendServiceContractFlow(builder, state, handler, serviceNode, serviceMethodName, nextIndent, service);
                }

                foreach (var requestEdge in edges.Where(e => e.Kind == "sends_request"))
                {
                    if (!state.NodesById.TryGetValue(requestEdge.To, out var requestNode))
                    {
                        continue;
                    }

                    var label = $"sends_request {requestNode.Name}";
                    AppendIndented(builder, indent, FormatLinkedCode(label, requestEdge.Transform?.Location));
                    AppendCommandFlow(builder, state, requestNode, indent + 1);
                }

                foreach (var publish in edges.Where(e => e.Kind == "publishes_notification"))
                {
                    if (!state.NodesById.TryGetValue(publish.To, out var notificationNode))
                    {
                        continue;
                    }

                    var label = $"publishes_notification {notificationNode.Name}";
                    AppendIndented(builder, indent, FormatLinkedCode(label, publish.Transform?.Location));
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


namespace GraphKit.Outputs
{
    public static partial class Utilities
    {
    [GeneratedRegex(@"\{[^}]+\}", RegexOptions.NonBacktracking)]
    private static partial Regex RouteParamRx();

            [GeneratedRegex(@"With(?:Required|Optional)QueryParameter\(\s*""([^""]+)""\)", RegexOptions.NonBacktracking)]
    private static partial Regex QueryParamRx();

        private static readonly ConcurrentDictionary<string, string> __routeCache = new(StringComparer.Ordinal);

        public static string NormaliseRouteCached(string raw)
            => string.IsNullOrEmpty(raw)
                ? raw
                : __routeCache.GetOrAdd(raw, static r => RouteParamRx().Replace(r, "{}"));
    }
}
