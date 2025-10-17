[web] POST /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.AddFirm)  [L122–L147] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to FirmWithSubscriptionsDto [L146]
    └─ converts_to FirmWithStatsDto [L170]
  └─ uses_service UnitOfWork
    └─ method Table [L130]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ FirmWithSubscriptionsDto

