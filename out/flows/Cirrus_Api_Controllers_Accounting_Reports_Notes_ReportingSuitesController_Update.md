[web] PUT /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Update)  [L117–L123] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportingSuiteRepository.WriteQuery [L120]
  └─ write ReportingSuite [L120]
    └─ reads_from PolicySuites
  └─ uses_service IControlledRepository<ReportingSuite>
    └─ method WriteQuery [L120]
      └─ ... (no implementation details available)

