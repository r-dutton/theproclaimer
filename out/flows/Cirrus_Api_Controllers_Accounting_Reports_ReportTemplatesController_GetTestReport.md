[web] GET /api/accounting/reports/templates/test  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetTestReport)  [L150–L154] [auth=user]
  └─ sends_request GetTestReportQuery [L153]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetTestReportQueryHandler.Handle [L28–L64]
      └─ maps_to ReportSettingsDto [L58]
        └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L45]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L55]
      └─ uses_service IControlledRepository<ReportSettings>
        └─ method ReadQuery [L58]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]

