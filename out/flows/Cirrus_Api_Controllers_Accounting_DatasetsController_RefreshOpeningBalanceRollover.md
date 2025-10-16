[web] PUT /api/accounting/datasets/{id}/refresh-opening-balance-rollover  (Cirrus.Api.Controllers.Accounting.DatasetsController.RefreshOpeningBalanceRollover)  [L177–L191] status=200 [auth=user]
  └─ calls DatasetRepository.WriteQuery [L183]
  └─ write Dataset [L183]
  └─ uses_service IControlledRepository<Dataset>
    └─ method WriteQuery [L183]
      └─ ... (no implementation details available)
  └─ sends_request RefreshRolledOverOpeningBalances [L189]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.RefreshRolledOverOpeningBalancesHandler.Handle [L52–L253]
      └─ calls JournalRepository.Remove [L250]
      └─ calls JournalRepository.WriteQueryWithArchived [L242]
      └─ calls JournalRepository.Add [L235]
      └─ calls JournalRepository.WriteQuery [L224]
      └─ calls JournalRepository.WriteQuery [L213]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L134]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L138]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L118]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L92]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L184]
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

