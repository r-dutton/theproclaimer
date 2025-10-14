[web] GET /api/accounting/ledger/reports/{datasetId}/accounts  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetLedgerForAccounts)  [L101–L122] [auth=user]
  └─ sends_request CanIAccessDatasetQuery [L119]
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
  └─ sends_request GetGeneralLedgerByAccountQuery [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerByAccountQueryHandler.Handle [L56–L240]
      └─ maps_to AccountWithTransactionsDto [L82]
        └─ automapper.registration CirrusReportMappingProfile (Account->AccountWithTransactionsDto) [L133]
      └─ maps_to SourceAccountMappingDto [L193]
      └─ maps_to SourceLightDto [L176]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L181]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L89]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L144]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L82]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L129]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L87]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L201]

