using GraphKit.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Classification
{
    public sealed class CallClassifier
    {
        private readonly TypeClassifier _typeClassifier = new();

        /// <summary>
        /// Classify an invocation into a coarse semantic bucket using existing analyzer predicates
        /// and symbol-based type classification.
        /// </summary>
        public CallKind Classify(IInvocationOperation inv)
        {
            if (inv is null || inv.TargetMethod is not { } method)
            {
                return CallKind.Other;
            }

            // Mediator / CQRS patterns
            if (AnalysisPredicates.IsMediatorSend(inv))
            {
                return CallKind.MediatorSend;
            }

            if (AnalysisPredicates.IsMediatorPublish(inv))
            {
                return CallKind.MediatorPublish;
            }

            if (AnalysisPredicates.IsDomainEventPublish(inv))
            {
                return CallKind.DomainEventPublish;
            }

            if (AnalysisPredicates.IsHandlerHandle(inv))
            {
                return CallKind.HandlerHandle;
            }

            // Storage / EF / repositories
            if (AnalysisPredicates.IsDbContextOrRepoCall(inv))
            {
                var receiver = AnalysisPredicates.GetReceiverType(inv);
                var classification = _typeClassifier.Classify(receiver ?? method.ContainingType);

                if (classification.IsDbContext)
                {
                    return CallKind.DbContext;
                }

                if (classification.IsRepository)
                {
                    return CallKind.Repository;
                }

                // Default to Repository when in doubt; existing behavior treated all as repo.
                return CallKind.Repository;
            }

            // HTTP / clients
            if (AnalysisPredicates.IsHttpClientCall(inv))
            {
                return CallKind.Http;
            }

            // Mapping / validation / pipeline
            if (AnalysisPredicates.IsMapperMap(inv))
            {
                return CallKind.Mapper;
            }

            if (AnalysisPredicates.IsValidatorCall(inv))
            {
                return CallKind.Validator;
            }

            if (AnalysisPredicates.IsPipelineBehavior(inv))
            {
                return CallKind.Pipeline;
            }

            return CallKind.Other;
        }
    }
}
