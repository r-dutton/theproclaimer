[web] GET /api/accounting/reports/styles/css/branding-css  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetBrandingCss)  [L46–L56] [auth=Authentication.AdminPolicy]
  └─ maps_to ReportSettingsDto [L53]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
  └─ calls ReportSettingsRepository.ReadQuery [L53]
  └─ calls OfficeRepository.ReadQuery [L51]
  └─ queries ReportSettings [L53]
    └─ reads_from ReportSettings
  └─ queries Office [L51]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L51]
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method ReadQuery [L53]
  └─ sends_request GetPdfBrandingCssQuery [L54]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfBrandingCssQueryHandler.Handle [L20–L41]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L31]

