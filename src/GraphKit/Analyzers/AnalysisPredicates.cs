using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
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

        var receiver = GetReceiverType(invocation);
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

        return false;
    }

    public static bool IsMapperMap(IInvocationOperation invocation)
    {
        if (invocation.TargetMethod is not { } method)
        {
            return false;
        }

        return MatchesName(method.Name, "Map", "ProjectTo", "MapAsync") &&
               (IsMapperType(GetReceiverType(invocation)) || IsMapperType(method.ContainingType));
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

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        return MediatorInterfaces.Any(meta => display.Contains(meta, StringComparison.Ordinal));
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

    private static bool IsEntityFrameworkType(ITypeSymbol? type)
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

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        return EntityFrameworkTypes.Any(meta => display.Contains(meta, StringComparison.Ordinal));
    }

    private static bool IsRepositoryType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (display.Contains("Repository", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (ifaceName.Contains("Repository", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsHttpClientType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (HttpClientTypes.Any(meta => display.Contains(meta, StringComparison.Ordinal)))
        {
            return true;
        }

        if (display.EndsWith("HttpClient", StringComparison.Ordinal))
        {
            return true;
        }

        if (type is INamedTypeSymbol named && named.BaseType is not null)
        {
            return IsHttpClientType(named.BaseType);
        }

        return false;
    }

    private static bool IsMapperType(ITypeSymbol? type)
    {
        if (type is null)
        {
            return false;
        }

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (MapperTypes.Any(meta => display.Contains(meta, StringComparison.Ordinal)))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (MapperTypes.Any(meta => ifaceName.Contains(meta, StringComparison.Ordinal)))
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

        var display = type.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        if (ValidatorTypes.Any(meta => display.Contains(meta, StringComparison.Ordinal)))
        {
            return true;
        }

        if (type is INamedTypeSymbol named)
        {
            foreach (var iface in named.AllInterfaces)
            {
                var ifaceName = iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                if (ValidatorTypes.Any(meta => ifaceName.Contains(meta, StringComparison.Ordinal)))
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

    private static ITypeSymbol? GetReceiverType(IInvocationOperation invocation)
    {
        if (invocation.Instance?.Type is { } instanceType)
        {
            return instanceType;
        }

        if (invocation.TargetMethod.IsExtensionMethod && invocation.Arguments.Length > 0)
        {
            return invocation.Arguments[0].Value.Type;
        }

        return invocation.TargetMethod.ContainingType;
    }
}
