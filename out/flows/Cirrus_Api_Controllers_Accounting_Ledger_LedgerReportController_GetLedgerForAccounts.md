[web] GET /api/accounting/ledger/reports/{datasetId}/accounts  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetLedgerForAccounts)  [L101–L122] status=200 [auth=user]
  └─ sends_request CanIAccessDatasetQuery [L119]
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
  └─ sends_request GetGeneralLedgerByAccountQuery [L120]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerByAccountQueryHandler.Handle [L56–L240]
      └─ maps_to AccountWithTransactionsDto [L82]
        └─ automapper.registration CirrusReportMappingProfile (Account->AccountWithTransactionsDto) [L133]
      └─ maps_to SourceAccountMappingDto [L193]
      └─ maps_to SourceLightDto [L176]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L181]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L89]
          └─ ... (no implementation details available)
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L144]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L82]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L129]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L87]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L201]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

