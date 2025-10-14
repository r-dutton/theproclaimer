[web] PUT /api/firms/{id:Guid}/options  (Workpapers.Next.API.Controllers.Firms.FirmsController.UpdateFirmOptions)  [L162–L175] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L169]
  └─ uses_service UserService
    └─ method IsSuperUser [L166]

