[web] GET /api/ui/contacts/{id}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Get)  [L83–L93] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ContactDto [L88]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
  └─ calls ContactRepository.ReadQuery [L88]
  └─ query Contact [L88]
    └─ reads_from DVS_Clients
  └─ sends_request CanIAccessContactQuery -> CanIAccessContactQueryHandler [L86]
    └─ handled_by Dataverse.ApplicationService.Queries.Contacts.CanIAccessContactQueryHandler.Handle [L36–L90]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L76]
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
      └─ uses_service IControlledRepository<Contact> (Scoped (inferred))
        └─ method ReadQuery [L74]
          └─ implementation Dataverse.Data.Repository.Clients.ContactRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L69]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L68]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L65]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L82]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L71]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L81]
            └─ logs ILogger<IRequestInfoService> [Warning] [L89]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L86]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L72]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L70]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Contact writes=0 reads=1
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 1
      └─ CanIAccessContactQuery
    └─ handlers 1
      └─ CanIAccessContactQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ ContactDto

