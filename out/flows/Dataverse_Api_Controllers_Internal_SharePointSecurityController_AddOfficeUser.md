[web] PUT /api/internal/sharepoint-security/office-user/{officeId:guid}/{userId:guid}  (Dataverse.Api.Controllers.Internal.SharePointSecurityController.AddOfficeUser)  [L50–L64] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L56]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L56]

