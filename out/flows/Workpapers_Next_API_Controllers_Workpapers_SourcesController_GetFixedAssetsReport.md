[web] GET /api/sources/{type}/fixed-assets-workpaper-report  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetFixedAssetsReport)  [L449–L453] [auth=AuthorizationPolicies.User]
  └─ sends_request GetFixedAssetReportQuery [L452]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFixedAssetReportQueryHandler.Handle [L32–L126]
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L72]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L81]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L98]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]

