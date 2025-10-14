[web] PUT /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Save)  [L42–L57] [auth=Authentication.AdminPolicy]
  └─ calls ReportSettingsRepository.Add [L51]
  └─ calls ReportSettingsRepository.WriteQuery [L45]
  └─ writes_to ReportSettings [L51]
    └─ reads_from ReportSettings
  └─ writes_to ReportSettings [L45]
    └─ reads_from ReportSettings
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method WriteQuery [L45]

