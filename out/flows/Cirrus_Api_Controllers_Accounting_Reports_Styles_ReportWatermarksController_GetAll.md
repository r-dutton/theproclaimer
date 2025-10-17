[web] GET /api/accounting/reports/watermarks/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWatermarksController.GetAll)  [L48–L54] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ReportWatermarkDto [L51]
    └─ automapper.registration CirrusMappingProfile (ReportWatermark->ReportWatermarkDto) [L605]
  └─ calls WatermarkRepository.ReadQuery [L51]
  └─ query ReportWatermark [L51]
    └─ reads_from ReportWatermarks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportWatermark writes=0 reads=1
    └─ mappings 1
      └─ ReportWatermarkDto

