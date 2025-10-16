[web] GET /api/accounting/ledger/reports/{datasetId}/accounts  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetLedgerForAccounts)  [L101–L122] status=200 [auth=user]
  └─ sends_request GetGeneralLedgerByAccountQuery -> GetGeneralLedgerByAccountQueryHandler [L120]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerByAccountQueryHandler.Handle [L56–L240]
      └─ maps_to SourceAccountMappingDto [L193]
      └─ maps_to SourceLightDto [L176]
      └─ maps_to AccountWithTransactionsDto [L82]
        └─ automapper.registration CirrusReportMappingProfile (Account->AccountWithTransactionsDto) [L133]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L201]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L181]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L144]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L129]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L89]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQueryHandler.Handle [L38–L87]
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L82]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L119]
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
    └─ requests 4
      └─ CanIAccessDatasetQuery
      └─ GetGeneralLedgerByAccountQuery
      └─ GetGeneralLedgerForDatesQuery
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 4
      └─ CanIAccessDatasetQueryHandler
      └─ GetGeneralLedgerByAccountQueryHandler
      └─ GetGeneralLedgerForDatesQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ AccountWithTransactionsDto
      └─ SourceAccountMappingDto
      └─ SourceLightDto

