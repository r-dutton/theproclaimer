[web] GET /api/accounting/reports/templates/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPackage)  [L144–L148] status=200 [auth=user]
  └─ sends_request ReportTemplatePackageQuery -> ReportTemplatePackageQueryHandler [L147]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportTemplatePackageQueryHandler.Handle [L50–L129]
      └─ maps_to ReportingSuiteUltraLightDto [L123]
        └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteUltraLightDto) [L766]
      └─ maps_to ReportPageLayoutLightDto [L119]
        └─ automapper.registration CirrusMappingProfile (ReportPageLayout->ReportPageLayoutLightDto) [L646]
      └─ maps_to FooterTemplateDto [L116]
        └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
        └─ converts_to FooterContentDto [L49]
      └─ maps_to HeaderTemplateLightDto [L113]
        └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
      └─ maps_to TradingAccountLightDto [L109]
        └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountLightDto) [L228]
      └─ maps_to DivisionLightDto [L105]
        └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
      └─ maps_to DatasetLightDto [L101]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ maps_to ReportWatermarkDto [L87]
        └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
      └─ maps_to ReportSettingsLightDto [L83]
        └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
      └─ uses_service IControlledRepository<ReportingSuite> (Scoped (inferred))
        └─ method ReadQuery [L123]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.ReportingSuiteRepository.ReadQuery
      └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
        └─ method ReadQuery [L119]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
      └─ uses_service IControlledRepository<FooterTemplate> (Scoped (inferred))
        └─ method ReadQuery [L116]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.FooterTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<HeaderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L113]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.HeaderTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<TradingAccount> (Scoped (inferred))
        └─ method ReadQuery [L109]
          └─ implementation Cirrus.Data.Repository.Accounting.Setup.TradingAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Division> (Scoped (inferred))
        └─ method ReadQuery [L105]
          └─ implementation Cirrus.Data.Repository.Accounting.Setup.DivisionRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IJurisdictionService (AddScoped)
        └─ method GetUserJurisdictions [L97]
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
      └─ uses_service IControlledRepository<ReportWatermark> (Scoped (inferred))
        └─ method ReadQuery [L87]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.WatermarkRepository.ReadQuery [L7-L42]
      └─ uses_service IControlledRepository<ReportSettings> (Scoped (inferred))
        └─ method ReadQuery [L83]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportSettingsRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ ReportTemplatePackageQuery
    └─ handlers 1
      └─ ReportTemplatePackageQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 9
      └─ DatasetLightDto
      └─ DivisionLightDto
      └─ FooterTemplateDto
      └─ HeaderTemplateLightDto
      └─ ReportingSuiteUltraLightDto
      └─ +4 more

