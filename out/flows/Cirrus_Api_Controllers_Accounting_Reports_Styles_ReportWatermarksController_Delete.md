[web] DELETE /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Delete)  [L83–L88] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ writes_to ReportWatermark [L87]
    └─ reads_from ReportWatermarks
  └─ writes_to ReportWatermark [L86]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method WriteQuery [L86]

