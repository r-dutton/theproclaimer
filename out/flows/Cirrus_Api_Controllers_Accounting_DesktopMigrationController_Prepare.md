[web] POST /api/accounting/desktop-migrate/prepare  (Cirrus.Api.Controllers.Accounting.DesktopMigrationController.Prepare)  [L23–L29] [auth=user]
  └─ sends_request PrepareDesktopMigrationCommand [L27]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Utilities.DesktopMigration.Commands.PrepareDesktopMigrationCommandHandler.Handle [L33–L117]
      └─ uses_service DesktopSQLiteFileService (InstancePerLifetimeScope)
        └─ method InitialiseAsync [L61]
      └─ uses_service EntityRelationshipBuilder (InstancePerLifetimeScope)
        └─ method PrepareMap [L66]
      └─ uses_service GetAllDivisionsQuery
        └─ method Execute [L62]
      └─ uses_service GetAllSourcesQuery
        └─ method Execute [L86]
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L88]
      └─ uses_service StorageService
        └─ method GetStream [L60]

