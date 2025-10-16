[web] POST /api/accounting/desktop-migrate/prepare  (Cirrus.Api.Controllers.Accounting.DesktopMigrationController.Prepare)  [L23–L29] status=201 [auth=user]
  └─ sends_request PrepareDesktopMigrationCommand -> PrepareDesktopMigrationCommandHandler [L27]
    └─ handled_by Cirrus.Utilities.DesktopMigration.Commands.PrepareDesktopMigrationCommandHandler.Handle [L33–L117]
      └─ calls SourceTypesRepository.ReadQuery [L88]
      └─ uses_service GetAllSourcesQuery
        └─ method Execute [L86]
          └─ ... (no implementation details available)
      └─ uses_service EntityRelationshipBuilder (InstancePerLifetimeScope)
        └─ method PrepareMap [L66]
          └─ ... (no implementation details available)
      └─ uses_service GetAllDivisionsQuery
        └─ method Execute [L62]
          └─ ... (no implementation details available)
      └─ uses_service DesktopSQLiteFileService (InstancePerLifetimeScope)
        └─ method InitialiseAsync [L61]
          └─ implementation Cirrus.Utilities.DesktopMigration.DesktopSQLiteFileService.InitialiseAsync [L8-L56]
      └─ uses_service StorageService
        └─ method GetStream [L60]
          └─ implementation Cirrus.Services.Features.Storage.StorageService.GetStream [L19-L283]
            └─ uses_service TenantService
              └─ method GetBlobServiceClient [L31]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetBlobServiceClient [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_storage StorageService.GetStream [L60]
  └─ impact_summary
    └─ requests 1
      └─ PrepareDesktopMigrationCommand
    └─ handlers 1
      └─ PrepareDesktopMigrationCommandHandler
    └─ caches 1
      └─ IMemoryCache

