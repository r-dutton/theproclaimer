using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Classification
{
    public sealed class CallClassifier
    {
        // TODO: inject symbol references/namespaces as needed
        public CallKind Classify(IInvocationOperation inv)
        {
            var m = inv.TargetMethod;
            // Replace with your existing IsXxx helpers or name-based checks:
            if (IsMediatorSend(m)) return CallKind.MediatorSend;
            if (IsMediatorPublish(m)) return CallKind.MediatorPublish;
            if (IsHandlerHandle(m)) return CallKind.HandlerHandle;
            if (IsRepoCall(m)) return CallKind.Repo;
            if (IsDbContextCall(m)) return CallKind.Db;
            if (IsHttpClientCall(m)) return CallKind.Http;
            if (IsMapperMap(m)) return CallKind.Mapper;
            if (IsValidatorCall(m)) return CallKind.Validator;
            if (IsPipelineBehavior(m)) return CallKind.Pipeline;
            return CallKind.Other;
        }

        // Use predicates that are symbol-aware
        private static bool IsMediatorSend(IMethodSymbol m)
            => m.Name == "Send" || m.Name == "SendAsync";
        private static bool IsMediatorPublish(IMethodSymbol m)
            => m.Name == "Publish" || m.Name == "PublishAsync";
        private static bool IsHandlerHandle(IMethodSymbol m)
            => m.Name == "Handle" || m.Name == "HandleAsync";
        private static bool IsRepoCall(IMethodSymbol m)
            => m.ContainingType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                   .Contains("Repository", System.StringComparison.OrdinalIgnoreCase);
        private static bool IsDbContextCall(IMethodSymbol m)
            => InheritsFrom(m.ContainingType, "Microsoft.EntityFrameworkCore.DbContext");
        private static bool IsHttpClientCall(IMethodSymbol m)
            => ImplementsOrIs(m.ContainingType, "System.Net.Http.IHttpClientFactory") ||
               InheritsFrom(m.ContainingType, "System.Net.Http.HttpClient");
        private static bool IsMapperMap(IMethodSymbol m)
            => m.Name == "Map" || m.Name == "ProjectTo" || m.Name == "MapAsync";
        private static bool IsValidatorCall(IMethodSymbol m)
            => m.Name.StartsWith("Validate", System.StringComparison.Ordinal);
        private static bool IsPipelineBehavior(IMethodSymbol m)
            => ImplementsOrIs(m.ContainingType, "MediatR.IPipelineBehavior");

        private static bool InheritsFrom(ITypeSymbol? type, string metadata)
        {
            if (type is INamedTypeSymbol named)
            {
                var cur = named;
                while (cur is not null)
                {
                    if (cur.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                        .Equals(metadata, System.StringComparison.Ordinal))
                    {
                        return true;
                    }
                    cur = cur.BaseType;
                }
            }
            return false;
        }

        private static bool ImplementsOrIs(ITypeSymbol? type, string metadata)
        {
            if (type is INamedTypeSymbol named)
            {
                if (named.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                    .Equals(metadata, System.StringComparison.Ordinal))
                {
                    return true;
                }
                foreach (var iface in named.AllInterfaces)
                {
                    if (iface.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                        .Equals(metadata, System.StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
