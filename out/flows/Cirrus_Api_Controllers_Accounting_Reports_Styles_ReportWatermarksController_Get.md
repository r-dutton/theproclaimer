[web] GET /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Get)  [L35–L42] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to ReportWatermarkDto [L38]
    └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
  └─ query ReportWatermark [L38]
    └─ reads_from ReportWatermarks
  └─ uses_service IControlledRepository<ReportWatermark>
    └─ method ReadQuery [L38]
      └─ ... (no implementation details available)

