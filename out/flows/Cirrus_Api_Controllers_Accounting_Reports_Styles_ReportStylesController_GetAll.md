[web] GET /api/accounting/reports/styles/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.GetAll)  [L59–L66] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ReportStyleLightDto [L62]
    └─ automapper.registration CirrusMappingProfile (ReportStyle->ReportStyleLightDto) [L582]
  └─ calls ReportStyleRepository.ReadQuery [L62]
  └─ query ReportStyle [L62]
    └─ reads_from ReportStyles
  └─ uses_service IControlledRepository<ReportStyle>
    └─ method ReadQuery [L62]
      └─ ... (no implementation details available)

