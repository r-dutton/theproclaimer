[web] POST /api/internal/data-cloud/sync  (Dataverse.Api.Controllers.Internal.DataCloudController.SyncDataLake)  [L62–L72] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L65]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L65]

