[web] PUT /api/accounting/datasets/{id}/refresh-opening-balance-rollover  (Cirrus.Api.Controllers.Accounting.DatasetsController.RefreshOpeningBalanceRollover)  [L177–L191] [auth=user]
  └─ calls DatasetRepository.WriteQuery [L183]
  └─ writes_to Dataset [L183]
  └─ uses_service IControlledRepository<Dataset>
    └─ method WriteQuery [L183]
  └─ sends_request RefreshRolledOverOpeningBalances [L189]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.RefreshRolledOverOpeningBalancesHandler.Handle [L52–L253]
      └─ calls JournalRepository.Remove [L250]
      └─ calls JournalRepository.WriteQueryWithArchived [L242]
      └─ calls JournalRepository.Add [L235]
      └─ calls JournalRepository.WriteQuery [L224]
      └─ calls JournalRepository.WriteQuery [L213]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L134]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L138]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L118]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L92]
  └─ sends_request CanIAccessDatasetQuery [L184]
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

