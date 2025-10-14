[web] PUT /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Update)  [L60–L66] [auth=user,admin]
  └─ calls FooterTemplateRepository.WriteQuery [L63]
  └─ writes_to FooterTemplate [L63]
    └─ reads_from ReportFooterTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method WriteQuery [L63]

