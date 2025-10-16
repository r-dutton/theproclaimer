[web] GET /api/accounting/reports/footer-templates/  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.GetAll)  [L44–L50] status=200 [auth=user]
  └─ maps_to FooterTemplateLightDto [L47]
    └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateLightDto) [L628]
  └─ calls FooterTemplateRepository.ReadQuery [L47]
  └─ query FooterTemplate [L47]
    └─ reads_from ReportFooterTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method ReadQuery [L47]
      └─ ... (no implementation details available)

