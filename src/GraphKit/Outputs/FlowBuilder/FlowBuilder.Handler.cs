using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using static GraphKit.Outputs.Utilities;
namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
	public static void AppendHandlerFlow(
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

			// Configuration for handler
			foreach (var configEdge in edges.Where(e => e.Kind == "uses_configuration"))
			{
				if (!state.NodesById.TryGetValue(configEdge.To, out var configNode))
				{
					continue;
				}

				var key = configEdge.Props is { } cprops && cprops.TryGetValue("key", out var keyVal)
					? keyVal?.ToString()
					: null;
				var accessor = configEdge.Props is { } cprops2 && cprops2.TryGetValue("accessor", out var accVal)
					? accVal?.ToString()
					: null;
				var value = configEdge.Props is { } cprops3 && cprops3.TryGetValue("value", out var valVal)
					? valVal?.ToString()
					: null;
				var lineText = configEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var detailParts = new List<string>();
				if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
				if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
				var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
				var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
				AppendIndented(builder, indent, $"uses_configuration {details}{valueText}{lineText}");
			}

			// Group repository calls in handler
			var handlerCallEdges = edges.Where(e => e.Kind == "calls").ToList();
			var printedHandlerCallKeys = new HashSet<string>(StringComparer.Ordinal);
			for (int i = 0; i < handlerCallEdges.Count; i++)
			{
				var call = handlerCallEdges[i];
				if (!state.NodesById.TryGetValue(call.To, out var target)) continue;
				if (state.AllowedIds is { } allow && !allow.Contains(target.Id)) continue;
				var isRepo = target.Type == "app.repository" || target.Type == "repository";
				if (!isRepo)
				{
					var callMethod = call.Props is { } props && props.TryGetValue("method", out var methodValue) ? methodValue?.ToString() : null;
					var dedupKey = BuildCallDedupKey(call, target, callMethod);
					if (!printedHandlerCallKeys.Add(dedupKey))
					{
						continue;
					}
					var serviceMethodText = string.IsNullOrWhiteSpace(callMethod) ? string.Empty : $".{callMethod}";
					var lineText = call.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
					AppendIndented(builder, indent, $"calls {target.Name}{serviceMethodText}{lineText}");
					continue;
				}
				var methods = new List<string>();
				int? firstLine = call.Transform?.Location?.Line;
				int j = i;
				while (j < handlerCallEdges.Count)
				{
					var ej = handlerCallEdges[j];
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

			foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
			{
				if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
				{
					continue;
				}
				if (state.AllowedIds is { } allow && !allow.Contains(entityNode.Id))
				{
					continue;
				}

				var lineText = dataEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var label = ExtractOperationLabel(dataEdge);
				var dedupKey = $"{handler.Id}::{dataEdge.To}::{label}::{lineText}";
				state.DedupHandlers ??= new HashSet<string>(StringComparer.Ordinal);
				if (!state.DedupHandlers.Add("DB::" + dedupKey))
				{
					continue;
				}

				AppendIndented(builder, indent, $"{label} {entityNode.Name}{lineText}");
				state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);
				AppendEntityFlow(builder, state, entityNode, indent + 1);
			}

			foreach (var mapping in edges.Where(e => e.Kind == "maps_to"))
			{
				AppendMappingEdge(builder, state, mapping, indent);
			}

			foreach (var dataEdge in edges.Where(e => e.Kind is "queries" or "writes_to" or "inserts_into" or "updates" or "deletes_from" or "upserts"))
			{
				if (!state.NodesById.TryGetValue(dataEdge.To, out var entityNode))
				{
					continue;
				}

				if (state.AllowedIds is { } allow && !allow.Contains(entityNode.Id))
				{
					continue;
				}

				var lineText = dataEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var label = ExtractOperationLabel(dataEdge);
				AppendIndented(builder, indent, $"{label} {entityNode.Name}{lineText}");
				state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);

				if (entityNode.Type == "ef.entity")
				{
					AppendEntityFlow(builder, state, entityNode, indent + 1, dataEdge.Kind);
				}
			}

			foreach (var clientEdge in edges.Where(e => e.Kind == "uses_client"))
			{
				if (!state.NodesById.TryGetValue(clientEdge.To, out var clientNode))
				{
					continue;
				}
				if (state.AllowedIds is { } allow && !allow.Contains(clientNode.Id)) continue;

				AppendHttpClientUsage(builder, state, clientEdge, clientNode, indent);
			}

			foreach (var service in edges.Where(e => e.Kind == "uses_service"))
			{
				if (!state.NodesById.TryGetValue(service.To, out var serviceNode))
				{
					continue;
				}
				if (state.AllowedIds is { } allow && !allow.Contains(serviceNode.Id)) continue;

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

			foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
			{
				if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
				{
					continue;
				}

				var lineText = storageEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var value)
					? value?.ToString()
					: null;
				var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
				AppendIndented(builder, indent, $"uses_storage {storageNode.Name}{methodSuffix}{lineText}");
			}

			foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
			{
				if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode))
				{
					continue;
				}

				var lineText = logEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var level = logEdge.Props is { } props && props.TryGetValue("level", out var levelValue)
					? levelValue?.ToString()
					: null;
				var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
				AppendIndented(builder, indent, $"logs {loggerNode.Name}{levelText}{lineText}");
			}

			foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
			{
				if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode))
				{
					continue;
				}

				var lineText = validationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var validationMethod = validationEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
					? methodValue?.ToString()
					: null;
				var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
				AppendIndented(builder, indent, $"validation {guardNode.Name}{methodText}{lineText}");
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
			}

			foreach (var publish in edges.Where(e => e.Kind == "publishes"))
			{
				if (!state.NodesById.TryGetValue(publish.To, out var messageNode))
				{
					continue;
				}

				var lineText = publish.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				var details = BuildPublisherDetails(messageNode);
				AppendIndented(builder, indent, $"publishes {messageNode.Name}{details}{lineText}");
				state.CurrentImpact?.RecordMessage(GetDisplayName(messageNode));
				AppendPublisherFlow(builder, state, messageNode, indent + 1);
			}

			foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
			{
				if (!state.NodesById.TryGetValue(notificationEdge.To, out var notificationNode))
				{
					continue;
				}

				var lineText = notificationEdge.Transform?.Location?.Line is int line ? $" [L{line}]" : string.Empty;
				AppendIndented(builder, indent, $"publishes_notification {notificationNode.Name}{lineText}");
				AppendNotificationFlow(builder, state, notificationNode, indent + 1);
			}
		}
		finally
		{
			state.HandlerStack.Remove(handler.Id);
		}
	}
}