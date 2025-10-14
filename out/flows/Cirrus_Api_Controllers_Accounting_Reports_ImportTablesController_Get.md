[web] POST /api/accounting/reports/templates/import-tables/extract  (Cirrus.Api.Controllers.Accounting.Reports.ImportTablesController.Get)  [L24–L28] [auth=user]
  └─ sends_request ExtractExcelTablesCommand [L27]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportTemplates.ExtractExcelTablesCommandHandler.Handle [L35–L98]
      └─ uses_service DataverseService
        └─ method Get [L85]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetStream [L76]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L84]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L79]

