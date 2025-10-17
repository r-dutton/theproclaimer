[web] PUT /api/reportsettings/policies/options/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.UpdatePolicyOptions)  [L162–L180] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L166]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L171]
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

