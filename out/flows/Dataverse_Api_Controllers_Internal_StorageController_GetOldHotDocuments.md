[web] GET /api/internal/storage/old-hot-documents/{jobId:Guid}  (Dataverse.Api.Controllers.Internal.StorageController.GetOldHotDocuments)  [L168–L207] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L204]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L201]
  └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L171]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L176]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ sends_request GetOldHotDocumentsQuery [L188]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetOldHotDocumentsQueryHandler.Handle [L40–L253]
      └─ calls TenantRepository.ReadTable [L196]
      └─ maps_to SecureDocumentStoreCredentialsDto [L187]
      └─ uses_service ActiveDocumentsConfiguration
        └─ method DaysUntilMovedToColdStorage [L79]
          └─ ... (no implementation details available)
      └─ uses_service DocumentStoreService
        └─ method SetCurrentDocumentStoreCredentials [L77]
          └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.SetCurrentDocumentStoreCredentials [L17-L89]
      └─ uses_service IGraphClientService
        └─ method GetOldDocuments [L84]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L187]
          └─ ... (no implementation details available)
      └─ uses_storage DocumentStoreService.SetCurrentDocumentStoreCredentials [L77]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L147]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L143]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L67]

