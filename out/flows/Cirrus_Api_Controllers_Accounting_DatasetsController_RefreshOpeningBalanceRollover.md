[web] PUT /api/accounting/datasets/{id}/refresh-opening-balance-rollover  (Cirrus.Api.Controllers.Accounting.DatasetsController.RefreshOpeningBalanceRollover)  [L177–L191] status=200 [auth=user]
  └─ calls DatasetRepository.WriteQuery [L183]
  └─ write Dataset [L183]
  └─ sends_request RefreshRolledOverOpeningBalances -> RefreshRolledOverOpeningBalancesHandler [L189]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.RefreshRolledOverOpeningBalancesHandler.Handle [L52–L253]
      └─ calls JournalRepository (methods: Remove,WriteQueryWithArchived,Add,WriteQuery) [L250]
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L138]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L134]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.ExecuteForRollover
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L118]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L92]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L184]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L127]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Dataset writes=1 reads=0
    └─ requests 3
      └─ CanIAccessDatasetQuery
      └─ GetTrialBalanceForDatesQuery
      └─ RefreshRolledOverOpeningBalances
    └─ handlers 3
      └─ CanIAccessDatasetQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
      └─ RefreshRolledOverOpeningBalancesHandler
    └─ caches 1
      └─ IMemoryCache

