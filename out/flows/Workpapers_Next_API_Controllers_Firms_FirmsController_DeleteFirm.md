[web] DELETE /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.DeleteFirm)  [L185–L195] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to FirmDto [L194]
    └─ converts_to FirmWithStatsDto [L162]
  └─ uses_service UnitOfWork
    └─ method Table [L189]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ FirmDto

