[web] DELETE /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.DeleteHelpContent)  [L84–L95] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls TemplateHelpContentRepository.Remove [L92]
  └─ calls TemplateHelpContentRepository.WriteQuery [L87]
  └─ delete TemplateHelpContent [L92]
    └─ reads_from TemplateHelpContents
  └─ write TemplateHelpContent [L87]
    └─ reads_from TemplateHelpContents
  └─ uses_service IControlledRepository<TemplateHelpContent>
    └─ method WriteQuery [L87]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L90]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

