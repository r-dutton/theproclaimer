[web] GET /api/accounting/reports/styles/css/branding-css  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetBrandingCss)  [L46–L56] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to ReportSettingsDto [L53]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
  └─ calls ReportSettingsRepository.ReadQuery [L53]
  └─ calls OfficeRepository.ReadQuery [L51]
  └─ query ReportSettings [L53]
    └─ reads_from ReportSettings
  └─ query Office [L51]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method ReadQuery [L53]
      └─ ... (no implementation details available)
  └─ sends_request GetPdfBrandingCssQuery [L54]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfBrandingCssQueryHandler.Handle [L20–L41]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L31]
          └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]

