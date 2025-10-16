[web] GET /api/accounting/reports/templates/test  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetTestReport)  [L150–L154] status=200 [auth=user]
  └─ sends_request GetTestReportQuery -> GetTestReportQueryHandler [L153]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetTestReportQueryHandler.Handle [L28–L64]
      └─ maps_to ReportSettingsDto [L58]
        └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsDto) [L531]
      └─ uses_service IControlledRepository<ReportSettings> (Scoped (inferred))
        └─ method ReadQuery [L58]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportSettingsRepository.ReadQuery
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L45]
          └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]
  └─ impact_summary
    └─ requests 1
      └─ GetTestReportQuery
    └─ handlers 1
      └─ GetTestReportQueryHandler
    └─ mappings 1
      └─ ReportSettingsDto

