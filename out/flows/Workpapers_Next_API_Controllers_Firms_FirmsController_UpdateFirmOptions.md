[web] PUT /api/firms/{id:Guid}/options  (Workpapers.Next.API.Controllers.Firms.FirmsController.UpdateFirmOptions)  [L170–L183] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L177]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L174]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

