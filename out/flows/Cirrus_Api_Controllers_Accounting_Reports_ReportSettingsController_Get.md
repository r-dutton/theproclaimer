[web] GET /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Get)  [L31–L37] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to ReportSettingsDto [L34]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
  └─ calls ReportSettingsRepository.ReadQuery [L34]
  └─ query ReportSettings [L34]
    └─ reads_from ReportSettings
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportSettings writes=0 reads=1
    └─ mappings 1
      └─ ReportSettingsDto

