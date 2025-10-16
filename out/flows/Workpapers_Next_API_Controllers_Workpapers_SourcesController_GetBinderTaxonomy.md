[web] GET /api/sources/taxonomy  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinderTaxonomy)  [L433–L439] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTaxonomyQuery [L436]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderTaxonomyQueryHandler.Handle [L32–L113]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L92]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L86]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]

