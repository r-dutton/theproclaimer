[web] GET /api/reportsettings/status  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.SettingsStatus)  [L72–L85] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L77]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method GetFirmId [L75]
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

