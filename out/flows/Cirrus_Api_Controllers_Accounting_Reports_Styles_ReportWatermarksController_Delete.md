[web] DELETE /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Delete)  [L83–L88] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ delete ReportWatermark [L87]
    └─ reads_from ReportWatermarks
  └─ write ReportWatermark [L86]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method WriteQuery [L86]
      └─ ... (no implementation details available)

