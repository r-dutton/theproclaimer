[web] GET /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UserAuthorisedForTemplate)  [L161–L176] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service UnitOfWork
    └─ method Table [L163]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L171]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

