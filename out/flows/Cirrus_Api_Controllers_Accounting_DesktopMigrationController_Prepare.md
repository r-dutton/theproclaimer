[web] POST /api/accounting/desktop-migrate/prepare  (Cirrus.Api.Controllers.Accounting.DesktopMigrationController.Prepare)  [L23–L29] status=201 [auth=user]
  └─ sends_request PrepareDesktopMigrationCommand [L27]
    └─ handled_by Cirrus.Utilities.DesktopMigration.Commands.PrepareDesktopMigrationCommandHandler.Handle [L33–L117]
      └─ uses_service StorageService
        └─ method GetStream [L60]
          └─ implementation Cirrus.Services.Features.Storage.StorageService.GetStream [L19-L283]
      └─ uses_service DesktopSQLiteFileService (InstancePerLifetimeScope)
        └─ method InitialiseAsync [L61]
          └─ implementation Cirrus.Utilities.DesktopMigration.DesktopSQLiteFileService.InitialiseAsync [L8-L56]
      └─ uses_service EntityRelationshipBuilder (InstancePerLifetimeScope)
        └─ method PrepareMap [L66]
          └─ ... (no implementation details available)
      └─ uses_service GetAllDivisionsQuery
        └─ method Execute [L62]
          └─ ... (no implementation details available)
      └─ uses_service GetAllSourcesQuery
        └─ method Execute [L86]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L88]
          └─ ... (no implementation details available)
      └─ uses_storage StorageService.GetStream [L60]

