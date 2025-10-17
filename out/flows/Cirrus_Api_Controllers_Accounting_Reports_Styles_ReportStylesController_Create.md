[web] POST /api/accounting/reports/styles/  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Create)  [L72–L79] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportStyleRepository.Add [L76]
  └─ insert ReportStyle [L76]
    └─ reads_from ReportStyles
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportStyle writes=1 reads=0

