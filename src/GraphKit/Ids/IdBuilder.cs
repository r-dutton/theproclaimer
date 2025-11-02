using Microsoft.CodeAnalysis;

namespace GraphKit.Ids
{
    public static class IdBuilder
    {
        public static string EndpointId(IMethodSymbol action) => "endpoint:" + action.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string MinimalApiId(IMethodSymbol method) => "minapi:" + method.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string HandlerId(INamedTypeSymbol handler) => "handler:" + handler.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string CqrsRequestId(ITypeSymbol request) => "cqrsreq:" + request.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string HttpClientId(ISymbol scope) => "httpclient:" + scope.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string EntityId(ITypeSymbol entity) => "entity:" + entity.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        public static string ServiceId(ITypeSymbol service) => "service:" + service.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }
}
