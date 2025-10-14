[web] DELETE /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.DeleteHelpContent)  [L84–L95] [auth=AuthorizationPolicies.Administrator]
  └─ calls TemplateHelpContentRepository.Remove [L92]
  └─ calls TemplateHelpContentRepository.WriteQuery [L87]
  └─ writes_to TemplateHelpContent [L92]
    └─ reads_from TemplateHelpContents
  └─ writes_to TemplateHelpContent [L87]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method WriteQuery [L87]
  └─ uses_service UserService
    └─ method IsSuperUser [L90]

