[web] GET /api/dataverse/firms/{dataverseId}/settings  (Workpapers.Next.API.Controllers.DataverseController.GetFirmSettings)  [L216–L228] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to WorkpaperSettingsDto [L220]
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L225]
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
  └─ uses_service UnitOfWork
    └─ method Table [L220]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ FirmFeatureFlagService
      └─ UnitOfWork
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ WorkpaperSettingsDto

