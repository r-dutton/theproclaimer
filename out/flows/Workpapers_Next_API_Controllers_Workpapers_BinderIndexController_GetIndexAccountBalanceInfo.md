[web] GET /api/binder-index/account-balance-info  (Workpapers.Next.API.Controllers.Workpapers.BinderIndexController.GetIndexAccountBalanceInfo)  [L23–L27] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndexAccountBalanceInfoQuery [L26]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetIndexAccountBalanceInfoQueryHandler.Handle [L36–L358]
      └─ maps_to AccountBalanceInfoDto [L65]
        └─ automapper.registration WorkpapersMappingProfile (Binder->AccountBalanceInfoDto) [L457]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L129]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

