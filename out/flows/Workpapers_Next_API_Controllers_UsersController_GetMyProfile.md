[web] GET /api/users/profile  (Workpapers.Next.API.Controllers.UsersController.GetMyProfile)  [L131–L147] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to UserProfileDto [L135]
    └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L135]
  └─ query User [L135]
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L144]
      └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L110]
        └─ calls FirmFeatureFlagRepository.ReadQuery [L107]
        └─ uses_service FirmSettingsService
          └─ method IsFirmPartOfControlledBeta [L94]
            └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.IsFirmPartOfControlledBeta [L23-L154]
              └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
                └─ method GetSettings [L88]
                  └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.GetSettings
              └─ uses_service DataverseService
                └─ method GetSettings [L76]
                  └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetSettings [L17-L127]
                    └─ uses_service TenantIdentificationService
                      └─ method GetStandardQueryParams [L70]
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams [L15-L131]
                          └─ uses_service RequestProcessor
                            └─ method GetCatalogByFirmId [L104]
                              └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ +1 additional_requests elided
                          └─ uses_service FirmLightDto
                            └─ method AssignActiveTenant [L77]
                              └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                          └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
              └─ uses_service TenantIdentificationService
                └─ method GetSettings [L52]
                  └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetSettings [L15-L131]
                    └─ uses_service RequestProcessor
                      └─ method GetCatalogByFirmId [L104]
                        └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                        └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                        └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                        └─ +1 additional_requests elided
                    └─ uses_service FirmLightDto
                      └─ method AssignActiveTenant [L77]
                        └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
              └─ uses_service TenantService
                └─ method GetCurrentSettings [L46]
                  └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings [L5-L22]
                    └─ uses_service TenantIdentificationService
                      └─ method GetCurrentTenant [L20]
                        └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                          └─ uses_service RequestProcessor
                            └─ method GetCatalogByFirmId [L104]
                              └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                              └─ +1 additional_requests elided
                          └─ uses_service FirmLightDto
                            └─ method AssignActiveTenant [L77]
                              └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L60]
        └─ uses_service FeatureFlagService
          └─ method CanIUseFeature [L80]
            └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FeatureFlagService.CanIUseFeature [L10-L34]
              └─ calls FeatureFlagRepository.ReadQuery [L30]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L25]
        └─ uses_service UserService
          └─ method CanIUseFeature [L61]
            └─ implementation Workpapers.Next.ApplicationService.Services.UserService.CanIUseFeature [L20-L295]
              └─ uses_service RequestProcessor
                └─ method GetUserByDataverseId [L287]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                  └─ +1 additional_requests elided
              └─ uses_service bool?
                └─ method IsSuperUser [L174]
                  └─ ... (no implementation details available)
              └─ uses_service Firm>
                └─ method GetFirmId [L139]
                  └─ ... (no implementation details available)
              └─ uses_service User>
                └─ method GetUserIdFromToken [L106]
                  └─ ... (no implementation details available)
              └─ uses_service User
                └─ method GetUserId [L67]
                  └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
              └─ uses_service Guid?
                └─ method GetUserId [L64]
                  └─ ... (no implementation details available)
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L102]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettings [L137]
      └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
        └─ uses_service TenantService
          └─ method GetCurrentSettings [L46]
            └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings (see previous expansion)
  └─ uses_service UserService
    └─ method GetUserId [L135]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
  └─ uses_service UserRepository
    └─ method ReadQuery [L135]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
  └─ sends_request GetDataverseProfileQuery -> GetDataverseProfileQueryHandler [L139]
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetDataverseProfileQueryHandler.Handle [L25–L66]
      └─ uses_service FirmSettingsService
        └─ method StoreSettings [L57]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.StoreSettings [L15-L112]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCurrentSettings [L45]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings [L7-L35]
            └─ uses_service TenantService
              └─ method GetCurrentSettings [L34]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
            └─ uses_cache IDistributedCache.SetStringAsync [write] [L108]
            └─ uses_cache IDistributedCache.GetStringAsync [read] [L98]
      └─ uses_service DataverseService
        └─ method Get [L56]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
            └─ uses_service TenantService
              └─ method GetStandardQueryParams [L62]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
      └─ uses_service UserService
        └─ method GetUser [L46]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
            └─ uses_service IRequestInfoService (AddScoped)
              └─ method GetIdentityId [L103]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
            └─ uses_service IControlledRepository<User> (Scoped (inferred))
              └─ method GetUserId [L50]
                └─ implementation Cirrus.Data.Repository.Firm.UserRepository.GetUserId
            └─ uses_service User
              └─ method GetUserId [L37]
                └─ implementation Cirrus.DomainModel.Model.Firm.User.GetUserId [L18-L345]
            └─ uses_service Guid?
              └─ method GetUserId [L34]
                └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L43]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
  └─ returns DataverseProfileDto [L139]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 4
      └─ FirmFeatureFlagService
      └─ FirmSettingsService
      └─ UserRepository
      └─ UserService
    └─ requests 1
      └─ GetDataverseProfileQuery
    └─ handlers 1
      └─ GetDataverseProfileQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 2
      └─ DataverseProfileDto
      └─ UserProfileDto

