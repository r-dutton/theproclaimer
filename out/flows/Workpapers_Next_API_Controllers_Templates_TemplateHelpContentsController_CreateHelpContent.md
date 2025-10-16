[web] POST /api/template-help-contents  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.CreateHelpContent)  [L60–L69] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L68]
  └─ calls TemplateHelpContentRepository.Add [L66]
  └─ insert TemplateHelpContent [L66]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method Add [L66]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L68]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

