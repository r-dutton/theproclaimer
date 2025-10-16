[web] DELETE /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Delete)  [L83–L88] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls WatermarkRepository (methods: Remove,WriteQuery) [L87]
  └─ delete ReportWatermark [L87]
    └─ reads_from ReportWatermarks
  └─ write ReportWatermark [L86]
    └─ reads_from ReportWatermarks
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ ReportWatermark writes=2 reads=0

