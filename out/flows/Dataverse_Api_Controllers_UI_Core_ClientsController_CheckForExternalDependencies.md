[web] GET /api/ui/clients/{id:guid}/has-dependencies  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForExternalDependencies)  [L139–L145] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CheckClientExternalDependenciesQuery [L142]
    └─ handled_by Dataverse.ApplicationService.Queries.Clients.CheckClientExternalDependenciesQueryHandler.Handle [L35–L158]
      └─ uses_client CirrusClient [L115]
      └─ uses_client WorkpapersClient [L100]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L62]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service CirrusClient
        └─ method Get [L115]
          └─ ... (no implementation details available)
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L153]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L130]
          └─ ... (no implementation details available)
      └─ uses_service WorkpapersClient
        └─ method Get [L100]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L155]
      └─ uses_cache IDistributedCache.GetRecordAsync [read] [L146]

