[web] PUT /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.PutFirm)  [L149–L168] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L156]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L153]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

