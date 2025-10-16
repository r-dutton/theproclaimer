[web] GET /api/accounting/reports/pageTypes/package  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetUiPackage)  [L123–L134] status=200 [auth=user]
  └─ maps_to FooterTemplateLightDto [L129]
    └─ automapper.registration CirrusMappingProfile (FooterTemplate->FooterTemplateLightDto) [L628]
  └─ maps_to HeaderTemplateLightDto [L126]
    └─ automapper.registration CirrusMappingProfile (HeaderTemplate->HeaderTemplateLightDto) [L627]
  └─ calls FooterTemplateRepository.ReadQuery [L129]
  └─ calls HeaderTemplateRepository.ReadQuery [L126]
  └─ query FooterTemplate [L129]
    └─ reads_from ReportFooterTemplates
  └─ query HeaderTemplate [L126]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<FooterTemplate>
    └─ method ReadQuery [L129]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method ReadQuery [L126]
      └─ ... (no implementation details available)

