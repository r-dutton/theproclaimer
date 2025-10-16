[web] POST /api/internal/documents/binder-document  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateBinderDocument)  [L188–L225] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service UserService
    └─ method GetUserId [L214]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ uses_service StorageService
    └─ method UploadFile [L197]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile [L18-L331]
        └─ uses_service TenantService
          └─ method GetPrivateContainer [L305]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetPrivateContainer [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                    └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                    └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
        └─ uses_service StorageSettingsConfig
          └─ method GetBlobServiceClient [L31]
            └─ implementation StorageSettingsConfig.GetBlobServiceClient
        └─ uses_storage StorageSettingsConfig.GetBlobServiceClient [L31]
  └─ uses_storage StorageService.UploadFile [L197]
  └─ sends_request CreateAuditHistoryCommand -> CreateAuditHistoryCommandHandler [L221]
    └─ handled_by Dataverse.ApplicationService.Commands.AuditHistories.CreateAuditHistoryCommandHandler.Handle [L21–L49]
      └─ calls AuditHistoryRepository.Add [L46]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L36]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserIfPresentAsync [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                      └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L82]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                      └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L71]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L81]
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 2
      └─ StorageService
      └─ UserService
    └─ requests 1
      └─ CreateAuditHistoryCommand
    └─ handlers 1
      └─ CreateAuditHistoryCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 2
      └─ StorageService
      └─ StorageSettingsConfig

