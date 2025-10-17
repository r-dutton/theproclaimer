[web] GET /api/accounting/reports/header-templates/  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.GetAll)  [L54–L60] status=200 [auth=user]
  └─ maps_to HeaderTemplateLightDto [L57]
    └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
  └─ calls HeaderTemplateRepository.ReadQuery [L57]
  └─ query HeaderTemplate [L57]
    └─ reads_from ReportHeaderTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ HeaderTemplate writes=0 reads=1
    └─ mappings 1
      └─ HeaderTemplateLightDto

