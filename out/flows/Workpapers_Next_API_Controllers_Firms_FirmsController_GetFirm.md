[web] GET /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirm)  [L88–L112] status=200
  └─ maps_to FirmDto [L96]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to FirmWithStatsDto [L96]
  └─ maps_to FirmWithSubscriptionsDto [L95]
    └─ converts_to FirmWithStatsDto [L170]
  └─ uses_service UnitOfWork
    └─ method Table [L95]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method IsSuperUser [L91]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ FirmDto
      └─ FirmWithStatsDto
      └─ FirmWithSubscriptionsDto

