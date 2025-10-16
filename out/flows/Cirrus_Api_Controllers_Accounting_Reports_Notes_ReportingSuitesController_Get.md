[web] GET /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Get)  [L77–L81] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetReportingSuiteQuery -> GetReportingSuiteQueryHandler [L80]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetReportingSuiteQueryHandler.Handle [L26–L50]
      └─ maps_to ReportingSuiteDto [L42]
        └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteDto) [L762]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L43]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<ReportingSuite> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.ReportingSuiteRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetReportingSuiteQuery
    └─ handlers 1
      └─ GetReportingSuiteQueryHandler
    └─ mappings 1
      └─ ReportingSuiteDto

