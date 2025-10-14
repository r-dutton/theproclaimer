[web] GET /api/ui/clients/{id:guid}/has-dependencies  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForExternalDependencies)  [L139–L145] [auth=Authentication.UserPolicy]
  └─ sends_request CheckClientExternalDependenciesQuery [L142]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Clients.CheckClientExternalDependenciesQueryHandler.Handle [L35–L158]
      └─ uses_client CirrusClient [L115]
      └─ uses_client WorkpapersClient [L100]
      └─ uses_service CirrusClient
        └─ method Get [L115]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L130]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L153]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L62]
      └─ uses_service WorkpapersClient
        └─ method Get [L100]
      └─ uses_cache IDistributedCache [L155]
        └─ method SetRecordAsync [write] [L155]
      └─ uses_cache IDistributedCache [L146]
        └─ method GetRecordAsync [read] [L146]

