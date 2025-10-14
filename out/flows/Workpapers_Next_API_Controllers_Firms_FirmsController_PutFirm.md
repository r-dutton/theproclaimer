[web] PUT /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.PutFirm)  [L141–L160] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L148]
  └─ uses_service UserService
    └─ method IsSuperUser [L145]

