[web] PUT /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.PutFirm)  [L149–L168] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L156]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method IsSuperUser [L153]
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

