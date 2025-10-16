[web] DELETE /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Delete)  [L68–L74] status=200 [auth=user,admin]
  └─ calls FooterTemplateRepository.Remove [L72]
  └─ calls FooterTemplateRepository.WriteQuery [L71]
  └─ delete FooterTemplate [L72]
    └─ reads_from ReportFooterTemplates
  └─ write FooterTemplate [L71]
    └─ reads_from ReportFooterTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method WriteQuery [L71]
      └─ ... (no implementation details available)

