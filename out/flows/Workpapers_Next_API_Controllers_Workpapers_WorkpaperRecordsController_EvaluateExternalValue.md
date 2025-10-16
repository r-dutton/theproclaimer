[web] GET /api/workpaper-records/external-values/evaluate  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.EvaluateExternalValue)  [L244–L248] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request EvaluateExternalValueQuery [L247]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.ExternalValues.EvaluateExternalValueQueryHandler.Handle [L54–L126]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDataset [L121]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDataset [L18-L250]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L104]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L110]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

