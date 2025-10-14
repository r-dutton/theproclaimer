[web] GET /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Get)  [L31–L37] [auth=Authentication.AdminPolicy]
  └─ maps_to ReportSettingsDto [L34]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
  └─ calls ReportSettingsRepository.ReadQuery [L34]
  └─ queries ReportSettings [L34]
    └─ reads_from ReportSettings
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method ReadQuery [L34]

