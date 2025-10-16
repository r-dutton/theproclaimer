[web] POST /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.AddPolicyBindingModel)  [L110–L126] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L117]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method IsSuperUser [L114]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L122]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ requests 1
      └─ GetFirmReportSettingQuery
    └─ handlers 1
      └─ GetFirmReportSettingQueryHandler
    └─ caches 1
      └─ IMemoryCache

