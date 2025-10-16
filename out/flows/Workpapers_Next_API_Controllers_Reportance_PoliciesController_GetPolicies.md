[web] GET /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicies)  [L78–L92] status=200
  └─ maps_to OrderedPolicy [L91]
    └─ converts_to OrderedPolicy [L17]
  └─ uses_service UserService
    └─ method IsSuperUser [L82]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L88]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ GetFirmReportSettingQuery
    └─ handlers 1
      └─ GetFirmReportSettingQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ OrderedPolicy

