[web] DELETE /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.DeleteFirm)  [L177–L187] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to FirmDto [L186]
    └─ converts_to FirmWithStatsDto [L162]
  └─ uses_service IMapper
    └─ method Map [L186]
  └─ uses_service UnitOfWork
    └─ method Table [L181]

