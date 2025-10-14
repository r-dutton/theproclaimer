[web] POST /api/internal/data-lake/security/sync  (Dataverse.Api.Controllers.Internal.DataLakeController.SyncParentContainersPermissions)  [L67–L78] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IAzureDataLakeService (AddScoped)
    └─ method EnsureParentPermissionsAsync [L76]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L72]

