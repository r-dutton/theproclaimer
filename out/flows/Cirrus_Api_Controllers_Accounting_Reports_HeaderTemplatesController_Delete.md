[web] DELETE /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Delete)  [L79–L85] status=200 [auth=user,admin]
  └─ calls HeaderTemplateRepository (methods: Remove,WriteQuery) [L83]
  └─ delete HeaderTemplate [L83]
    └─ reads_from ReportHeaderTemplates
  └─ write HeaderTemplate [L82]
    └─ reads_from ReportHeaderTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ HeaderTemplate writes=2 reads=0

