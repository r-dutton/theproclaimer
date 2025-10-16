[web] POST /api/accounting/desktop-migrate/import  (Cirrus.Api.Controllers.Accounting.DesktopMigrationController.Import)  [L31–L37] status=201 [auth=user]
  └─ sends_request DesktopMigrationCommand [L35]
    └─ handled_by Cirrus.Utilities.DesktopMigration.Commands.DesktopMigrationCommandHandler.Handle [L24–L80]
      └─ uses_service AccountMigrator (InstancePerLifetimeScope)
        └─ method BuildAccounts [L72]
          └─ ... (no implementation details available)
      └─ uses_service AssetModuleMigrator (InstancePerLifetimeScope)
        └─ method BuildAssets [L76]
          └─ ... (no implementation details available)
      └─ uses_service StorageService
        └─ method GetStream [L64]
          └─ implementation Cirrus.Services.Features.Storage.StorageService.GetStream [L19-L283]
      └─ uses_service DesktopMigrationService (InstancePerLifetimeScope)
        └─ method InitialiseMigration [L57]
          └─ implementation Cirrus.Utilities.DesktopMigration.DesktopMigrationService.InitialiseMigration [L9-L19]
      └─ uses_service DesktopSQLiteFileService (InstancePerLifetimeScope)
        └─ method InitialiseAsync [L65]
          └─ implementation Cirrus.Utilities.DesktopMigration.DesktopSQLiteFileService.InitialiseAsync [L8-L56]
      └─ uses_service DatasetMigrator (InstancePerLifetimeScope)
        └─ method BuildDatasets [L71]
          └─ ... (no implementation details available)
      └─ uses_service EntityRelationshipBuilder (InstancePerLifetimeScope)
        └─ method ImplementMaps [L68]
          └─ ... (no implementation details available)
      └─ uses_service FileMigrator (InstancePerLifetimeScope)
        └─ method BuildFile [L69]
          └─ ... (no implementation details available)
      └─ uses_service JournalMigrator (InstancePerLifetimeScope)
        └─ method BuildJournals [L75]
          └─ ... (no implementation details available)
      └─ uses_service MatchingRuleMigrator (InstancePerLifetimeScope)
        └─ method BuildMatchingRules [L74]
          └─ ... (no implementation details available)
      └─ uses_service SourceAccountMigrator (InstancePerLifetimeScope)
        └─ method BuildSourceAccounts [L73]
          └─ ... (no implementation details available)
      └─ uses_service SourceMigrator (InstancePerLifetimeScope)
        └─ method BuildSources [L70]
          └─ ... (no implementation details available)
      └─ uses_storage StorageService.GetStream [L64]

