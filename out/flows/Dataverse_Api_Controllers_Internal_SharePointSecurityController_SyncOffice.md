[web] PUT /api/internal/sharepoint-security/office/{id:guid}  (Dataverse.Api.Controllers.Internal.SharePointSecurityController.SyncOffice)  [L92–L103] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L95]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L95]

