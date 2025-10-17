[web] DELETE /api/accounting/reports/settings/  (Cirrus.Api.Controllers.Accounting.Reports.ReportSettingsController.Delete)  [L62–L68] status=200 [auth=Authentication.AdminPolicy]
  └─ calls ReportSettingsRepository (methods: Remove,WriteQuery) [L67]
  └─ delete ReportSettings [L67]
    └─ reads_from ReportSettings
  └─ write ReportSettings [L65]
    └─ reads_from ReportSettings
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ ReportSettings writes=2 reads=0

