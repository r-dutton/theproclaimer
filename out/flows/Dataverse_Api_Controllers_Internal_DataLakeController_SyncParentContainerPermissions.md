[web] POST /api/internal/data-lake/security/{storeId:Guid}/sync  (Dataverse.Api.Controllers.Internal.DataLakeController.SyncParentContainerPermissions)  [L80–L93] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IAzureDataLakeService (AddScoped)
    └─ method EnsureParentPermissionsAsync [L92]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L86]

