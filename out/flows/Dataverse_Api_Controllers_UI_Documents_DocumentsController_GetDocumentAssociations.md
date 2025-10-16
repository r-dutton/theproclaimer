[web] GET /api/ui/documents/documents/associations  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentAssociations)  [L149–L167] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDocumentAssociations -> GetDocumentAssociationsHandler [L163]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentAssociationsHandler.Handle [L39–L148]
      └─ maps_to DeliverableUltraLightDto [L98]
        └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableUltraLightDto) [L355]
      └─ maps_to JobUltraLightDto [L86]
        └─ automapper.registration DataverseMappingProfile (Job->JobUltraLightDto) [L306]
      └─ uses_client WorkpapersClient [L119]
      └─ uses_service WorkpapersClient
        └─ method Get [L119]
          └─ ... (no implementation details available)
      └─ uses_service UserService
        └─ method GetUserId [L117]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L114]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IControlledRepository<Deliverable> (Scoped (inferred))
        └─ method ReadQuery [L98]
          └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery
      └─ uses_service IControlledRepository<Job> (Scoped (inferred))
        └─ method ReadQuery [L86]
          └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L74]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
  └─ sends_request CanIAccessDocumentQuery -> CanIAccessDocumentQueryHandler [L159]
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
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
            └─ maps_to FirmSettingsDto [L60]
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
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
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
      └─ GetDocumentAssociations
    └─ handlers 2
      └─ CanIAccessDocumentQueryHandler
      └─ GetDocumentAssociationsHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 3
      └─ DeliverableUltraLightDto
      └─ FirmSettingsDto
      └─ JobUltraLightDto

