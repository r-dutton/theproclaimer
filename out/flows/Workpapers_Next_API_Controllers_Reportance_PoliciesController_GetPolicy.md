[web] GET /api/reportsettings/policies/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicy)  [L46–L56] status=200
  └─ maps_to Policy [L49]
    └─ converts_to PolicyDto [L789]
    └─ converts_to PolicyLightDto [L792]
    └─ converts_to PolicyLightWithSuiteVariantsDto [L794]
    └─ converts_to Policy [L18]
  └─ uses_service UserService
    └─ method GetFirmId [L52]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L49]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ Policy

