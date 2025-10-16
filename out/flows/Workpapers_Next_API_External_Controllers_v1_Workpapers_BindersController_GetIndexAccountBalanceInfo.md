[web] GET /workpapers/v1/binders/{binderId:Guid}/trial-balance  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetIndexAccountBalanceInfo)  [L108–L114] status=200
  └─ uses_service IWorkpapersProxyService (AddScoped)
    └─ method Get [L111]
      └─ implementation Workpapers.Next.API.External.Features.WorkpapersProxy.WorkpapersProxyService.Get [L13-L83]
        └─ uses_client WorkpapersClient [L31]
        └─ uses_service WorkpapersClient
          └─ method Get [L31]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ services 1
      └─ IWorkpapersProxyService

