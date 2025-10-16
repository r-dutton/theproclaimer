[web] GET /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetAll)  [L83–L95] status=200 [auth=user]
  └─ maps_to ReportPageTypeLightWithContentFieldsDto [L90]
    └─ automapper.registration CirrusMappingProfile (ReportPageType->ReportPageTypeLightWithContentFieldsDto) [L635]
  └─ calls ReportPageTypeRepository.ReadQuery [L90]
  └─ query ReportPageType [L90]
    └─ reads_from ReportPageTypes
  └─ uses_service IJurisdictionService (AddScoped)
    └─ method GetUserJurisdictions [L88]
      └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetUserJurisdictions [L7-L27]
        └─ uses_service FirmSettingsService
          └─ method GetFirmJurisdictions [L18]
            └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetFirmJurisdictions [L23-L154]
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
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportPageType writes=0 reads=1
    └─ services 1
      └─ IJurisdictionService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ ReportPageTypeLightWithContentFieldsDto

