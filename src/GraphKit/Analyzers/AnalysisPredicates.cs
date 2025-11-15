using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Analyzers;

internal static class AnalysisPredicates
{
    private static readonly string[] MediatorInterfaces =
    {
        "MediatR.IMediator",
        "MediatR.ISender",
        "MediatR.IPublisher"
    };

    private static readonly string[] MediatorHandlerInterfaces =
    {
        "MediatR.IRequestHandler",
        "MediatR.INotificationHandler",
        "MediatR.IRequestStreamHandler",
        "MediatR.IStreamRequestHandler",
        "MediatR.IPipelineBehavior"
    };

    private static readonly string[] EntityFrameworkTypes =
    {
        "Microsoft.EntityFrameworkCore.DbContext",
        "Microsoft.EntityFrameworkCore.DbSet"
    };

    private static readonly string[] HttpClientTypes =
    {
        "System.Net.Http.HttpClient",
        "System.Net.Http.IHttpClientFactory"
    };

    private static readonly string[] MapperTypes =
    {
        "AutoMapper.IMapper",
        "AutoMapper.IConfigurationProvider",
        "AutoMapper.IProjectionExpression"
    };

    private static readonly string[] ValidatorTypes =
    {
        "FluentValidation.IValidator",
        "FluentValidation.IValidatorFactory"
    };

    private static readonly string[] DomainEventPublisherHints =
    {
        "DomainEvent",
        "DomainEvents",
        "EventDispatcher",
        "EventPublisher"
    };

    public static bool IsMediatorSend(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Send", "SendAsync") &&
               (IsMediatorType(GetReceiverType(invocation)) || IsMediatorType(method.ContainingType));
    }

    public static bool IsMediatorPublish(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Publish", "PublishAsync") &&
               (IsMediatorType(GetReceiverType(invocation)) || IsMediatorType(method.ContainingType));
    }

    public static bool IsHandlerHandle(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Handle", "HandleAsync") &&
               (IsMediatorHandler(method.ContainingType) || IsMediatorHandler(GetReceiverType(invocation) as INamedTypeSymbol));
    }

    public static bool IsDbContextOrRepoCall(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        var receiver = GetReceiverType(invocation);
        return IsEntityFrameworkType(method.ContainingType) ||
               IsEntityFrameworkType(receiver) ||
               IsRepositoryType(receiver) ||
               IsRepositoryType(method.ContainingType);
    }

    public static bool IsHttpClientCall(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        var methodName = method.Name;
        if (string.Equals(methodName, "Include", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(methodName, "ThenInclude", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var definition = method.ReducedFrom ?? method;
        if (definition.IsExtensionMethod && definition.Parameters.Length > 0)
        {
            var firstParamType = definition.Parameters[0].Type;
            if (IsQueryableLikeType(firstParamType) || IsRepositoryType(firstParamType))
            {
                return false;
            }
        }

        var receiver = GetReceiverType(invocation);
        if (IsQueryableLikeType(receiver))
        {
            return false;
        }

        if (IsCachingType(receiver) || IsCachingType(method.ContainingType))
        {
            return false;
        }

        if (IsHttpClientType(receiver) || IsHttpClientType(method.ContainingType))
        {
            return true;
        }

        // Extension methods returning HttpResponseMessage or Task<HttpResponseMessage>
        var returnType = method.ReturnType;
        if (MatchesMetadataName(returnType, "System.Net.Http.HttpResponseMessage"))
        {
            return true;
        }

        if (returnType is INamedTypeSymbol namedReturn &&
            MatchesMetadataName(namedReturn.ConstructedFrom, "System.Threading.Tasks.Task"))
        {
            if (namedReturn.TypeArguments.Length == 1 &&
                MatchesMetadataName(namedReturn.TypeArguments[0], "System.Net.Http.HttpResponseMessage"))
            {
                return true;
            }
        }

        // Recognize wrapper methods that look like HTTP calls by verb naming
        if (LooksLikeHttpWrapper(method.Name))
        {
            if (IsRepositoryType(receiver) || IsRepositoryType(method.ContainingType))
            {
                return false;
            }

            var containingTypeForWrapper = (method.ReducedFrom ?? method).ContainingType;
            if (containingTypeForWrapper is not null)
            {
                var containerName = containingTypeForWrapper.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (containerName.IndexOf("Extensions", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return false;
                }
            }

            if (!IsLikelyHttpWrapperType(receiver) && !IsLikelyHttpWrapperType(method.ContainingType))
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public static bool IsCachingType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (MatchesMetadataName(type, "Microsoft.Extensions.Caching.Memory.IMemoryCache") ||
            MatchesMetadataName(type, "Microsoft.Extensions.Caching.Memory.MemoryCache") ||
            MatchesMetadataName(type, "Microsoft.Extensions.Caching.Distributed.IDistributedCache") ||
            MatchesMetadataName(type, "Microsoft.Extensions.Caching.Distributed.IDistributedCacheExtensions"))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                if (MatchesMetadataName(iface, "Microsoft.Extensions.Caching.Memory.IMemoryCache") ||
                    iface.Name.Contains("Cache", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (display.IndexOf("Caching", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return true;
        }

        if (display.IndexOf("Cache", StringComparison.OrdinalIgnoreCase) >= 0 &&
            display.IndexOf("Http", StringComparison.OrdinalIgnoreCase) < 0)
        {
            return true;
        }

        return false;
    }

    private static bool LooksLikeHttpWrapper(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        foreach (var verb in HttpWrapperVerbs)
        {
            if (name.Equals(verb, StringComparison.OrdinalIgnoreCase) ||
                name.Equals(verb + "Async", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsLikelyHttpWrapperType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (display.IndexOf("Http", StringComparison.OrdinalIgnoreCase) >= 0 ||
            display.IndexOf("Client", StringComparison.OrdinalIgnoreCase) >= 0 ||
            display.IndexOf("Proxy", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (ifaceName.IndexOf("Http", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    ifaceName.IndexOf("Client", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    ifaceName.IndexOf("Proxy", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsMapperMap(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        // Common AutoMapper APIs
        if (MatchesName(method.Name, "Map", "ProjectTo", "MapAsync") &&
            (IsMapperType(GetReceiverType(invocation)) || IsMapperType(method.ContainingType)))
        {
            return true;
        }

        // Projector-like helpers seen in some codebases
        if (string.Equals(method.Name, "ProjectByIdAsync", StringComparison.Ordinal))
        {
            return true;
        }

        return false;
    }

    private static bool IsQueryableLikeType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (MatchesMetadataName(type, "System.Linq.IQueryable"))
        {
            return true;
        }

        if (MatchesMetadataName(type, "System.Collections.Generic.IEnumerable`1"))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                if (MatchesMetadataName(iface, "System.Linq.IQueryable") ||
                    MatchesMetadataName(iface, "System.Collections.Generic.IEnumerable`1"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsValidatorCall(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Validate", "ValidateAsync") &&
               (IsValidatorType(GetReceiverType(invocation)) || IsValidatorType(method.ContainingType));
    }

    public static bool IsPipelineBehavior(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Handle") &&
               IsPipelineBehaviorType(method.ContainingType);
    }

    public static bool IsDomainEventPublish(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        if (!MatchesName(method.Name, "Publish", "PublishAsync", "Raise", "Dispatch"))
        {
            return false;
        }

        var receiver = GetReceiverType(invocation);
        if (receiver is null)
        {
            return false;
        }

        if (IsMediatorPublish(invocation))
        {
            return true;
        }

        var displayName = receiver.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        return DomainEventPublisherHints.Any(hint =>
            displayName.Contains(hint, StringComparison.OrdinalIgnoreCase));
    }

    private static bool MatchesName(string candidate, params string[] names)
    {
        foreach (var name in names)
        {
            if (string.Equals(candidate, name, StringComparison.Ordinal) ||
                string.Equals(candidate, name + "Async", StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsMediatorType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (type is INamedTypeSymbol named)
        {
            if (MediatorInterfaces.Any(meta => MatchesMetadataName(named, meta)))
            {
                return true;
            }

            foreach (var iface in named.AllInterfaces)
            {
                if (MediatorInterfaces.Any(meta => MatchesMetadataName(iface, meta)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsMediatorHandler(INamedTypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        foreach (var iface in type.AllInterfaces)
        {
            if (MediatorHandlerInterfaces.Any(meta => MatchesMetadataName(iface, meta)))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsEntityFrameworkType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (type is INamedTypeSymbol named)
        {
            if (EntityFrameworkTypes.Any(meta => MatchesMetadataName(named, meta)))
            {
                return true;
            }

            var current = named;
            while (current.BaseType is not null)
            {
                current = current.BaseType;
                if (EntityFrameworkTypes.Any(meta => MatchesMetadataName(current, meta)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsRepositoryType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (display.Contains("Repository", StringComparison.OrdinalIgnoreCase) ||
            LooksLikeRepositoryFacade(display))
        {
            return true;
        }

        var simpleName = TrimGenericArity(type.Name);
        if (LooksLikeRepositorySimpleName(simpleName))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (ifaceName.Contains("Repository", StringComparison.OrdinalIgnoreCase) ||
                    LooksLikeRepositoryFacade(ifaceName) ||
                    LooksLikeRepositorySimpleName(TrimGenericArity(iface.Name)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsHttpClientType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (type is INamedTypeSymbol named)
        {
            // Check inheritance chain by metadata name equality
            var current = named;
            while (current is not null)
            {
                var meta = current.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (string.Equals(meta, "System.Net.Http.HttpClient", StringComparison.Ordinal))
                {
                    return true;
                }
                current = current.BaseType;
            }

            foreach (var iface in named.AllInterfaces)
            {
                var meta = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (string.Equals(meta, "System.Net.Http.IHttpClientFactory", StringComparison.Ordinal))
                {
                    return true;
                }
            }
        }

        // fallback to display name contains for unusual cases
        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (HttpClientTypes.Any(meta => display.Contains(meta, StringComparison.Ordinal)))
        {
            return true;
        }

        if (display.EndsWith("HttpClient", StringComparison.Ordinal))
        {
            return true;
        }

        return false;
    }

    private static bool IsMapperType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                if (MapperTypes.Any(meta => MatchesMetadataName(iface, meta)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsValidatorType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                if (ValidatorTypes.Any(meta => MatchesMetadataName(iface, meta)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsPipelineBehaviorType(ITypeSymbol? type)
    {
        if (type is not INamedTypeSymbol named)
        {
            return false;
        }

        foreach (var iface in named.AllInterfaces)
        {
            if (MatchesMetadataName(iface, "MediatR.IPipelineBehavior"))
            {
                return true;
            }
        }

        return false;
    }

    private static bool MatchesMetadataName(ITypeSymbol? candidate, string metadataName)
    {
        if (candidate is not INamedTypeSymbol named)
        {
            return false;
        }

        var normalized = NormalizeMetadataName(named);
        if (string.Equals(normalized, metadataName, StringComparison.Ordinal))
        {
            return true;
        }

        if (metadataName.StartsWith("global::", StringComparison.Ordinal))
        {
            var trimmed = metadataName.Substring("global::".Length);
            if (string.Equals(normalized, trimmed, StringComparison.Ordinal))
            {
                return true;
            }
        }

        if (normalized.StartsWith("global::", StringComparison.Ordinal))
        {
            var trimmedCandidate = normalized.Substring("global::".Length);
            if (string.Equals(trimmedCandidate, metadataName, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static string NormalizeMetadataName(INamedTypeSymbol symbol)
    {
        var display = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return display.Replace("global::", string.Empty, StringComparison.Ordinal);
    }

    public static ITypeSymbol? GetReceiverType(IInvocationOperation invocation)
    {
        if (invocation.Instance?.Type is { } instanceType)
        {
            return instanceType;
        }

        if (invocation.TargetMethod.IsExtensionMethod && invocation.Arguments.Length > 0)
        {
            return invocation.Arguments[0].Value.Type;
        }

        if (invocation.Syntax is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccess })
        {
            var model = invocation.SemanticModel;
            if (model is not null)
            {
                var expressionType = model.GetTypeInfo(memberAccess.Expression);
                if (expressionType.Type is { } type)
                {
                    return type;
                }

                if (expressionType.ConvertedType is { } converted)
                {
                    return converted;
                }
            }
        }

            return invocation.TargetMethod.ContainingType;
    }

    private static readonly string[] HttpWrapperVerbs =
    {
        "Get",
        "Post",
        "Put",
        "Delete",
        "Patch",
        "Head",
        "Options"
    };

    private static bool LooksLikeRepositoryFacade(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var normalized = typeName!;
        if (normalized.IndexOf(".IRead", StringComparison.OrdinalIgnoreCase) >= 0 ||
            normalized.IndexOf(".IWrite", StringComparison.OrdinalIgnoreCase) >= 0 ||
            normalized.IndexOf(".IControlledRepository", StringComparison.OrdinalIgnoreCase) >= 0 ||
            normalized.IndexOf(".IReadRepository", StringComparison.OrdinalIgnoreCase) >= 0 ||
            normalized.IndexOf(".IWriteRepository", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return true;
        }

        return false;
    }

    private static bool LooksLikeRepositorySimpleName(string? simpleName)
    {
        if (string.IsNullOrWhiteSpace(simpleName))
        {
            return false;
        }

        var trimmed = TrimGenericArity(simpleName);
        if (trimmed.Contains("Repository", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return trimmed.Equals("IRead", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IWrite", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IControlledRepository", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IReadRepository", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IWriteRepository", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IReadOnlyRepository", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("IWriteOnlyRepository", StringComparison.OrdinalIgnoreCase);
    }

    private static string TrimGenericArity(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        var tickIndex = name.IndexOf('`');
        return tickIndex >= 0 ? name[..tickIndex] : name;
    }
}
