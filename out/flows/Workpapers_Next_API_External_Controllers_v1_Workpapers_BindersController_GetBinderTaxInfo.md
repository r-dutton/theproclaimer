[web] GET /workpapers/v1/binders/{binderId:Guid}/tax-info  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderTaxInfo)  [L96–L100] status=200
  └─ uses_service IWorkpapersProxyService (AddScoped)
    └─ method Get [L99]
      └─ implementation Workpapers.Next.API.External.Features.WorkpapersProxy.WorkpapersProxyService.Get [L13-L83]
        └─ uses_client WorkpapersClient [L31]
        └─ uses_service WorkpapersClient
          └─ method Get [L31]
            └─ ... (no implementation details available)

