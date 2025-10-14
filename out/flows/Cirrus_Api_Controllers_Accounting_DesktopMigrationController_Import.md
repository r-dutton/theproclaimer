[web] POST /api/accounting/desktop-migrate/import  (Cirrus.Api.Controllers.Accounting.DesktopMigrationController.Import)  [L31–L37] [auth=user]
  └─ sends_request DesktopMigrationCommand [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Utilities.DesktopMigration.Commands.DesktopMigrationCommandHandler.Handle [L24–L80]
      └─ uses_service AccountMigrator (InstancePerLifetimeScope)
        └─ method BuildAccounts [L72]
      └─ uses_service AssetModuleMigrator (InstancePerLifetimeScope)
        └─ method BuildAssets [L76]
      └─ uses_service DatasetMigrator (InstancePerLifetimeScope)
        └─ method BuildDatasets [L71]
      └─ uses_service DesktopMigrationService (InstancePerLifetimeScope)
        └─ method InitialiseMigration [L57]
      └─ uses_service DesktopSQLiteFileService (InstancePerLifetimeScope)
        └─ method InitialiseAsync [L65]
      └─ uses_service EntityRelationshipBuilder (InstancePerLifetimeScope)
        └─ method ImplementMaps [L68]
      └─ uses_service FileMigrator (InstancePerLifetimeScope)
        └─ method BuildFile [L69]
      └─ uses_service JournalMigrator (InstancePerLifetimeScope)
        └─ method BuildJournals [L75]
      └─ uses_service MatchingRuleMigrator (InstancePerLifetimeScope)
        └─ method BuildMatchingRules [L74]
      └─ uses_service SourceAccountMigrator (InstancePerLifetimeScope)
        └─ method BuildSourceAccounts [L73]
      └─ uses_service SourceMigrator (InstancePerLifetimeScope)
        └─ method BuildSources [L70]
      └─ uses_service StorageService
        └─ method GetStream [L64]

