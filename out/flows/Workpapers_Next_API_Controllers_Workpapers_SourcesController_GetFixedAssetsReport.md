[web] GET /api/sources/{type}/fixed-assets-workpaper-report  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetFixedAssetsReport)  [L449–L453] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetFixedAssetReportQuery [L452]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFixedAssetReportQueryHandler.Handle [L32–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L72]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]

