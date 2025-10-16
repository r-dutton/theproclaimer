[web] POST /api/starters  (Workpapers.Next.API.Controllers.Templates.StartersController.AddStarter)  [L81–L92] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L89]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method IsSuperUser [L85]
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

