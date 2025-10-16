[web] GET /api/accounting/reports/styles/css/branding-css  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetBrandingCss)  [L46–L56] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to ReportSettingsDto [L53]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
  └─ calls ReportSettingsRepository.ReadQuery [L53]
  └─ calls OfficeRepository.ReadQuery [L51]
  └─ query ReportSettings [L53]
    └─ reads_from ReportSettings
  └─ query Office [L51]
    └─ reads_from Offices
  └─ sends_request GetPdfBrandingCssQuery -> GetPdfBrandingCssQueryHandler [L54]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfBrandingCssQueryHandler.Handle [L20–L41]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L31]
          └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Office writes=0 reads=1
      └─ ReportSettings writes=0 reads=1
    └─ requests 1
      └─ GetPdfBrandingCssQuery
    └─ handlers 1
      └─ GetPdfBrandingCssQueryHandler
    └─ mappings 1
      └─ ReportSettingsDto

