[web] POST /api/audit-trail/binders/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.CreateAuditMessageForBinder)  [L46–L52] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateAuditMessageForBinderCommand [L49]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.AuditHistories.CreateAuditMessageForBinderCommandHandler.Handle [L94–L145]
      └─ uses_service UserService
        └─ method GetUserIdIfPresent [L117]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserIdIfPresent [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserIdIfPresent [L20-L295]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L113]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service AuditHistoryStorageService
        └─ method QueueLog [L142]
          └─ implementation Workpapers.Next.Data.Storage.AuditHistory.AuditHistoryStorageService.QueueLog [L19-L174]
      └─ uses_storage AuditHistoryStorageService.QueueLog [L142]

