[web] DELETE /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Delete)  [L119–L129] [auth=user]
  └─ calls PrimaryProductionConfigRepository.Remove [L128]
  └─ calls PrimaryProductionConfigRepository.WriteQuery [L124]
  └─ writes_to PrimaryProductionConfig [L128]
    └─ reads_from PrimaryProductionConfigs
  └─ writes_to PrimaryProductionConfig [L124]
    └─ reads_from PrimaryProductionConfigs
  └─ uses_service IControlledRepository<PrimaryProductionConfig>
    └─ method WriteQuery [L124]
  └─ sends_request CanIAccessDatasetQuery [L122]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
      └─ uses_cache IDistributedCache [L116]
        └─ method SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache [L106]
        └─ method DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache [L103]
        └─ method CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache [L90]
        └─ method DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache [L88]
        └─ method CreateDatasetLockKey [write] [L88]

