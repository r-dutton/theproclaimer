[web] GET /api/templates  (Workpapers.Next.API.Controllers.Templates.TemplatesController.UserAuthorisedForTemplate)  [L141–L156] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L143]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L151]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

