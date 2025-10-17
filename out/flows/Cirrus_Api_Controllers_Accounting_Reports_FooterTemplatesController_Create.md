[web] POST /api/accounting/reports/footer-templates/  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Create)  [L52–L58] status=201 [auth=user,admin]
  └─ calls FooterTemplateRepository.Add [L56]
  └─ insert FooterTemplate [L56]
    └─ reads_from ReportFooterTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ FooterTemplate writes=1 reads=0

