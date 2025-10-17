using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        if (_services.TryGetValue(typeName, out var service))
        {
            var id = StableId.For("app.service", service.Fqdn, service.Assembly, service.SymbolId);
            reference = new NodeReference(id, service.FilePath, service.Span);
            return true;
        }

        if (_services.Values.FirstOrDefault(s =>
                s.Fqdn.Equals(typeName, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)) is { } serviceFallback)
        {
            var id = StableId.For("app.service", serviceFallback.Fqdn, serviceFallback.Assembly, serviceFallback.SymbolId);
            reference = new NodeReference(id, serviceFallback.FilePath, serviceFallback.Span);
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

        var matches = _handlers.Values
            .Where(h =>
                h.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
                h.RequestType.Split('.').Last().Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private RequestInfo? FindRequestByType(string requestType)
    {
        if (_requests.TryGetValue(requestType, out var request))
        {
            return request;
        }

        var simple = requestType.Split('.').Last();

        var matches = _requests.Values
            .Where(r =>
                r.Fqdn.Equals(requestType, StringComparison.OrdinalIgnoreCase) ||
                r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (matches.Count == 1)
        {
            return matches[0];
        }

        // Heuristic fallback: if multiple matches share the same simple name (common when the same
        // request type exists in different namespaces), attempt to disambiguate by preferring the
        // one whose namespace shares the longest common prefix with the incoming requestType.
        if (matches.Count > 1 && !string.IsNullOrWhiteSpace(requestType))
        {
            var requestNamespace = requestType.Contains('.') ? requestType[..requestType.LastIndexOf('.')] : string.Empty;
            if (!string.IsNullOrWhiteSpace(requestNamespace))
            {
                var ordered = matches
                    .Select(r => new { Item = r, Score = LongestCommonPrefixLength(requestNamespace, r.Fqdn) })
                    .OrderByDescending(x => x.Score)
                    .ThenBy(x => x.Item.Fqdn, StringComparer.OrdinalIgnoreCase)
                    .ToList();
                if (ordered.Count > 0 && ordered[0].Score > 0)
                {
                    // Additional tie-break: if top two have equal score, abandon to avoid incorrect binding.
                    if (ordered.Count == 1 || ordered[0].Score > ordered[1].Score)
                    {
                        return ordered[0].Item;
                    }
                }
            }
        }

        return null;
    }

    private PipelineBehaviorInfo? FindPipelineBehavior(string behaviorType)
    {
        if (string.IsNullOrWhiteSpace(behaviorType))
        {
            return null;
        }

        if (_pipelineBehaviors.TryGetValue(behaviorType, out var info))
        {
            return info;
        }

        var baseType = GetTypeNameWithoutGenerics(behaviorType);
        if (!string.Equals(baseType, behaviorType, StringComparison.Ordinal) &&
            _pipelineBehaviors.TryGetValue(baseType, out info))
        {
            return info;
        }

        var simple = baseType.Split('.').Last();
        var matches = _pipelineBehaviors.Values
            .Where(b =>
                b.Fqdn.Equals(behaviorType, StringComparison.OrdinalIgnoreCase) ||
                b.Fqdn.Equals(baseType, StringComparison.OrdinalIgnoreCase) ||
                b.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private RequestProcessorInfo? FindRequestProcessor(string processorType)
    {
        if (string.IsNullOrWhiteSpace(processorType))
        {
            return null;
        }

        if (_requestProcessors.TryGetValue(processorType, out var info))
        {
            return info;
        }

        var baseType = GetTypeNameWithoutGenerics(processorType);
        if (!string.Equals(baseType, processorType, StringComparison.Ordinal) &&
            _requestProcessors.TryGetValue(baseType, out info))
        {
            return info;
        }

        var simple = baseType.Split('.').Last();
        var matches = _requestProcessors.Values
            .Where(p =>
                p.Fqdn.Equals(processorType, StringComparison.OrdinalIgnoreCase) ||
                p.Fqdn.Equals(baseType, StringComparison.OrdinalIgnoreCase) ||
                p.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private NotificationInfo? FindNotificationByType(string notificationType)
    {
        if (_notifications.TryGetValue(notificationType, out var notification))
        {
            return notification;
        }

        var simple = notificationType.Split('.').Last();

        var matches = _notifications.Values
            .Where(n =>
                n.Fqdn.Equals(notificationType, StringComparison.OrdinalIgnoreCase) ||
                n.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return matches.Count == 1 ? matches[0] : null;
    }

    private string? ResolveImplementationType(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var registration = FindServiceRegistration(typeName);
        if (registration is not null && !string.IsNullOrWhiteSpace(registration.ImplementationType))
        {
            return registration.ImplementationType;
        }

        if (TryResolveGenericImplementation(typeName, out var implementationType))
        {
            return implementationType;
        }

        return null;
    }

    private bool TryResolveGenericImplementation(string typeName, out string? implementationType)
    {
        implementationType = null;
        if (!TryMakeOpenGenericType(typeName, out var openServiceType, out var typeArguments) || typeArguments.Count == 0)
        {
            return false;
        }

        var registration = FindServiceRegistration(openServiceType);
        if (registration is null)
        {
            var simpleOpen = openServiceType.Split('.').Last();
            registration = FindServiceRegistration(simpleOpen);
        }

        if (registration is null || string.IsNullOrWhiteSpace(registration.ImplementationType))
        {
            return false;
        }

        var implementationOpenType = registration.ImplementationType;
        if (!TryMakeOpenGenericType(implementationOpenType, out var implementationOpen, out _))
        {
            implementationType = registration.ImplementationType;
            return true;
        }

        implementationType = CloseGenericType(implementationOpen, typeArguments);
        return true;
    }

    private static bool TryMakeOpenGenericType(string typeName, out string openType, out IReadOnlyList<string> typeArguments)
    {
        typeArguments = SplitGenericArguments(typeName);
        if (typeArguments.Count == 0)
        {
            openType = typeName;
            return false;
        }

        var genericStart = typeName.IndexOf('<');
        var genericEnd = typeName.LastIndexOf('>');
        if (genericStart < 0 || genericEnd <= genericStart)
        {
            openType = typeName;
            return false;
        }

        var builder = new StringBuilder();
        builder.Append(typeName[..genericStart]);
        builder.Append('<');
        for (var i = 0; i < typeArguments.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }
        }
        builder.Append('>');
        openType = builder.ToString();
        return true;
    }

    private static string CloseGenericType(string openType, IReadOnlyList<string> typeArguments)
    {
        if (!openType.Contains('<') || typeArguments.Count == 0)
        {
            return openType;
        }

        var prefix = openType[..openType.IndexOf('<')];
        var builder = new StringBuilder(prefix);
        builder.Append('<');
        for (var i = 0; i < typeArguments.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }

            builder.Append(typeArguments[i]);
        }
        builder.Append('>');
        return builder.ToString();
    }

    private string NormalizeServiceType(string serviceType)
    {
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return serviceType;
        }

        if (_services.TryGetValue(serviceType, out var serviceInfo))
        {
            return serviceInfo.Fqdn;
        }

        var simple = serviceType.Split('.').Last();
        var match = _services.Values.FirstOrDefault(s =>
            s.Fqdn.Equals(serviceType, StringComparison.OrdinalIgnoreCase) ||
            s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));
        if (match is not null)
        {
            return match.Fqdn;
        }

        var registration = FindServiceRegistration(serviceType) ?? FindServiceRegistration(simple);
        if (registration is not null)
        {
            var implementation = registration.ImplementationType;
            if (!string.IsNullOrWhiteSpace(implementation) &&
                !string.Equals(implementation, serviceType, StringComparison.OrdinalIgnoreCase))
            {
                return NormalizeServiceType(implementation);
            }
        }

        return serviceType;
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

    private string? TryResolveExpressionType(ExpressionSyntax expression, IReadOnlyDictionary<string, string?> parameterTypes, Dictionary<string, string> localVariables)
    {
        if (expression is IdentifierNameSyntax identifier)
        {
            if (localVariables.TryGetValue(identifier.Identifier.Text, out var localType) && !string.Equals(localType, "var", StringComparison.OrdinalIgnoreCase))
            {
                return QualifyTypeName(localType);
            }

            if (parameterTypes.TryGetValue(identifier.Identifier.Text, out var parameterType) && !string.IsNullOrWhiteSpace(parameterType))
            {
                return QualifyTypeName(parameterType);
            }
        }

        if (expression is ObjectCreationExpressionSyntax creation)
        {
            return QualifyTypeName(creation.Type.ToString());
        }

        return null;
    }

    private string QualifyTypeName(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return typeName;
        }

        if (_requests.ContainsKey(typeName) || _handlers.ContainsKey(typeName) || _notifications.ContainsKey(typeName))
        {
            return typeName;
        }

        var simple = typeName.Split('.').Last();

        var requestMatches = _requests.Values
            .Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (requestMatches.Count == 1)
        {
            return requestMatches[0].Fqdn;
        }

        var handlerMatches = _handlers.Values
            .Where(h => h.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (handlerMatches.Count == 1)
        {
            return handlerMatches[0].RequestType;
        }

        return typeName;
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

    private static IReadOnlyList<string> SplitGenericArguments(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return Array.Empty<string>();
        }

        var start = typeName.IndexOf('<');
        var end = typeName.LastIndexOf('>');
        if (start < 0 || end <= start)
        {
            return Array.Empty<string>();
        }

        var inner = typeName.Substring(start + 1, end - start - 1);
        var arguments = new List<string>();
        var depth = 0;
        var current = new List<char>();
        foreach (var ch in inner)
        {
            if (ch == '<')
            {
                depth++;
                current.Add(ch);
                continue;
            }

            if (ch == '>')
            {
                depth--;
                current.Add(ch);
                continue;
            }

            if (ch == ',' && depth == 0)
            {
                var value = new string(current.ToArray()).Trim();
                if (value.Length > 0)
                {
                    arguments.Add(value);
                }
                current.Clear();
                continue;
            }

            current.Add(ch);
        }

        var last = new string(current.ToArray()).Trim();
        if (last.Length > 0)
        {
            arguments.Add(last);
        }

        return arguments;
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

    private static int LongestCommonPrefixLength(string a, string b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
        {
            return 0;
        }

        var max = Math.Min(a.Length, b.Length);
        var i = 0;
        for (; i < max; i++)
        {
            if (a[i] != b[i]) break;
        }
        return i;
    }

    private static bool IsLoggerType(string? typeName)
        => !string.IsNullOrWhiteSpace(typeName) && typeName.Contains("ILogger", StringComparison.Ordinal);

    private static string? TryExtractLogLevel(string? methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        if (methodName.StartsWith("Log", StringComparison.OrdinalIgnoreCase) && methodName.Length > 3)
        {
            var level = methodName[3..];
            if (!string.IsNullOrWhiteSpace(level))
            {
                return level;
            }
        }

        return null;
    }

    private static bool IsGuardInvocation(MemberAccessExpressionSyntax access)
    {
        var expressionText = access.Expression.ToString();
        if (!string.IsNullOrWhiteSpace(expressionText) && expressionText.Contains("Guard", StringComparison.Ordinal))
        {
            return true;
        }

        var method = access.Name.Identifier.Text;
        if (string.IsNullOrWhiteSpace(method))
        {
            return false;
        }

        return method.StartsWith("Ensure", StringComparison.OrdinalIgnoreCase) ||
               method.StartsWith("Validate", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsStorageService(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var simple = GetTypeNameWithoutGenerics(typeName);
        return simple.Contains("Storage", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("Blob", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("FileStore", StringComparison.OrdinalIgnoreCase) ||
               simple.Contains("DocumentStore", StringComparison.OrdinalIgnoreCase);
    }
}


