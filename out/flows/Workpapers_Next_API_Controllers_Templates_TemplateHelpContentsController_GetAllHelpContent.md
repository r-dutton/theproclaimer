[web] GET /api/template-help-contents/for-template/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.GetAllHelpContent)  [L50–L58] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L53]
    └─ automapper.registration WorkpapersMappingProfile (TemplateHelpContent->TemplateHelpContentDto) [L326]
  └─ calls TemplateHelpContentRepository.ReadQuery [L53]
  └─ query TemplateHelpContent [L53]
    └─ reads_from TemplateHelpContents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TemplateHelpContent writes=0 reads=1
    └─ mappings 1
      └─ TemplateHelpContentDto

