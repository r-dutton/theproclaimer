[web] POST /api/internal/data-lake/security/sync  (Dataverse.Api.Controllers.Internal.DataLakeController.SyncParentContainersPermissions)  [L67–L78] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IAzureDataLakeService (AddScoped)
    └─ method EnsureParentPermissionsAsync [L76]
      └─ implementation Dataverse.Services.DataLake.Implementations.AzureDataLakeService.EnsureParentPermissionsAsync [L33-L425]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L72]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L72]

