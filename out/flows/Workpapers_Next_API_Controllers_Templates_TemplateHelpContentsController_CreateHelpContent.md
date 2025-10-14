[web] POST /api/template-help-contents  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.CreateHelpContent)  [L60–L69] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L68]
  └─ calls TemplateHelpContentRepository.Add [L66]
  └─ writes_to TemplateHelpContent [L66]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method Add [L66]
  └─ uses_service IMapper
    └─ method Map [L68]
  └─ uses_service UserService
    └─ method IsSuperUser [L65]

