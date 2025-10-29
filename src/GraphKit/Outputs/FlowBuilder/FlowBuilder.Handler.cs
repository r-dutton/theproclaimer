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

			var entityExpansionKeys = new HashSet<string>(StringComparer.Ordinal);

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
				var detailParts = new List<string>();
				if (!string.IsNullOrWhiteSpace(accessor)) detailParts.Add(accessor!);
				if (!string.IsNullOrWhiteSpace(key)) detailParts.Add(key!);
				var details = detailParts.Count > 0 ? string.Join(":", detailParts) : configNode.Name;
				var valueText = string.IsNullOrWhiteSpace(value) ? string.Empty : $" value={value}";
				var baseLabel = $"uses_configuration {details}";
				AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, configEdge.Transform?.Location)}{valueText}");
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
					var label = $"calls {target.Name}{serviceMethodText}";
					AppendIndented(builder, indent, FormatLinkedCode(label, call.Transform?.Location));
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
					var label = $"calls {target.Name}{serviceMethodText}";
					AppendIndented(builder, indent, FormatLinkedCode(label, call.Transform?.Location));
				}
				else
				{
					var methodsPart = $" (methods: {string.Join(",", uniqueMethods)})";
					string? groupFile = call.Transform?.Location?.File;
					if (string.IsNullOrWhiteSpace(groupFile))
					{
						groupFile = handlerCallEdges
							.Skip(i)
							.Select(edge => edge.Transform?.Location?.File)
							.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f));
					}
					var label = $"calls {target.Name}{methodsPart}";
					var linked = firstLine.HasValue
						? FormatLinkedCode(label, groupFile, firstLine, null)
						: label;
					AppendIndented(builder, indent, linked);
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

				var label = ExtractOperationLabel(dataEdge);
				var locationSignature = dataEdge.Transform?.Location is { File: var file, Line: var line }
					? $"{file}:{line}"
					: string.Empty;
				var dedupKey = $"{handler.Id}::{dataEdge.To}::{label}::{locationSignature}";
				if (!entityExpansionKeys.Add("DB::" + dedupKey))
				{
					continue;
				}

				var baseLabel = $"{label} {entityNode.Name}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, dataEdge.Transform?.Location));
				state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);
				if (Utilities.IsEntityNode(entityNode) || Utilities.IsLikelyEntity(entityNode))
				{
					AppendEntityFlow(builder, state, entityNode, indent + 1);
				}
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

				var label = ExtractOperationLabel(dataEdge);
				var baseLabel = $"{label} {entityNode.Name}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, dataEdge.Transform?.Location));
				state.CurrentImpact?.RecordEntityOperation(GetDisplayName(entityNode), dataEdge.Kind);

				if (Utilities.IsEntityNode(entityNode) || Utilities.IsLikelyEntity(entityNode))
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

								AppendServiceContractFlow(builder, state, handler, serviceNode, serviceMethodName, nextIndent, service);
			}

			foreach (var storageEdge in edges.Where(e => e.Kind == "uses_storage"))
			{
				if (!state.NodesById.TryGetValue(storageEdge.To, out var storageNode))
				{
					continue;
				}

				var methodName = storageEdge.Props is { } props && props.TryGetValue("method", out var value)
					? value?.ToString()
					: null;
				var methodSuffix = string.IsNullOrWhiteSpace(methodName) ? string.Empty : $".{methodName}";
				var baseLabel = $"uses_storage {storageNode.Name}{methodSuffix}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, storageEdge.Transform?.Location));
			}

			foreach (var logEdge in edges.Where(e => e.Kind == "logs"))
			{
				if (!state.NodesById.TryGetValue(logEdge.To, out var loggerNode))
				{
					continue;
				}

				var level = logEdge.Props is { } props && props.TryGetValue("level", out var levelValue)
					? levelValue?.ToString()
					: null;
				var levelText = string.IsNullOrWhiteSpace(level) ? string.Empty : $" [{level}]";
				var baseLabel = $"logs {loggerNode.Name}";
				AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, logEdge.Transform?.Location)}{levelText}");
			}

			foreach (var validationEdge in edges.Where(e => e.Kind == "validation"))
			{
				if (!state.NodesById.TryGetValue(validationEdge.To, out var guardNode))
				{
					continue;
				}

				var validationMethod = validationEdge.Props is { } props && props.TryGetValue("method", out var methodValue)
					? methodValue?.ToString()
					: null;
				var methodText = string.IsNullOrWhiteSpace(validationMethod) ? string.Empty : $".{validationMethod}";
				var baseLabel = $"validation {guardNode.Name}{methodText}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, validationEdge.Transform?.Location));
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
				var methodPart = string.IsNullOrWhiteSpace(cacheMethod) ? string.Empty : $".{cacheMethod}";
				var opPart = string.IsNullOrWhiteSpace(operation) ? string.Empty : $" [{operation}]";
				var keyPart = string.IsNullOrWhiteSpace(key) ? string.Empty : $" (key={key})";
				var cacheKey = cacheEdge.From + "::" + cacheEdge.To + "::" + cacheMethod + "::" + operation + "::" + key;
				state.DedupRequests ??= new HashSet<string>(StringComparer.Ordinal);
				if (!state.DedupRequests.Add("CACHE::" + cacheKey)) continue;
				var baseLabel = $"uses_cache {cacheNode.Name}{methodPart}";
				AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, cacheEdge.Transform?.Location)}{opPart}{keyPart}");
			}

			foreach (var optionsEdge in edges.Where(e => e.Kind == "uses_options"))
			{
				if (!state.NodesById.TryGetValue(optionsEdge.To, out var optionsNode))
				{
					continue;
				}

				var section = GetNodeProp(optionsNode, "section");
				var sectionText = string.IsNullOrWhiteSpace(section) ? string.Empty : $" ({section})";
				var baseLabel = $"uses_options {optionsNode.Name}{sectionText}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, optionsEdge.Transform?.Location));
			}

			foreach (var publish in edges.Where(e => e.Kind == "publishes"))
			{
				if (!state.NodesById.TryGetValue(publish.To, out var messageNode))
				{
					continue;
				}

				var details = BuildPublisherDetails(messageNode);
				var baseLabel = $"publishes {messageNode.Name}";
				AppendIndented(builder, indent, $"{FormatLinkedCode(baseLabel, publish.Transform?.Location)}{details}");
				state.CurrentImpact?.RecordMessage(GetDisplayName(messageNode));
				AppendPublisherFlow(builder, state, messageNode, indent + 1);
			}

			foreach (var notificationEdge in edges.Where(e => e.Kind == "publishes_notification"))
			{
				if (!state.NodesById.TryGetValue(notificationEdge.To, out var notificationNode))
				{
					continue;
				}

				var baseLabel = $"publishes_notification {notificationNode.Name}";
				AppendIndented(builder, indent, FormatLinkedCode(baseLabel, notificationEdge.Transform?.Location));
				AppendNotificationFlow(builder, state, notificationNode, indent + 1);
			}
		}
		finally
		{
			state.HandlerStack.Remove(handler.Id);
		}
	}
}
