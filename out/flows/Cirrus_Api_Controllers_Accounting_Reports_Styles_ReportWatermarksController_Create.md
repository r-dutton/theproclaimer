[web] POST /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Create)  [L60–L67] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls WatermarkRepository.Add [L64]
  └─ insert ReportWatermark [L64]
    └─ reads_from ReportWatermarks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportWatermark writes=1 reads=0

