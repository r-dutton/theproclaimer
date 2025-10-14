[web] GET /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetAllForDataset)  [L55–L65] [auth=Authentication.UserPolicy]
  └─ maps_to CashflowJournalLightDto [L59]
    └─ automapper.registration CirrusMappingProfile (CashflowJournal->CashflowJournalLightDto) [L509]
  └─ calls CashflowJournalRepository.ReadQuery [L59]
  └─ queries CashflowJournal [L59]
    └─ reads_from CashflowJournals
  └─ uses_service IControlledRepository<CashflowJournal>
    └─ method ReadQuery [L59]
  └─ sends_request CanIAccessDatasetQuery [L58]
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

