[web] GET /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.GetAll)  [L48–L54] [auth=Authentication.UserPolicy]
  └─ maps_to ReportWatermarkDto [L51]
    └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
  └─ queries ReportWatermark [L51]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method ReadQuery [L51]

