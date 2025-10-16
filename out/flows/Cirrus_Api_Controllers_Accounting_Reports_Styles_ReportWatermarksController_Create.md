[web] POST /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Create)  [L60–L67] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ insert ReportWatermark [L64]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method Add [L64]
      └─ ... (no implementation details available)

