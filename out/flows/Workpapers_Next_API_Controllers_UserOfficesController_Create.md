[web] POST /api/useroffices/  (Workpapers.Next.API.Controllers.UserOfficesController.Create)  [L80–L90] status=201 [auth=admin]
  └─ uses_service UnitOfWork
    └─ method Add [L87]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetFirmId [L86]
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

