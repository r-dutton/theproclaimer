[web] GET /api/accounting/reports/header-templates/  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.GetAll)  [L54–L60] status=200 [auth=user]
  └─ maps_to HeaderTemplateLightDto [L57]
    └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
  └─ calls HeaderTemplateRepository.ReadQuery [L57]
  └─ query HeaderTemplate [L57]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method ReadQuery [L57]
      └─ ... (no implementation details available)

