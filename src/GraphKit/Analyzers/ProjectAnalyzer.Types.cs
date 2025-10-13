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
        public List<ControllerValidatorInvocation> ValidatorInvocations { get; } = new();
        public List<ControllerCastInvocation> CastInvocations { get; } = new();
        public Dictionary<string, string> LocalVariables { get; } = new(StringComparer.OrdinalIgnoreCase);
    }

    private sealed record ControllerRequestInvocation(string RequestType, int Line);

    private sealed record ControllerClientInvocation(string ClientType, string HttpMethod, string? RelativePath, int Line);

    private sealed record ControllerMappingInvocation(string? SourceType, string? DestinationType, string? AssignedVariable, int Line);

    private sealed record ControllerValidatorInvocation(string ValidatorType, int Line);

    private sealed record ControllerCastInvocation(string SourceType, string DestinationType, string? AssignedVariable, int Line, string Kind);

    private sealed record MinimalEndpointInfo(string Route, string HttpMethod, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public string Fqdn => $"{Assembly}.Endpoints.{Name}";
    }

    private sealed record RequestInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record HandlerInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string RequestType, string ResponseType)
    {
        public List<HandlerDbAccess> DbContextAccesses { get; } = new();
        public List<HandlerPublisherCall> PublisherCalls { get; } = new();
        public List<HandlerRepositoryCall> RepositoryCalls { get; } = new();
        public List<HandlerMapperCall> MapperCalls { get; } = new();
        public List<ServiceUsage> ServiceUsages { get; } = new();
    }

    private sealed record HandlerDbAccess(string DbContextType, string Member, int Line);

    private sealed record HandlerPublisherCall(string PublisherType, string Method, int Line);

    private sealed record HandlerRepositoryCall(string RepositoryType, string Method, int Line);

    private sealed record HandlerMapperCall(string? SourceType, string? DestinationType, int Line);

    private sealed record DtoInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record ValidatorInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string TargetType);

    private sealed record EntityInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string TableName)
    {
        public List<DbSetInfo> DbSetProperties { get; } = new();
    }

    private sealed record DbSetInfo(string Name, int Line);

    private sealed record TableInfo(string Name, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId);

    private sealed record HttpClientInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name)
    {
        public List<HttpClientCall> OutboundCalls { get; } = new();
    }

    private sealed record HttpClientCall(string HttpMethod, string? Route, int Line)
    {
        public string? CanonicalRoute => Route is null ? null : CanonicalizeRoute(Route);
    }

    private sealed record HttpCallInfo(HttpClientInfo Client, HttpClientCall Call)
    {
        public string? EndpointRoute => Call.CanonicalRoute;
    }

    private sealed record PublisherInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, IReadOnlyDictionary<string, FieldDescriptor> FieldTypes);

    private sealed record MessageContractInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name);

    private sealed record MappingInfo(string MapId, string FilePath, GraphSpan Span, string ProfileFqdn, string MapName, string SourceType, string DestinationType);

    private sealed record RepositoryInfo(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, IReadOnlyDictionary<string, FieldDescriptor> FieldTypes)
    {
        public List<RepositoryDbAccess> DbAccesses { get; } = new();
        public List<RepositoryMapperCall> MapperCalls { get; } = new();
    }

    private sealed record RepositoryDbAccess(string Member, string Method, int Line);

    private sealed record RepositoryMapperCall(string? SourceType, string? DestinationType, int Line);

    private sealed record DerivedRequestCandidate(string Fqdn, string Assembly, string Project, string FilePath, GraphSpan Span, string SymbolId, string Name, string BaseType);

    private sealed record NodeReference(string Id, string FilePath, GraphSpan Span);

    private sealed record ServiceUsage(string ServiceType, int Line);

    private sealed record FieldDescriptor(string Type, int Line);

    private sealed record ServiceRegistrationInfo(string ServiceType, string ImplementationType, string Lifetime, string FilePath, GraphSpan Span, string Assembly, string Project);

    private sealed record ConfigurationValue(string Key, string Value, string FilePath, GraphSpan Span);

    private sealed record HttpClientBaseAddress(string ClientType, string BaseUrl, string SourceFile, int Line, string? ConfigurationKey, ConfigurationValue? Configuration);
}
