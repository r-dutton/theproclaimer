[web] GET /api/internal/storage/all-sites  (Dataverse.Api.Controllers.Internal.StorageController.GetAllSites)  [L222–L228] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetAllSharePointSitesQuery [L225]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetAllSharePointSitesQueryHandler.Handle [L33–L189]
      └─ calls TenantRepository.ReadTable [L92]
      └─ maps_to SecureDocumentStoreCredentialsDto [L83]
      └─ uses_service IMapper
        └─ method Map [L83]
      └─ uses_service SharePointRestAuthenticationManager
        └─ method GetSharePointCertificate [L111]

