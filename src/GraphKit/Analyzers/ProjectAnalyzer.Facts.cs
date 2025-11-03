using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphKit.Constants;
using GraphKit.Facts;
using GraphKit.Graph;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private string EnsureControllerFactNode(ControllerActionInfo action)
    {
        var id = StableId.For("endpoint.controller", action.Fqdn, action.Assembly, action.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = action.Name,
            ["fqdn"] = action.Fqdn,
            ["assembly"] = action.Assembly,
            ["project"] = action.Project,
            ["route"] = action.Route,
            ["http_method"] = action.HttpMethod,
            ["verb"] = action.HttpMethod,
            ["symbol_id"] = action.SymbolId
        };
        props["controller_display"] = GetControllerDisplay(action);
        var auth = DetectAuth(action);
        if (!string.IsNullOrWhiteSpace(auth))
        {
            props["auth"] = auth;
        }
        AddSource(props, action.FilePath, action.Span);
        _facts.AddNode(new NodeFact(id, "endpoint.controller", props));
        return id;
    }

    private string EnsureMinimalEndpointFactNode(MinimalEndpointInfo endpoint)
    {
        var id = StableId.For("endpoint.minimal_api", endpoint.Fqdn, endpoint.Assembly, endpoint.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = endpoint.Name,
            ["fqdn"] = endpoint.Fqdn,
            ["assembly"] = endpoint.Assembly,
            ["project"] = endpoint.Project,
            ["route"] = endpoint.Route,
            ["http_method"] = endpoint.HttpMethod,
            ["verb"] = endpoint.HttpMethod,
            ["symbol_id"] = endpoint.SymbolId
        };
        props["controller_display"] = endpoint.Name;
        var auth = DetectAuth(endpoint);
        if (!string.IsNullOrWhiteSpace(auth))
        {
            props["auth"] = auth;
        }
        AddSource(props, endpoint.FilePath, endpoint.Span);
        _facts.AddNode(new NodeFact(id, "endpoint.minimal_api", props));
        return id;
    }

    private string EnsureRequestFactNode(string requestType, RequestInfo? info, string? assemblyHint, string? projectHint)
    {
        var fqdn = info?.Fqdn ?? requestType;
        var assembly = info?.Assembly ?? assemblyHint ?? GuessAssemblyName(fqdn);
        var symbolId = info?.SymbolId ?? $"T:{fqdn}";
        var id = StableId.For("cqrs.request", fqdn, assembly, symbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = info?.Name ?? GetTopLevelSimpleIdentifier(fqdn),
            ["fqdn"] = fqdn,
            ["assembly"] = assembly,
            ["project"] = info?.Project ?? projectHint,
            ["symbol_id"] = symbolId
        };

        if (!string.IsNullOrWhiteSpace(info?.ResponseType) && !IsGenericPlaceholder(info.ResponseType))
        {
            props["response_type"] = info.ResponseType;
        }

        AddSource(props, info?.FilePath, info?.Span);
        _facts.AddNode(new NodeFact(id, "cqrs.request", props));
        return id;
    }

    private string EnsureHandlerFactNode(HandlerInfo handler)
    {
        var id = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = handler.Name,
            ["fqdn"] = handler.Fqdn,
            ["assembly"] = handler.Assembly,
            ["project"] = handler.Project,
            ["request_type"] = handler.RequestType,
            ["symbol_id"] = handler.SymbolId
        };

        if (!string.IsNullOrWhiteSpace(handler.ResponseType) && !IsGenericPlaceholder(handler.ResponseType))
        {
            props["response_type"] = handler.ResponseType;
        }

        AddSource(props, handler.FilePath, handler.Span);
        _facts.AddNode(new NodeFact(id, "cqrs.handler", props));
        return id;
    }

    private string EnsureNotificationFactNode(NotificationInfo notification)
    {
        var id = StableId.For("cqrs.notification", notification.Fqdn, notification.Assembly, notification.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = notification.Name,
            ["fqdn"] = notification.Fqdn,
            ["assembly"] = notification.Assembly,
            ["project"] = notification.Project,
            ["contract_type"] = notification.ContractType,
            ["symbol_id"] = notification.SymbolId
        };
        AddSource(props, notification.FilePath, notification.Span);
        _facts.AddNode(new NodeFact(id, "cqrs.notification", props));
        return id;
    }

    private string EnsureHttpClientFactNode(string clientType, string? verb = null, string? route = null)
    {
        var assembly = GuessAssemblyName(clientType);
        var symbolId = $"T:{clientType}";
        var id = StableId.For("http.client", clientType, assembly, symbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = GetTopLevelSimpleIdentifier(clientType),
            ["fqdn"] = clientType,
            ["assembly"] = assembly,
            ["symbol_id"] = symbolId
        };

        if (!string.IsNullOrWhiteSpace(verb))
        {
            props["verb"] = verb;
        }

        if (!string.IsNullOrWhiteSpace(route))
        {
            props["route"] = route;
        }

        _facts.AddNode(new NodeFact(id, "http.client", props));
        return id;
    }

    private static void AddFactEdge(FactWriter facts, string fromId, string toId, string kind, Dictionary<string, object?> props)
    {
        facts.AddEdge(new EdgeFact(fromId, toId, kind, props));
    }

    private static Dictionary<string, object?> EdgeProps(params (string Key, object? Value)[] kvps)
    {
        var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        foreach (var (key, value) in kvps)
        {
            if (value is null)
            {
                continue;
            }

            dict[key] = value;
        }

        return dict;
    }

    private string? ResolveAbsolutePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        var combined = Path.IsPathRooted(path)
            ? path
            : Path.Combine(_workspaceRoot, path);
        return Path.GetFullPath(combined);
    }

    private void AddSource(Dictionary<string, object?> props, string? relativePath, int line)
    {
        var absolute = ResolveAbsolutePath(relativePath);
        props.AddSource(absolute, line);
    }

    private void AddSource(Dictionary<string, object?> props, string? relativePath, GraphSpan? span)
    {
        var absolute = ResolveAbsolutePath(relativePath);
        props.AddSource(absolute, span);
    }

    private string? ResolveResponseType(RequestInfo? requestInfo, HandlerInfo? handler, string requestType)
    {
        if (!string.IsNullOrWhiteSpace(requestInfo?.ResponseType) && !IsGenericPlaceholder(requestInfo.ResponseType))
        {
            return requestInfo.ResponseType;
        }

        if (handler is null)
        {
            return null;
        }

        var signature = handler.RequestSignatures.FirstOrDefault(sig =>
            sig.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
            sig.RequestType.Equals(requestInfo?.Fqdn, StringComparison.OrdinalIgnoreCase));

        var candidate = signature?.ResponseType;
        if (string.IsNullOrWhiteSpace(candidate) || IsGenericPlaceholder(candidate))
        {
            candidate = handler.ResponseType;
        }

        if (!string.IsNullOrWhiteSpace(candidate) && !IsGenericPlaceholder(candidate))
        {
            return candidate;
        }

        return null;
    }

    private void RecordControllerRequestFact(ControllerActionInfo action, string requestType, string? invocationName, int line)
    {
        var controllerId = EnsureControllerFactNode(action);
        var requestInfo = FindRequestByType(requestType, preferredAssembly: action.Assembly, preferredProject: action.Project);
        var handler = FindHandlerForRequest(requestType);
        var requestId = EnsureRequestFactNode(requestType, requestInfo, action.Assembly, action.Project);
        var responseType = ResolveResponseType(requestInfo, handler, requestType);
        var edgeProps = EdgeProps(
            ("request_type", requestInfo?.Fqdn ?? requestType),
            ("response_type", responseType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        if (!string.IsNullOrWhiteSpace(invocationName))
        {
            edgeProps["invocation"] = invocationName;
        }
        AddSource(edgeProps, action.FilePath, line);
        AddFactEdge(_facts, controllerId, requestId, "sends_request", edgeProps);

        if (handler is not null)
        {
            var handlerId = EnsureHandlerFactNode(handler);
            var handledProps = EdgeProps(
                ("request_type", requestInfo?.Fqdn ?? requestType),
                ("response_type", responseType),
                ("line", line),
                ("provenance", "Interprocedural"),
                ("confidence", "High"));

            if (!string.IsNullOrWhiteSpace(invocationName))
            {
                handledProps["invocation"] = invocationName;
            }
            AddSource(handledProps, action.FilePath, line);
            AddFactEdge(_facts, controllerId, handlerId, "handled_by", handledProps);
        }
    }

    private void RecordControllerNotificationFact(ControllerActionInfo action, string notificationType, int line)
    {
        var notificationInfo = FindNotificationByType(notificationType);
        if (notificationInfo is null)
        {
            return;
        }

        var controllerId = EnsureControllerFactNode(action);
        var notificationId = EnsureNotificationFactNode(notificationInfo);
        var props = EdgeProps(
            ("notification_type", notificationInfo.Fqdn),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, action.FilePath, line);
        AddFactEdge(_facts, controllerId, notificationId, "publishes_notification", props);
    }

    private void RecordControllerMappingFact(ControllerActionInfo action, string? sourceType, string destinationType, string? variable, int line)
    {
        var controllerId = EnsureControllerFactNode(action);
        if (!TryResolveNodeReference(destinationType, out var destination, action.Assembly, action.Project))
        {
            return;
        }

        var destinationProps = new Dictionary<string, object?>
        {
            ["type"] = destinationType,
            ["file_path"] = destination.FilePath
        };

        AddSource(destinationProps, destination.FilePath, destination.Span);
        _facts.AddNode(new NodeFact(destination.Id, "mapping.destination", destinationProps));

        var props = EdgeProps(
            ("source_type", sourceType),
            ("destination_type", destinationType),
            ("variable", variable),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, action.FilePath, line);
        AddFactEdge(_facts, controllerId, destination.Id, "maps_to", props);
    }

    private void RecordControllerHttpClientFact(ControllerActionInfo action, string clientType, string? verb, string? route, string methodName, int line)
    {
        var controllerId = EnsureControllerFactNode(action);
        var clientId = EnsureHttpClientFactNode(clientType, verb, route);
        var props = EdgeProps(
            ("verb", verb),
            ("route", route),
            ("method", methodName),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", string.IsNullOrWhiteSpace(route) ? "Medium" : "High"));

        if (!string.IsNullOrWhiteSpace(methodName))
        {
            props["client_method"] = methodName;
        }
        var targetService = ResolveClientTargetService(clientType);
        if (!string.IsNullOrWhiteSpace(targetService))
        {
            props["target_service"] = targetService!;
        }
        AddSource(props, action.FilePath, line);
        AddFactEdge(_facts, controllerId, clientId, "uses_client", props);
    }

    private string EnsureRepositoryFactNode(RepositoryInfo repository)
    {
        var id = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = repository.Name,
            ["fqdn"] = repository.Fqdn,
            ["assembly"] = repository.Assembly,
            ["project"] = repository.Project,
            ["symbol_id"] = repository.SymbolId
        };
        AddSource(props, repository.FilePath, repository.Span);
        _facts.AddNode(new NodeFact(id, "app.repository", props));
        return id;
    }

    private string EnsureEntityFactNode(EntityInfo entity)
    {
        var id = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = entity.Name,
            ["fqdn"] = entity.Fqdn,
            ["assembly"] = entity.Assembly,
            ["project"] = entity.Project,
            ["table"] = entity.TableName,
            ["symbol_id"] = entity.SymbolId
        };
        AddSource(props, entity.FilePath, entity.Span);
        _facts.AddNode(new NodeFact(id, "ef.entity", props));
        return id;
    }

    private void RecordHandlerRepositoryFact(HandlerInfo handler, string repositoryType, string method, string operation, int line)
    {
        var handlerId = EnsureHandlerFactNode(handler);
        var targetType = ResolveImplementationType(repositoryType) ?? repositoryType;
        RepositoryInfo? repository = null;
        if (!string.IsNullOrWhiteSpace(targetType))
        {
            repository = _repositories.Values.FirstOrDefault(r =>
                r.Fqdn.Equals(targetType, StringComparison.OrdinalIgnoreCase) ||
                r.Name.Equals(GetTopLevelSimpleIdentifier(targetType), StringComparison.OrdinalIgnoreCase));
        }

        if (repository is null)
        {
            return;
        }

        var repositoryId = EnsureRepositoryFactNode(repository);
        var props = EdgeProps(
            ("method", method),
            ("operation", operation),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, repositoryId, "calls", props);
    }

    private void RecordHandlerNotificationFact(HandlerInfo handler, string notificationType, int line)
    {
        var notificationInfo = FindNotificationByType(notificationType);
        if (notificationInfo is null)
        {
            return;
        }

        var handlerId = EnsureHandlerFactNode(handler);
        var notificationId = EnsureNotificationFactNode(notificationInfo);
        var props = EdgeProps(
            ("notification_type", notificationInfo.Fqdn),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, notificationId, "publishes_notification", props);
    }

    private void RecordHandlerMappingFact(HandlerInfo handler, string? sourceType, string destinationType, int line)
    {
        var handlerId = EnsureHandlerFactNode(handler);
        if (!TryResolveNodeReference(destinationType, out var destination, handler.Assembly, handler.Project))
        {
            return;
        }

        var destinationProps = new Dictionary<string, object?>
        {
            ["type"] = destinationType,
            ["file_path"] = destination.FilePath
        };

        AddSource(destinationProps, destination.FilePath, destination.Span);
        _facts.AddNode(new NodeFact(destination.Id, "mapping.destination", destinationProps));

        var props = EdgeProps(
            ("source_type", sourceType),
            ("destination_type", destinationType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, destination.Id, "maps_to", props);
    }

    private void RecordHandlerHttpClientFact(HandlerInfo handler, string clientType, string? verb, string? route, string? methodName, int line, string? ownerMethod)
    {
        var handlerId = EnsureHandlerFactNode(handler);
        var clientId = EnsureHttpClientFactNode(clientType, verb, route);
        var props = EdgeProps(
            ("verb", verb),
            ("route", route),
            ("method", methodName),
            ("owner_method", ownerMethod),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", string.IsNullOrWhiteSpace(route) ? "Medium" : "High"));

        if (!string.IsNullOrWhiteSpace(methodName))
        {
            props["client_method"] = methodName;
        }
        var targetService = ResolveClientTargetService(clientType);
        if (!string.IsNullOrWhiteSpace(targetService))
        {
            props["target_service"] = targetService!;
        }
        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, clientId, "uses_client", props);
    }

    private void RecordHandlerEfAccessFact(HandlerInfo handler, string contextType, string entityName, string operation, int line)
    {
        var handlerId = EnsureHandlerFactNode(handler);

        var entity = _entities.Values.FirstOrDefault(e =>
            e.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase) ||
            entityName.StartsWith(e.Name, StringComparison.OrdinalIgnoreCase));

        if (entity is null)
        {
            return;
        }

        var entityId = EnsureEntityFactNode(entity);
        var props = EdgeProps(
            ("context", contextType),
            ("entity", entityName),
            ("operation", operation),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, entityId, "queries", props);
    }

    private void RecordHttpClientOutboundCallFact(HttpClientInfo client, HttpClientCall call)
    {
        EnsureHttpClientFactNode(client.Fqdn, call.HttpMethod, call.Route);
    }

    private string EnsurePublisherFactNode(string publisherType, string? assemblyHint, string? projectHint)
    {
        PublisherInfo? publisher = null;
        if (_publishers.TryGetValue(publisherType, out var direct))
        {
            publisher = direct;
        }
        else
        {
            var simple = GetTopLevelSimpleIdentifier(publisherType);
            publisher = _publishers.Values.FirstOrDefault(p =>
                p.Fqdn.Equals(publisherType, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrWhiteSpace(simple) && p.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)));
        }

        if (publisher is not null)
        {
            var id = StableId.For("message.publisher", publisher.Fqdn, publisher.Assembly, publisher.SymbolId);
            var props = new Dictionary<string, object?>
            {
                ["name"] = publisher.Name,
                ["fqdn"] = publisher.Fqdn,
                ["assembly"] = publisher.Assembly,
                ["project"] = publisher.Project,
                ["symbol_id"] = publisher.SymbolId
            };
            AddSource(props, publisher.FilePath, publisher.Span);
            _facts.AddNode(new NodeFact(id, "message.publisher", props));
            return id;
        }

        var assembly = assemblyHint ?? GuessAssemblyName(publisherType);
        var symbolId = $"T:{publisherType}";
        var fallbackId = StableId.For("message.publisher", publisherType, assembly, symbolId);
        var fallbackProps = new Dictionary<string, object?>
        {
            ["fqdn"] = publisherType,
            ["assembly"] = assembly,
            ["project"] = projectHint,
            ["symbol_id"] = symbolId
        };
        _facts.AddNode(new NodeFact(fallbackId, "message.publisher", fallbackProps));
        return fallbackId;
    }

    private string EnsureMessageContractFactNode(string messageType)
    {
        var contract = ResolveMessageContract(messageType);
        var id = StableId.For("message.contract", contract.Fqdn, contract.Assembly, contract.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = contract.Name,
            ["fqdn"] = contract.Fqdn,
            ["assembly"] = contract.Assembly,
            ["project"] = contract.Project,
            ["symbol_id"] = contract.SymbolId
        };
        AddSource(props, contract.FilePath, contract.Span);
        _facts.AddNode(new NodeFact(id, "message.contract", props));
        return id;
    }

    private void RecordPublisherInvocationFact(string publisherType, string methodName, string? messageType, int line, string ownerMethod, string assemblyHint, string projectHint)
    {
        EnsurePublisherFactNode(publisherType, assemblyHint, projectHint);
        if (!string.IsNullOrWhiteSpace(messageType))
        {
            EnsureMessageContractFactNode(messageType);
        }
    }

    private string EnsureNotificationHandlerFactNode(NotificationHandlerInfo handler)
    {
        var id = StableId.For("cqrs.notification_handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = handler.Name,
            ["fqdn"] = handler.Fqdn,
            ["assembly"] = handler.Assembly,
            ["project"] = handler.Project,
            ["notification_type"] = handler.NotificationType,
            ["symbol_id"] = handler.SymbolId
        };
        AddSource(props, handler.FilePath, handler.Span);
        _facts.AddNode(new NodeFact(id, "cqrs.notification_handler", props));
        return id;
    }

    private void RecordNotificationHandlerRequestFact(NotificationHandlerInfo handler, string requestType, int line)
    {
        var handlerId = EnsureNotificationHandlerFactNode(handler);
        var requestInfo = FindRequestByType(requestType, handler.Assembly, handler.Project);
        var downstream = FindHandlerForRequest(requestType);
        var requestId = EnsureRequestFactNode(requestType, requestInfo, handler.Assembly, handler.Project);
        var responseType = ResolveResponseType(requestInfo, downstream, requestType);

        var props = EdgeProps(
            ("request_type", requestInfo?.Fqdn ?? requestType),
            ("response_type", responseType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, requestId, "sends_request", props);

        if (downstream is not null)
        {
            var downstreamId = EnsureHandlerFactNode(downstream);
            var handledProps = EdgeProps(
                ("request_type", requestInfo?.Fqdn ?? requestType),
                ("response_type", responseType),
                ("line", line),
                ("provenance", "Interprocedural"),
                ("confidence", "High"));
            AddSource(handledProps, handler.FilePath, line);
            AddFactEdge(_facts, handlerId, downstreamId, "handled_by", handledProps);
        }
    }

    private void RecordNotificationHandlerPublishFact(NotificationHandlerInfo handler, string notificationType, int line)
    {
        var handlerId = EnsureNotificationHandlerFactNode(handler);
        var notification = FindNotificationByType(notificationType);
        if (notification is null)
        {
            return;
        }

        var notificationId = EnsureNotificationFactNode(notification);
        var props = EdgeProps(
            ("notification_type", notification.Fqdn),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, notificationId, "publishes_notification", props);
    }

    private void RecordNotificationHandlerMappingFact(NotificationHandlerInfo handler, string? sourceType, string destinationType, int line)
    {
        var handlerId = EnsureNotificationHandlerFactNode(handler);
        if (!TryResolveNodeReference(destinationType, out var destination, handler.Assembly, handler.Project))
        {
            return;
        }

        var destinationProps = new Dictionary<string, object?>
        {
            ["type"] = destinationType,
            ["file_path"] = destination.FilePath
        };

        AddSource(destinationProps, destination.FilePath, destination.Span);
        _facts.AddNode(new NodeFact(destination.Id, "mapping.destination", destinationProps));

        var props = EdgeProps(
            ("source_type", sourceType),
            ("destination_type", destinationType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, destination.Id, "maps_to", props);
    }

    private void RecordNotificationHandlerRepositoryFact(NotificationHandlerInfo handler, string repositoryType, string methodName, string operation, int line)
    {
        var handlerId = EnsureNotificationHandlerFactNode(handler);
        var targetType = ResolveImplementationType(repositoryType) ?? repositoryType;
        RepositoryInfo? repository = null;
        if (!string.IsNullOrWhiteSpace(targetType))
        {
            repository = _repositories.Values.FirstOrDefault(r =>
                r.Fqdn.Equals(targetType, StringComparison.OrdinalIgnoreCase) ||
                r.Name.Equals(GetTopLevelSimpleIdentifier(targetType), StringComparison.OrdinalIgnoreCase));
        }

        if (repository is null)
        {
            return;
        }

        var repositoryId = EnsureRepositoryFactNode(repository);
        var props = EdgeProps(
            ("method", methodName),
            ("operation", operation),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, repositoryId, "calls", props);
    }

    private string EnsureDomainEventHandlerFactNode(DomainEventHandlerInfo handler)
    {
        var id = StableId.For("domain.event_handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
        var props = new Dictionary<string, object?>
        {
            ["name"] = handler.Name,
            ["fqdn"] = handler.Fqdn,
            ["assembly"] = handler.Assembly,
            ["project"] = handler.Project,
            ["event_type"] = handler.EventType,
            ["symbol_id"] = handler.SymbolId
        };
        AddSource(props, handler.FilePath, handler.Span);
        _facts.AddNode(new NodeFact(id, "domain.event_handler", props));
        return id;
    }

    private void RecordDomainEventHandlerRequestFact(DomainEventHandlerInfo handler, string requestType, int line)
    {
        var handlerId = EnsureDomainEventHandlerFactNode(handler);
        var requestInfo = FindRequestByType(requestType, handler.Assembly, handler.Project);
        var downstream = FindHandlerForRequest(requestType);
        var requestId = EnsureRequestFactNode(requestType, requestInfo, handler.Assembly, handler.Project);
        var responseType = ResolveResponseType(requestInfo, downstream, requestType);

        var props = EdgeProps(
            ("request_type", requestInfo?.Fqdn ?? requestType),
            ("response_type", responseType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, requestId, "sends_request", props);

        if (downstream is not null)
        {
            var downstreamId = EnsureHandlerFactNode(downstream);
            var handledProps = EdgeProps(
                ("request_type", requestInfo?.Fqdn ?? requestType),
                ("response_type", responseType),
                ("line", line),
                ("provenance", "Interprocedural"),
                ("confidence", "High"));
            AddSource(handledProps, handler.FilePath, line);
            AddFactEdge(_facts, handlerId, downstreamId, "handled_by", handledProps);
        }
    }

    private void RecordDomainEventHandlerPublishFact(DomainEventHandlerInfo handler, string notificationType, int line)
    {
        var handlerId = EnsureDomainEventHandlerFactNode(handler);
        var notification = FindNotificationByType(notificationType);
        if (notification is null)
        {
            return;
        }

        var notificationId = EnsureNotificationFactNode(notification);
        var props = EdgeProps(
            ("notification_type", notification.Fqdn),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, notificationId, "publishes_notification", props);
    }

    private void RecordDomainEventHandlerMappingFact(DomainEventHandlerInfo handler, string? sourceType, string destinationType, int line)
    {
        var handlerId = EnsureDomainEventHandlerFactNode(handler);
        if (!TryResolveNodeReference(destinationType, out var destination, handler.Assembly, handler.Project))
        {
            return;
        }

        var destinationProps = new Dictionary<string, object?>
        {
            ["type"] = destinationType,
            ["file_path"] = destination.FilePath
        };

        AddSource(destinationProps, destination.FilePath, destination.Span);
        _facts.AddNode(new NodeFact(destination.Id, "mapping.destination", destinationProps));

        var props = EdgeProps(
            ("source_type", sourceType),
            ("destination_type", destinationType),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, destination.Id, "maps_to", props);
    }

    private void RecordDomainEventHandlerRepositoryFact(DomainEventHandlerInfo handler, string repositoryType, string methodName, string operation, int line)
    {
        var handlerId = EnsureDomainEventHandlerFactNode(handler);
        var targetType = ResolveImplementationType(repositoryType) ?? repositoryType;
        RepositoryInfo? repository = null;
        if (!string.IsNullOrWhiteSpace(targetType))
        {
            repository = _repositories.Values.FirstOrDefault(r =>
                r.Fqdn.Equals(targetType, StringComparison.OrdinalIgnoreCase) ||
                r.Name.Equals(GetTopLevelSimpleIdentifier(targetType), StringComparison.OrdinalIgnoreCase));
        }

        if (repository is null)
        {
            return;
        }

        var repositoryId = EnsureRepositoryFactNode(repository);
        var props = EdgeProps(
            ("method", methodName),
            ("operation", operation),
            ("line", line),
            ("provenance", "Interprocedural"),
            ("confidence", "High"));

        AddSource(props, handler.FilePath, line);
        AddFactEdge(_facts, handlerId, repositoryId, "calls", props);
    }

    private static string GetControllerDisplay(ControllerActionInfo action)
    {
        if (string.IsNullOrWhiteSpace(action.Fqdn))
        {
            return action.Name;
        }

        var index = action.Fqdn.LastIndexOf('.');
        return index > 0 ? action.Fqdn[..index] : action.Fqdn;
    }

    private static string? DetectAuth(ControllerActionInfo action)
        => BuildAuthLabel(action.AllowsAnonymous, action.Authorizations);

    private static string? DetectAuth(MinimalEndpointInfo endpoint)
        => BuildAuthLabel(endpoint.AllowsAnonymous, endpoint.Authorizations);
}
