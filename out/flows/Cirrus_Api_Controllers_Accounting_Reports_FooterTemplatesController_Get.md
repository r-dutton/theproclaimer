[web] GET /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Get)  [L34–L39] status=200 [auth=user]
  └─ maps_to FooterTemplateDto [L37]
    └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
    └─ converts_to FooterContentDto [L49]
  └─ calls FooterTemplateRepository.ReadQuery [L37]
  └─ query FooterTemplate [L37]
    └─ reads_from ReportFooterTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method ReadQuery [L37]
      └─ ... (no implementation details available)

