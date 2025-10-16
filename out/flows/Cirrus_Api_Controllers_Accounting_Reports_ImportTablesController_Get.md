[web] POST /api/accounting/reports/templates/import-tables/extract  (Cirrus.Api.Controllers.Accounting.Reports.ImportTablesController.Get)  [L24–L28] status=201 [auth=user]
  └─ sends_request ExtractExcelTablesCommand -> ExtractExcelTablesCommandHandler [L27]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportTemplates.ExtractExcelTablesCommandHandler.Handle [L35–L98]
      └─ uses_service DataverseService
        └─ method Get [L85]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
            └─ uses_service TenantService
              └─ method GetStandardQueryParams [L62]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L84]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L79]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
            └─ uses_service RequestProcessor
              └─ method GetUserByDataverseId [L287]
                └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ +1 additional_requests elided
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_service Firm>
              └─ method GetFirmId [L139]
                └─ ... (no implementation details available)
            └─ uses_service User>
              └─ method GetUserIdFromToken [L106]
                └─ ... (no implementation details available)
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method GetStream [L76]
          └─ implementation DataGet.Data.BlobStorage.StorageService.GetStream [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.GetStream [L76]
  └─ impact_summary
    └─ requests 1
      └─ ExtractExcelTablesCommand
    └─ handlers 1
      └─ ExtractExcelTablesCommandHandler
    └─ caches 1
      └─ IMemoryCache

