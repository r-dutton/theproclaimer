[web] GET /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UserAuthorisedForTemplate)  [L161–L176] [auth=AuthorizationPolicies.User]
  └─ uses_service UnitOfWork
    └─ method Table [L163]
  └─ uses_service UserService
    └─ method IsSuperUser [L171]

