[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetEligibleSourceAccounts)  [L69–L89] [auth=user]
  └─ maps_to SourceAccountLightDto [L80]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountLightDto) [L617]
    └─ converts_to SourceAccountInJournalModel [L269]
    └─ converts_to LinkedSourceAccount [L72]
    └─ converts_to MappingSourceAccountModel [L830]
  └─ calls DatasetRepository.ReadQuery [L73]
  └─ calls SourceAccountRepository.ReadQuery [L80]
  └─ queries SourceAccount [L80]
    └─ reads_from SourceAccounts
  └─ queries Dataset [L73]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L73]
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L80]
  └─ sends_request CanIAccessDatasetQuery [L72]
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

