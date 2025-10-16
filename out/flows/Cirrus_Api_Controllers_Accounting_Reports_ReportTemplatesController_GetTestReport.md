[web] GET /api/accounting/reports/templates/test  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetTestReport)  [L150–L154] status=200 [auth=user]
  └─ sends_request GetTestReportQuery [L153]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetTestReportQueryHandler.Handle [L28–L64]
      └─ maps_to ReportSettingsDto [L58]
        └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L45]
          └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportSettings>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
          └─ ... (no implementation details available)

