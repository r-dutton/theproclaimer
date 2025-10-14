[web] GET /api/template-help-contents/for-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.GetAllHelpContent)  [L50–L58] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L53]
    └─ automapper.registration WorkpapersMappingProfile (TemplateHelpContent->TemplateHelpContentDto) [L326]
  └─ calls TemplateHelpContentRepository.ReadQuery [L53]
  └─ queries TemplateHelpContent [L53]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method ReadQuery [L53]

