[web] GET /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetAll)  [L159–L179] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeLightDto [L173]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
  └─ calls OfficeRepository.ReadQuery [L173]
  └─ query Office [L173]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L173]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L167]
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
    └─ method IsInRole [L162]
      └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]
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
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ clients 1
      └─ WorkpapersClient
    └─ services 3
      └─ FirmSettingsService
      └─ OfficeRepository
      └─ UserService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ OfficeLightDto

