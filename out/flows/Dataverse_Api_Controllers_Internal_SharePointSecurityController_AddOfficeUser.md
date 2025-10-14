[web] PUT /api/internal/sharepoint-security/office-user/{officeId:guid}/{userId:guid}  (Dataverse.Api.Controllers.Internal.SharePointSecurityController.AddOfficeUser)  [L50–L64] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L56]

