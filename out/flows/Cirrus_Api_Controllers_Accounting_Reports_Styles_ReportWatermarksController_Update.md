[web] PUT /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Update)  [L72–L78] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ writes_to ReportWatermark [L75]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method WriteQuery [L75]

