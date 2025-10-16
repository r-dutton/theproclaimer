[web] DELETE /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.DeleteFirm)  [L185–L195] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to FirmDto [L194]
    └─ converts_to FirmWithStatsDto [L162]
  └─ uses_service IMapper
    └─ method Map [L194]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L189]
      └─ ... (no implementation details available)

