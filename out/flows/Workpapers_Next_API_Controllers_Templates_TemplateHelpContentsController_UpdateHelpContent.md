[web] PUT /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.UpdateHelpContent)  [L71–L82] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L81]
  └─ calls TemplateHelpContentRepository.WriteQuery [L74]
  └─ writes_to TemplateHelpContent [L74]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method WriteQuery [L74]
  └─ uses_service IMapper
    └─ method Map [L81]
  └─ uses_service UserService
    └─ method IsSuperUser [L77]

