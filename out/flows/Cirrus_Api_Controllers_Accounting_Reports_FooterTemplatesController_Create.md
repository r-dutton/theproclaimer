[web] POST /api/accounting/reports/footer-templates/  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Create)  [L52–L58] [auth=user,admin]
  └─ calls FooterTemplateRepository.Add [L56]
  └─ writes_to FooterTemplate [L56]
    └─ reads_from ReportFooterTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method Add [L56]

