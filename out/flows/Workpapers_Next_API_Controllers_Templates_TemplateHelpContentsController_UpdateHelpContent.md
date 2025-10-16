[web] PUT /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.UpdateHelpContent)  [L71–L82] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L81]
  └─ calls TemplateHelpContentRepository.WriteQuery [L74]
  └─ write TemplateHelpContent [L74]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method WriteQuery [L74]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L81]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L77]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

