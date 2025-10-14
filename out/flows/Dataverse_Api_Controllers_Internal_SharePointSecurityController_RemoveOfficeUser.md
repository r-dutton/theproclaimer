[web] DELETE /api/internal/sharepoint-security/office-user/{officeId:guid}/{userId:guid}  (Dataverse.Api.Controllers.Internal.SharePointSecurityController.RemoveOfficeUser)  [L66–L79] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L71]

