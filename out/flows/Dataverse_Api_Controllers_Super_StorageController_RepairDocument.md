[web] GET /api/super/storage/repair-document/{documentId:Guid}/job/{jobId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.RepairDocument)  [L69–L170] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache [L149]
    └─ method GetRecordAsync [read] [L149]
  └─ uses_cache IDistributedCache [L145]
    └─ method GetRecordAsync [read] [L145]
  └─ uses_cache IDistributedCache [L133]
    └─ method SetRecordAsync [write] [L133]
  └─ uses_cache IDistributedCache [L122]
    └─ method SetRecordAsync [write] [L122]
  └─ uses_cache IDistributedCache [L91]
    └─ method SetRecordAsync [write] [L91]
  └─ uses_cache IDistributedCache [L80]
    └─ method SetRecordAsync [write] [L80]
  └─ uses_cache IDistributedCache [L76]
    └─ method DoesRecordExistAsync [access] [L76]
  └─ calls DocumentVersionRepository.WriteQuery [L155]
  └─ writes_to DocumentVersion [L155]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method WriteQuery [L155]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L104]
  └─ sends_request CopyDocumentVersionCommand [L106]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L89]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L54]
      └─ uses_service UserService
        └─ method GetUserId [L79]
  └─ sends_request RepairDocumentVersionCommand [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.RepairDocumentVersionCommandHandler.Handle [L25–L101]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L41]
  └─ sends_request UpdateDocumentVersionFileSizeCommand [L160]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.UpdateDocumentVersionFileSizeCommandHandler.Handle [L18–L40]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L30]
  └─ sends_request ValidateDocumentCommand [L85]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L41]

