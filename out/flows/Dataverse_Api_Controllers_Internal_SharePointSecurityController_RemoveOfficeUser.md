[web] DELETE /api/internal/sharepoint-security/office-user/{officeId:guid}/{userId:guid}  (Dataverse.Api.Controllers.Internal.SharePointSecurityController.RemoveOfficeUser)  [L66–L79] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L71]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L71]

