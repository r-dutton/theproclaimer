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
    private static GenericNameSyntax? MatchGenericInterface(TypeSyntax typeSyntax, params string[] interfaceNames)
    {
        if (typeSyntax is GenericNameSyntax generic)
        {
            foreach (var name in interfaceNames)
            {
                if (generic.Identifier.Text.Equals(name, StringComparison.Ordinal))
                {
                    return generic;
                }
            }

            return null;
        }

        if (typeSyntax is QualifiedNameSyntax qualified)
        {
            return MatchGenericInterface(qualified.Right, interfaceNames);
        }

        if (typeSyntax is AliasQualifiedNameSyntax alias)
        {
            return MatchGenericInterface(alias.Name, interfaceNames);
        }

        return null;
    }

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

        if (_dbContexts.TryGetValue(typeName, out var dbContext))
        {
            var id = StableId.For("ef.db_context", dbContext.Fqdn, dbContext.Assembly, dbContext.SymbolId);
            reference = new NodeReference(id, dbContext.FilePath, dbContext.Span);
            return true;
        }

        if (_dbContexts.Values.FirstOrDefault(c =>
                c.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)) is { } contextFallback)
        {
            var id = StableId.For("ef.db_context", contextFallback.Fqdn, contextFallback.Assembly, contextFallback.SymbolId);
            reference = new NodeReference(id, contextFallback.FilePath, contextFallback.Span);
            return true;
        }

        if (_backgroundServices.TryGetValue(typeName, out var backgroundService))
        {
            var id = StableId.For("app.background_service", backgroundService.Fqdn, backgroundService.Assembly, backgroundService.SymbolId);
            reference = new NodeReference(id, backgroundService.FilePath, backgroundService.Span);
            return true;
        }

        if (_backgroundServices.Values.FirstOrDefault(s =>
                s.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)) is { } backgroundFallback)
        {
            var id = StableId.For("app.background_service", backgroundFallback.Fqdn, backgroundFallback.Assembly, backgroundFallback.SymbolId);
            reference = new NodeReference(id, backgroundFallback.FilePath, backgroundFallback.Span);
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

        if (_pipelineBehaviors.TryGetValue(typeName, out var pipeline))
        {
            var id = StableId.For("cqrs.pipeline_behavior", pipeline.Fqdn, pipeline.Assembly, pipeline.SymbolId);
            reference = new NodeReference(id, pipeline.FilePath, pipeline.Span);
            return true;
        }

        if (_pipelineBehaviors.Values.FirstOrDefault(p => p.Name.Equals(simple, StringComparison.Ordinal)) is { } pipelineFallback)
        {
            var id = StableId.For("cqrs.pipeline_behavior", pipelineFallback.Fqdn, pipelineFallback.Assembly, pipelineFallback.SymbolId);
            reference = new NodeReference(id, pipelineFallback.FilePath, pipelineFallback.Span);
            return true;
        }

        if (_requestProcessors.TryGetValue(typeName, out var processor))
        {
            var id = StableId.For("cqrs.request_processor", processor.Fqdn, processor.Assembly, processor.SymbolId);
            reference = new NodeReference(id, processor.FilePath, processor.Span);
            return true;
        }

        if (_requestProcessors.Values.FirstOrDefault(p => p.Name.Equals(simple, StringComparison.Ordinal)) is { } processorFallback)
        {
            var id = StableId.For("cqrs.request_processor", processorFallback.Fqdn, processorFallback.Assembly, processorFallback.SymbolId);
            reference = new NodeReference(id, processorFallback.FilePath, processorFallback.Span);
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

    private string? TryResolveProjectionSource(ExpressionSyntax expression, IReadOnlyDictionary<string, string?> parameterTypes, Dictionary<string, string> localVariables, IReadOnlyDictionary<string, FieldDescriptor> fieldLookup)
    {
        var resolved = TryResolveExpressionType(expression, parameterTypes, localVariables);
        if (!string.IsNullOrWhiteSpace(resolved))
        {
            return ExtractInnermostGenericType(resolved);
        }

        if (expression is IdentifierNameSyntax identifier)
        {
            var identifierName = identifier.Identifier.Text.TrimStart('_');
            if (fieldLookup.TryGetValue(identifierName, out var descriptor))
            {
                return ExtractInnermostGenericType(descriptor.Type) ?? descriptor.Type;
            }
        }

        if (expression is MemberAccessExpressionSyntax member)
        {
            if (member.Expression is IdentifierNameSyntax rootIdentifier)
            {
                var rootName = rootIdentifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(rootName, out var descriptor) && descriptor.Type.Contains("DbContext", StringComparison.Ordinal))
                {
                    var propertyName = member.Name.Identifier.Text;
                    var entity = _entities.Values.FirstOrDefault(e => e.DbSetProperties.Any(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)));
                    if (entity is not null)
                    {
                        return entity.Fqdn;
                    }
                }
            }

            var nested = TryResolveProjectionSource(member.Expression, parameterTypes, localVariables, fieldLookup);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                return nested;
            }
        }

        if (expression is InvocationExpressionSyntax invocation && invocation.Expression is MemberAccessExpressionSyntax invocationAccess)
        {
            var nested = TryResolveProjectionSource(invocationAccess.Expression, parameterTypes, localVariables, fieldLookup);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                return nested;
            }
        }

        return null;
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

    private static string? ExtractInnermostGenericType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var current = typeName;
        while (true)
        {
            var generic = ExtractGenericArgument(current);
            if (string.IsNullOrWhiteSpace(generic))
            {
                return current;
            }

            var separator = generic.IndexOf(',');
            if (separator >= 0)
            {
                generic = generic[..separator];
            }

            current = generic;
        }
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
