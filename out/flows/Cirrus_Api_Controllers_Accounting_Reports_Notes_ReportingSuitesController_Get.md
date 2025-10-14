[web] GET /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Get)  [L77–L81] [auth=Authentication.UserPolicy]
  └─ sends_request GetReportingSuiteQuery [L80]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetReportingSuiteQueryHandler.Handle [L26–L50]
      └─ maps_to ReportingSuiteDto [L42]
        └─ automapper.registration CirrusMappingProfile (ReportingSuite->ReportingSuiteDto) [L762]
      └─ uses_service IControlledRepository<ReportingSuite>
        └─ method ReadQuery [L42]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L43]

