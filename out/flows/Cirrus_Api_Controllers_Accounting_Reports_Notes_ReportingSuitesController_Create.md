[web] POST /api/accounting/reports/notes/reporting-suites/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.ReportingSuitesController.Create)  [L106–L112] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportingSuiteRepository.Add [L110]
  └─ insert ReportingSuite [L110]
    └─ reads_from PolicySuites
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportingSuite writes=1 reads=0

