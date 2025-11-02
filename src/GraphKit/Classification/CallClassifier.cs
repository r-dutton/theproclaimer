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

        // Stub methods below; wire to your AnalysisPredicates from Phase 1.
        private static bool IsMediatorSend(IMethodSymbol m) => m.Name == "Send";
        private static bool IsMediatorPublish(IMethodSymbol m) => m.Name == "Publish";
        private static bool IsHandlerHandle(IMethodSymbol m) => m.Name == "Handle";
        private static bool IsRepoCall(IMethodSymbol m) => m.ContainingType.Name.Contains("Repository");
        private static bool IsDbContextCall(IMethodSymbol m) => m.ContainingType.Name.EndsWith("DbContext");
        private static bool IsHttpClientCall(IMethodSymbol m) => m.ContainingType.Name.Contains("HttpClient");
        private static bool IsMapperMap(IMethodSymbol m) => m.Name == "Map";
        private static bool IsValidatorCall(IMethodSymbol m) => m.Name.StartsWith("Validate");
        private static bool IsPipelineBehavior(IMethodSymbol m) => m.ContainingType.Name.Contains("PipelineBehavior");
    }
}
