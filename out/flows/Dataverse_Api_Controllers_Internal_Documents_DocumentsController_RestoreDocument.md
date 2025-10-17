[web] PUT /api/internal/documents/{id:guid}/restore-document  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.RestoreDocument)  [L123–L138] status=204,404 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RestoreDocumentCommand -> RestoreDocumentCommandHandler [L130]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RestoreDocumentCommandHandler.Handle [L23–L46]
  └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L129]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
            └─ uses_client WorkpapersClient [L78]
            └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
              └─ method GetCurrentSettingsAsync [L52]
                └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
            └─ uses_service TenantService
              └─ method GetCurrentSettingsAsync [L44]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
            └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
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
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 2
      └─ CanIAccessDocumentQuery
      └─ RestoreDocumentCommand
    └─ handlers 2
      └─ CanIAccessDocumentQueryHandler
      └─ RestoreDocumentCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

