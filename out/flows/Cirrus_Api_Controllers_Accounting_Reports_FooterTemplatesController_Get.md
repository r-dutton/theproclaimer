[web] GET /api/accounting/reports/footer-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.FooterTemplatesController.Get)  [L34–L39] status=200 [auth=user]
  └─ maps_to FooterTemplateDto [L37]
    └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateDto) [L629]
    └─ converts_to FooterContentDto [L49]
  └─ calls FooterTemplateRepository.ReadQuery [L37]
  └─ query FooterTemplate [L37]
    └─ reads_from ReportFooterTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ FooterTemplate writes=0 reads=1
    └─ mappings 1
      └─ FooterTemplateDto

