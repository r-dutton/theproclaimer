[web] GET /api/sources/{type}/fixed-assets-workpaper-report  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetFixedAssetsReport)  [L449–L453] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetFixedAssetReportQuery -> GetFixedAssetReportQueryHandler [L452]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFixedAssetReportQueryHandler.Handle [L32–L126]
      └─ calls SourceAccountRepository.ReadQuery [L98]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L81]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L72]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetFixedAssetReportQuery
    └─ handlers 1
      └─ GetFixedAssetReportQueryHandler

