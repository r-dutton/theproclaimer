[web] DELETE /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Delete)  [L62–L68] [auth=Authentication.AdminPolicy]
  └─ calls ReportSettingsRepository.Remove [L67]
  └─ calls ReportSettingsRepository.WriteQuery [L65]
  └─ writes_to ReportSettings [L67]
    └─ reads_from ReportSettings
  └─ writes_to ReportSettings [L65]
    └─ reads_from ReportSettings
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method WriteQuery [L65]

