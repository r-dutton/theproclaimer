[web] GET /api/accounting/reports/masters/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetPackage)  [L72–L76] status=200 [auth=user,admin]
  └─ sends_request ReportMasterPackageQuery -> ReportMasterPackageQueryHandler [L75]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterPackageQueryHandler.Handle [L37–L75]
      └─ maps_to ReportPageLayoutLightDto [L68]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
      └─ maps_to FooterTemplateDto [L65]
        └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
        └─ converts_to FooterContentDto [L49]
      └─ maps_to HeaderTemplateLightDto [L62]
        └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
      └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
      └─ uses_service IControlledRepository<FooterTemplate> (Scoped (inferred))
        └─ method ReadQuery [L65]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.FooterTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<HeaderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.HeaderTemplateRepository.ReadQuery
      └─ uses_service IJurisdictionService (AddScoped)
        └─ method GetFirmJurisdictions [L58]
          └─ implementation Workpapers.Next.ApplicationService.Features.Jurisdictions.JurisdictionService.GetFirmJurisdictions [L7-L27]
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
    └─ requests 1
      └─ ReportMasterPackageQuery
    └─ handlers 1
      └─ ReportMasterPackageQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ FooterTemplateDto
      └─ HeaderTemplateLightDto
      └─ ReportPageLayoutLightDto

