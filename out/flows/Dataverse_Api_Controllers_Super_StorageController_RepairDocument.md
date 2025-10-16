[web] GET /api/super/storage/repair-document/{documentId:Guid}/job/{jobId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.RepairDocument)  [L69–L170] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L149]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L145]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L133]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L122]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L80]
  └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L76]
  └─ calls DocumentVersionRepository.WriteQuery [L155]
  └─ write DocumentVersion [L155]
    └─ reads_from DocumentVersions
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L104]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method WriteQuery [L155]
      └─ ... (no implementation details available)
  └─ sends_request CopyDocumentVersionCommand [L106]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
      └─ uses_service UserService
        └─ method GetUserId [L79]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L89]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L54]
          └─ ... (no implementation details available)
  └─ sends_request RepairDocumentVersionCommand [L120]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.RepairDocumentVersionCommandHandler.Handle [L25–L101]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L41]
          └─ ... (no implementation details available)
  └─ sends_request UpdateDocumentVersionFileSizeCommand [L160]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.UpdateDocumentVersionFileSizeCommandHandler.Handle [L18–L40]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L30]
          └─ ... (no implementation details available)
  └─ sends_request ValidateDocumentCommand [L85]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L41]
          └─ ... (no implementation details available)

