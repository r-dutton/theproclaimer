[web] POST /api/accounting/reports/notes/reporting-suites/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Create)  [L106–L112] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportingSuiteRepository.Add [L110]
  └─ insert ReportingSuite [L110]
    └─ reads_from PolicySuites
  └─ uses_service IControlledRepository<ReportingSuite>
    └─ method Add [L110]
      └─ ... (no implementation details available)

