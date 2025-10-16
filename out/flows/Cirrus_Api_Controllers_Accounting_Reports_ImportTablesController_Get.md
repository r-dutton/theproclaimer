[web] POST /api/accounting/reports/templates/import-tables/extract  (Cirrus.Api.Controllers.Accounting.Reports.ImportTablesController.Get)  [L24–L28] status=201 [auth=user]
  └─ sends_request ExtractExcelTablesCommand [L27]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportTemplates.ExtractExcelTablesCommandHandler.Handle [L35–L98]
      └─ uses_service DataverseService
        └─ method Get [L85]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetStream [L76]
          └─ implementation IStorageService.GetStream [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L84]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L79]
          └─ implementation IUserService.GetUser [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.GetStream [L76]

