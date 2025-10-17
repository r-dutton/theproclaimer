[web] POST /api/reportsettings/reporttemplates/  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.AddReportTemplate)  [L64–L75] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L72]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method IsSuperUser [L68]
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

