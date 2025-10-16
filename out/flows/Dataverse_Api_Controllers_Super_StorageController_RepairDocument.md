[web] GET /api/super/storage/repair-document/{documentId:Guid}/job/{jobId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.RepairDocument)  [L69–L170] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L149]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
  └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L76]
  └─ calls DocumentVersionRepository.WriteQuery [L155]
  └─ write DocumentVersion [L155]
    └─ reads_from DocumentVersions
  └─ uses_service RequiredService
    └─ method AssignActiveTenant [L113]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L104]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ sends_request UpdateDocumentVersionFileSizeCommand -> UpdateDocumentVersionFileSizeCommandHandler [L160]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.UpdateDocumentVersionFileSizeCommandHandler.Handle [L18–L40]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L30]
          └─ implementation DocumentServiceFactory.GetDocumentWithService
  └─ sends_request CopyDocumentVersionCommand -> CopyDocumentVersionCommandHandler [L106]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
      └─ calls DocumentVersionRepository (methods: Add,ReadQuery,WriteQuery) [L110]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L89]
          └─ implementation DocumentServiceFactory.GetDocumentWithService (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L79]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ sends_request ValidateDocumentCommand -> ValidateDocumentCommandHandler [L85]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ calls DocumentVersionRepository.ReadQuery [L41]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
          └─ implementation DocumentServiceFactory.GetDefaultColdStorage
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DocumentVersion writes=1 reads=0
    └─ services 2
      └─ RequiredService
      └─ TenantService
    └─ requests 3
      └─ CopyDocumentVersionCommand
      └─ UpdateDocumentVersionFileSizeCommand
      └─ ValidateDocumentCommand
    └─ handlers 3
      └─ CopyDocumentVersionCommandHandler
      └─ UpdateDocumentVersionFileSizeCommandHandler
      └─ ValidateDocumentCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

