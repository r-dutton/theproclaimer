[web] POST /api/offices  (Workpapers.Next.API.Controllers.OfficeController.CreateOffice)  [L102–L111] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to OfficeDto [L110]
  └─ uses_service UnitOfWork
    └─ method Add [L108]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetFirmId [L106]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ OfficeDto

