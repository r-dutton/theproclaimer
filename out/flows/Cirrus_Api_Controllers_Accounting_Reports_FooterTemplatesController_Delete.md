[web] DELETE /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Delete)  [L68–L74] status=200 [auth=user,admin]
  └─ calls FooterTemplateRepository (methods: Remove,WriteQuery) [L72]
  └─ delete FooterTemplate [L72]
    └─ reads_from ReportFooterTemplates
  └─ write FooterTemplate [L71]
    └─ reads_from ReportFooterTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ FooterTemplate writes=2 reads=0

