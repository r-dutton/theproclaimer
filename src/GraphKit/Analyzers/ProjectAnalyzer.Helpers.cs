using System;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private bool TryResolveNodeReference(string typeName, out NodeReference reference)
    {
        var simple = typeName.Split('.').Last();

        if (_dtos.TryGetValue(typeName, out var dto))
        {
            var id = StableId.For("dto", dto.Fqdn, dto.Assembly, dto.SymbolId);
            reference = new NodeReference(id, dto.FilePath, dto.Span);
            return true;
        }

        if (_dtos.Values.FirstOrDefault(d => d.Name.Equals(simple, StringComparison.Ordinal)) is { } dtoFallback)
        {
            var id = StableId.For("dto", dtoFallback.Fqdn, dtoFallback.Assembly, dtoFallback.SymbolId);
            reference = new NodeReference(id, dtoFallback.FilePath, dtoFallback.Span);
            return true;
        }

        if (_requests.TryGetValue(typeName, out var request))
        {
            var id = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
            reference = new NodeReference(id, request.FilePath, request.Span);
            return true;
        }

        if (_requests.Values.FirstOrDefault(r => r.Name.Equals(simple, StringComparison.Ordinal)) is { } requestFallback)
        {
            var id = StableId.For("cqrs.request", requestFallback.Fqdn, requestFallback.Assembly, requestFallback.SymbolId);
            reference = new NodeReference(id, requestFallback.FilePath, requestFallback.Span);
            return true;
        }

        if (_entities.TryGetValue(typeName, out var entity))
        {
            var id = StableId.For("ef.entity", entity.Fqdn, entity.Assembly, entity.SymbolId);
            reference = new NodeReference(id, entity.FilePath, entity.Span);
            return true;
        }

        if (_entities.Values.FirstOrDefault(e => e.Name.Equals(simple, StringComparison.Ordinal)) is { } entityFallback)
        {
            var id = StableId.For("ef.entity", entityFallback.Fqdn, entityFallback.Assembly, entityFallback.SymbolId);
            reference = new NodeReference(id, entityFallback.FilePath, entityFallback.Span);
            return true;
        }

        if (_handlers.TryGetValue(typeName, out var handler))
        {
            var id = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            reference = new NodeReference(id, handler.FilePath, handler.Span);
            return true;
        }

        if (_handlers.Values.FirstOrDefault(h => h.Name.Equals(simple, StringComparison.Ordinal)) is { } handlerFallback)
        {
            var id = StableId.For("cqrs.handler", handlerFallback.Fqdn, handlerFallback.Assembly, handlerFallback.SymbolId);
            reference = new NodeReference(id, handlerFallback.FilePath, handlerFallback.Span);
            return true;
        }

        if (_repositories.TryGetValue(typeName, out var repository))
        {
            var id = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
            reference = new NodeReference(id, repository.FilePath, repository.Span);
            return true;
        }

        if (_repositories.Values.FirstOrDefault(r => r.Name.Equals(simple, StringComparison.Ordinal)) is { } repositoryFallback)
        {
            var id = StableId.For("app.repository", repositoryFallback.Fqdn, repositoryFallback.Assembly, repositoryFallback.SymbolId);
            reference = new NodeReference(id, repositoryFallback.FilePath, repositoryFallback.Span);
            return true;
        }

        if (ResolveImplementationType(typeName) is { } resolved && !string.Equals(resolved, typeName, StringComparison.Ordinal))
        {
            return TryResolveNodeReference(resolved, out reference);
        }

        reference = default!;
        return false;
    }

    private HandlerInfo? FindHandlerForRequest(string requestType)
    {
        if (_handlers.TryGetValue(requestType, out var handler))
        {
            return handler;
        }

        var simple = requestType.Split('.').Last();

        return _handlers.Values.FirstOrDefault(h =>
            h.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
            h.RequestType.Split('.').Last().Equals(simple, StringComparison.OrdinalIgnoreCase));
    }

    private RequestInfo? FindRequestByType(string requestType)
    {
        if (_requests.TryGetValue(requestType, out var request))
        {
            return request;
        }

        var simple = requestType.Split('.').Last();

        return _requests.Values.FirstOrDefault(r =>
            r.Fqdn.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
            r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));
    }

    private string? ResolveImplementationType(string typeName)
    {
        var registration = FindServiceRegistration(typeName);
        return registration?.ImplementationType;
    }
}
