[web] GET /api/export/binders/{binderId:guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.BinderExportController.GetTrialBalanceExportData)  [L39–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetTrialBalanceExportDataQuery [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTrialBalanceExportDataQueryHandler.Handle [L32–L160]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L50]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L113]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

