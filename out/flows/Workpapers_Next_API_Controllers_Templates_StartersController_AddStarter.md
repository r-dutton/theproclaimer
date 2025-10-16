[web] POST /api/starters  (Workpapers.Next.API.Controllers.Templates.StartersController.AddStarter)  [L81–L92] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L89]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L85]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

