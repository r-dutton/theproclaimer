[web] DELETE /api/reportsettings/reset  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.ResetToFactoryDefaults)  [L99–L110] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method GetFirmId [L103]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L103]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

