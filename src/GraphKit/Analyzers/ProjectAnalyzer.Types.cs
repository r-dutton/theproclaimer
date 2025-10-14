using System;
using System.Collections.Generic;
using GraphKit.Graph;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed record ControllerActionInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string Route, string HttpMethod, string ControllerSymbolId)
    {
        public List<ControllerRequestInvocation> RequestInvocations { get; } = new();
        public List<ControllerClientInvocation> HttpClientInvocations { get; } = new();
        public List<ControllerMappingInvocation> MappingInvocations { get; } = new();
        public List<ControllerResponseUsage> ResponseUsages { get; } = new();
        public List<ControllerValidatorInvocation> ValidatorInvocations { get; } = new();
        public List<ControllerCastInvocation> CastInvocations { get; } = new();
        public List<ControllerNotificationInvocation> NotificationInvocations { get; } = new();
        public Dictionary<string, string> LocalVariables { get; } = new(StringComparer.OrdinalIgnoreCase);
        public List<CacheInvocation> CacheInvocations { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<ControllerRepositoryInvocation> RepositoryInvocations { get; } = new();
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
        public List<EndpointAuthorization> Authorizations { get; } = new();
        public bool AllowsAnonymous { get; set; }
        public HashSet<int> StatusCodes { get; } = new();
    }

    private sealed record ControllerRequestInvocation(string RequestType, int Line);

    private sealed record ControllerClientInvocation(string ClientType, string HttpMethod, string? RelativePath, int Line);

    private sealed record ControllerMappingInvocation(string? SourceType, string? DestinationType, string? AssignedVariable, int Line);

    private sealed record ControllerResponseUsage(string ResponseType, string? Variable, int Line, bool IsReturn);

    private sealed record ControllerValidatorInvocation(string ValidatorType, int Line);

    private sealed record ControllerCastInvocation(string SourceType, string DestinationType, string? AssignedVariable, int Line, string Kind);

    private sealed record ControllerNotificationInvocation(string NotificationType, int Line);

    private sealed record ControllerRepositoryInvocation(string RepositoryType, string? EntityType, string Method, string Operation, int Line);

    private sealed record MinimalEndpointInfo(string Route, string HttpMethod, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public string Fqdn => $"{Assembly}.Endpoints.{Name}";
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
        public List<EndpointAuthorization> Authorizations { get; } = new();
        public bool AllowsAnonymous { get; set; }
    }

    private sealed record RequestInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record HandlerInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string RequestType, string ResponseType)
    {
        public List<HandlerDbAccess> DbContextAccesses { get; } = new();
        public List<HandlerPublisherCall> PublisherCalls { get; } = new();
        public List<HandlerRepositoryCall> RepositoryCalls { get; } = new();
        public List<HandlerMapperCall> MapperCalls { get; } = new();
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<HandlerNotificationPublication> PublishedNotifications { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
        public List<HandlerClientInvocation> HttpClientInvocations { get; } = new();
    }

    private sealed record PipelineBehaviorInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string RequestType, string ResponseType)
    {
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
    }

    private sealed record RequestProcessorInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string RequestType, string ResponseType, string Kind)
    {
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
    }

    private sealed record HandlerDbAccess(string DbContextType, string Member, int Line);

    private sealed record HandlerPublisherCall(string PublisherType, string Method, int Line, string? MessageType);

    private sealed record HandlerRepositoryCall(string RepositoryType, string Method, int Line);

    private sealed record HandlerClientInvocation(string ClientType, string HttpMethod, string? RelativePath, int Line);

    private sealed record HandlerMapperCall(string? SourceType, string? DestinationType, int Line);

    private sealed record HandlerNotificationPublication(string NotificationType, int Line);

    private sealed record DtoInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record ValidatorInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string TargetType);

    private sealed record EntityInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string TableName)
    {
        public List<DbSetInfo> DbSetProperties { get; } = new();
    }

    private sealed record DbSetInfo(string Name, int Line);

    private sealed record DbContextInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public List<DbContextDbSet> DbSets { get; } = new();
    }

    private sealed record DbContextDbSet(string EntityType, string PropertyName, int Line);

    private sealed record TableInfo(string Name, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId);

    private sealed record HttpClientInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public List<HttpClientCall> OutboundCalls { get; } = new();
    }

    private sealed record HttpClientCall(string HttpMethod, string? Route, int Line, IReadOnlyCollection<string> QueryParameters)
    {
        public string? CanonicalRoute
        {
            get
            {
                if (Route is null)
                {
                    return null;
                }

                var path = Route.Split('?', 2)[0];
                return CanonicalizeRoute(path);
            }
        }
    }

    private sealed record HttpCallInfo(HttpClientInfo Client, HttpClientCall Call)
    {
        public string? EndpointRoute => Call.CanonicalRoute;
    }

    private sealed record PublisherInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, IReadOnlyDictionary<string, FieldDescriptor> FieldTypes)
    {
        public string? QueueOrTopic { get; init; }
        public string? Subject { get; init; }
    }

    private sealed record MessageContractInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record MappingInfo(string MapId, string FilePath, GraphSpan Span, string ProfileFqdn, string MapName, string SourceType, string DestinationType);

    private sealed record RepositoryInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, IReadOnlyDictionary<string, FieldDescriptor> FieldTypes)
    {
        public List<RepositoryDbAccess> DbAccesses { get; } = new();
        public List<RepositoryMapperCall> MapperCalls { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
        public List<HandlerClientInvocation> HttpClientInvocations { get; } = new();
    }

    private sealed record RepositoryDbAccess(string Member, string Method, int Line);

    private sealed record RepositoryMapperCall(string? SourceType, string? DestinationType, int Line);

    private sealed record DerivedRequestCandidate(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string BaseType);

    private sealed record NodeReference(string Id, string FilePath, GraphSpan Span);

    private sealed record ServiceUsage(string ServiceType, int Line, string? Method = null);

    private sealed record FieldDescriptor(string Type, int Line);

    private sealed record ServiceRegistrationInfo(string ServiceType, string ImplementationType, string Lifetime, string FilePath, GraphSpan Span, string Assembly, string Project);

    private sealed record ConfigurationValue(string Key, string Value, string FilePath, GraphSpan Span);

    private sealed record HttpClientBaseAddress(string ClientType, string BaseUrl, string SourceFile, int Line, string? ConfigurationKey, ConfigurationValue? Configuration);

    private sealed record NotificationInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string? ContractType);

    private sealed record NotificationHandlerInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string NotificationType)
    {
        public List<NotificationHandlerRepositoryCall> RepositoryCalls { get; } = new();
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<NotificationHandlerRequestInvocation> RequestInvocations { get; } = new();
        public List<HandlerMapperCall> MapperCalls { get; } = new();
        public List<HandlerNotificationPublication> PublishedNotifications { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
    }

    private sealed record NotificationHandlerRepositoryCall(string RepositoryType, string Method, int Line);

    private sealed record NotificationHandlerRequestInvocation(string RequestType, int Line);

    private sealed record BackgroundServiceInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public List<ServiceUsage> ServiceUsages { get; } = new();
        public List<BackgroundServiceRepositoryCall> RepositoryCalls { get; } = new();
        public List<CacheInvocation> CacheInvocations { get; } = new();
        public List<OptionsUsage> OptionsUsages { get; } = new();
        public List<ConfigurationUsage> ConfigurationUsages { get; } = new();
    }

    private sealed record BackgroundServiceRepositoryCall(string RepositoryType, string Method, int Line);

    private sealed record CacheInvocation(string CacheType, string Method, string? Key, int Line, string Operation);

    private sealed record CacheInfo(string TypeName, string Kind);

    private sealed record OptionsInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string? SectionName)
    {
        public Dictionary<string, string> Values { get; } = new(StringComparer.OrdinalIgnoreCase);
    }

    private sealed record OptionsUsage(string OptionsType, int Line);

    private sealed record ConfigurationUsage(string ConfigurationType, string Accessor, string? Key, int Line, string FilePath);

    private sealed record EndpointAuthorization(string? Policy, string? Roles, string? AuthenticationSchemes, string Source, int Line);

    private sealed record AuthorizationMetadata(List<EndpointAuthorization> Requirements, bool AllowsAnonymous);
}



