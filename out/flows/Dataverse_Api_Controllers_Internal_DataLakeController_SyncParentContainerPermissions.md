[web] POST /api/internal/data-lake/security/{storeId:Guid}/sync  (Dataverse.Api.Controllers.Internal.DataLakeController.SyncParentContainerPermissions)  [L80–L93] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IAzureDataLakeService (AddScoped)
    └─ method EnsureParentPermissionsAsync [L92]
      └─ implementation Dataverse.Services.DataLake.Implementations.AzureDataLakeService.EnsureParentPermissionsAsync [L33-L425]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L86]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L86]

