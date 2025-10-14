[web] DELETE /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Delete)  [L128–L134] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportingSuiteRepository.Remove [L132]
  └─ calls ReportingSuiteRepository.WriteQuery [L131]
  └─ writes_to ReportingSuite [L132]
    └─ reads_from PolicySuites
  └─ writes_to ReportingSuite [L131]
    └─ reads_from PolicySuites
  └─ uses_service IControlledRepository<ReportingSuite>
    └─ method WriteQuery [L131]

