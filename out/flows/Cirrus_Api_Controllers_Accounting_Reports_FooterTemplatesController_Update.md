[web] PUT /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Update)  [L60–L66] status=200 [auth=user,admin]
  └─ calls FooterTemplateRepository.WriteQuery [L63]
  └─ write FooterTemplate [L63]
    └─ reads_from ReportFooterTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ FooterTemplate writes=1 reads=0

