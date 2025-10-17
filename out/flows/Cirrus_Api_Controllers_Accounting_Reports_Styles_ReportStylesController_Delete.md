[web] DELETE /api/accounting/reports/styles/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Delete)  [L100–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportStyleRepository (methods: Remove,WriteQuery) [L104]
  └─ delete ReportStyle [L104]
    └─ reads_from ReportStyles
  └─ write ReportStyle [L103]
    └─ reads_from ReportStyles
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ ReportStyle writes=2 reads=0

