[web] GET /api/internal/storage/all-sites  (Dataverse.Api.Controllers.Internal.StorageController.GetAllSites)  [L222–L228] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetAllSharePointSitesQuery -> GetAllSharePointSitesQueryHandler [L225]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetAllSharePointSitesQueryHandler.Handle [L33–L189]
      └─ calls TenantRepository.ReadTable [L92]
      └─ maps_to SecureDocumentStoreCredentialsDto [L83]
      └─ uses_service SharePointRestAuthenticationManager
        └─ method GetSharePointCertificate [L111]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ GetAllSharePointSitesQuery
    └─ handlers 1
      └─ GetAllSharePointSitesQueryHandler
    └─ mappings 1
      └─ SecureDocumentStoreCredentialsDto

