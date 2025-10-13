using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        if (_notifications.TryGetValue(typeName, out var notification))
        {
            var id = StableId.For("cqrs.notification", notification.Fqdn, notification.Assembly, notification.SymbolId);
            reference = new NodeReference(id, notification.FilePath, notification.Span);
            return true;
        }

        if (_notifications.Values.FirstOrDefault(n => n.Name.Equals(simple, StringComparison.Ordinal)) is { } notificationFallback)
        {
            var id = StableId.For("cqrs.notification", notificationFallback.Fqdn, notificationFallback.Assembly, notificationFallback.SymbolId);
            reference = new NodeReference(id, notificationFallback.FilePath, notificationFallback.Span);
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

    private NotificationInfo? FindNotificationByType(string notificationType)
    {
        if (_notifications.TryGetValue(notificationType, out var notification))
        {
            return notification;
        }

        var simple = notificationType.Split('.').Last();

        return _notifications.Values.FirstOrDefault(n =>
            n.Fqdn.Equals(notificationType, StringComparison.OrdinalIgnoreCase) ||
            n.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));
    }

    private string? ResolveImplementationType(string typeName)
    {
        var registration = FindServiceRegistration(typeName);
        return registration?.ImplementationType;
    }

    private static string? TryResolveExpressionType(ExpressionSyntax expression, IReadOnlyDictionary<string, string?> parameterTypes, Dictionary<string, string> localVariables)
    {
        if (expression is IdentifierNameSyntax identifier)
        {
            if (localVariables.TryGetValue(identifier.Identifier.Text, out var localType) && !string.Equals(localType, "var", StringComparison.OrdinalIgnoreCase))
            {
                return localType;
            }

            if (parameterTypes.TryGetValue(identifier.Identifier.Text, out var parameterType) && !string.IsNullOrWhiteSpace(parameterType))
            {
                return parameterType;
            }
        }

        if (expression is ObjectCreationExpressionSyntax creation)
        {
            return creation.Type.ToString();
        }

        return null;
    }

    private static bool IsOptionsDeclaration(TypeDeclarationSyntax declaration)
    {
        if (declaration.Identifier.Text.EndsWith("Options", StringComparison.Ordinal))
        {
            return true;
        }

        return declaration.Members.OfType<FieldDeclarationSyntax>().Any(field =>
            field.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword)) &&
            field.Declaration.Type.ToString().Equals("string", StringComparison.OrdinalIgnoreCase) &&
            field.Declaration.Variables.Any(variable =>
                variable.Identifier.Text.Equals("SectionName", StringComparison.OrdinalIgnoreCase)));
    }
}
