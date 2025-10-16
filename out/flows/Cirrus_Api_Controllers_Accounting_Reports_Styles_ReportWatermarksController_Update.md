[web] PUT /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Update)  [L72–L78] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls WatermarkRepository.WriteQuery [L75]
  └─ write ReportWatermark [L75]
    └─ reads_from ReportWatermarks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportWatermark writes=1 reads=0

