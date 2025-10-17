[web] GET /api/accounting/reports/watermarks/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.Get)  [L35–L42] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to ReportWatermarkDto [L38]
    └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
  └─ calls WatermarkRepository.ReadQuery [L38]
  └─ query ReportWatermark [L38]
    └─ reads_from ReportWatermarks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportWatermark writes=0 reads=1
    └─ mappings 1
      └─ ReportWatermarkDto

