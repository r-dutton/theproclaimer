[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetEligibleSourceAccounts)  [L69–L89] status=200 [auth=user]
  └─ maps_to SourceAccountLightDto [L80]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountLightDto) [L617]
    └─ converts_to SourceAccountInJournalModel [L269]
    └─ converts_to LinkedSourceAccount [L72]
    └─ converts_to MappingSourceAccountModel [L830]
  └─ calls DatasetRepository.ReadQuery [L73]
  └─ calls SourceAccountRepository.ReadQuery [L80]
  └─ query SourceAccount [L80]
    └─ reads_from SourceAccounts
  └─ query Dataset [L73]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L72]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]

