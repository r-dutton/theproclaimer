[web] GET /api/internal/storage/old-hot-documents/{jobId:Guid}  (Dataverse.Api.Controllers.Internal.StorageController.GetOldHotDocuments)  [L168–L207] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache [L204]
    └─ method GetRecordAsync [read] [L204]
  └─ uses_cache IDistributedCache [L201]
    └─ method GetRecordAsync [read] [L201]
  └─ uses_cache IDistributedCache [L171]
    └─ method DoesRecordExistAsync [access] [L171]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L176]
  └─ sends_request GetOldHotDocumentsQuery [L188]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetOldHotDocumentsQueryHandler.Handle [L40–L253]
      └─ calls TenantRepository.ReadTable [L196]
      └─ maps_to SecureDocumentStoreCredentialsDto [L187]
      └─ uses_service ActiveDocumentsConfiguration
        └─ method DaysUntilMovedToColdStorage [L79]
      └─ uses_service DocumentStoreService
        └─ method SetCurrentDocumentStoreCredentials [L77]
      └─ uses_service IGraphClientService
        └─ method GetOldDocuments [L84]
      └─ uses_service IMapper
        └─ method Map [L187]
      └─ uses_cache IDistributedCache [L147]
        └─ method SetRecordAsync [write] [L147]
      └─ uses_cache IDistributedCache [L143]
        └─ method SetRecordAsync [write] [L143]
      └─ uses_cache IDistributedCache [L67]
        └─ method SetRecordAsync [write] [L67]

