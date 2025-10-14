[web] POST /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Create)  [L60–L67] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ writes_to ReportWatermark [L64]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method Add [L64]

