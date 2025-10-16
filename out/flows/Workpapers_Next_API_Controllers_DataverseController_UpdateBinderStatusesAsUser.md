[web] PUT /api/dataverse/synchronise-binder-status-as-user  (Workpapers.Next.API.Controllers.DataverseController.UpdateBinderStatusesAsUser)  [L339–L345] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ sends_request SynchroniseBinderStatusCommand [L344]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Dataverse.SynchroniseBinderStatusCommandHandler.Handle [L28–L85]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ ... (no implementation details available)

