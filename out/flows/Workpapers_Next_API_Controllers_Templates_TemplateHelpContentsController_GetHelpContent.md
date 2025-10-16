[web] GET /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.GetHelpContent)  [L41–L48] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L44]
    └─ automapper.registration WorkpapersMappingProfile (TemplateHelpContent->TemplateHelpContentDto) [L326]
  └─ calls TemplateHelpContentRepository.ReadQuery [L44]
  └─ query TemplateHelpContent [L44]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

