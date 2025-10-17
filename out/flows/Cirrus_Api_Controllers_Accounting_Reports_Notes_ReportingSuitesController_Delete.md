[web] DELETE /api/accounting/reports/notes/reporting-suites/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Delete)  [L128–L134] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportingSuiteRepository (methods: Remove,WriteQuery) [L132]
  └─ delete ReportingSuite [L132]
    └─ reads_from PolicySuites
  └─ write ReportingSuite [L131]
    └─ reads_from PolicySuites
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ ReportingSuite writes=2 reads=0

