[web] PUT /api/reportsettings/reporttemplates/setdefault/{id:Guid}  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.SetDefault)  [L94–L109] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L101]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method IsSuperUser [L98]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFirmReportSettingQuery -> GetFirmReportSettingQueryHandler [L99]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ implementation UnitOfWork.Table (see previous expansion)
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

