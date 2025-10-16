[web] GET /api/accounting/ledger/cashflow/journals/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Get)  [L40–L49] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CashflowJournalDto [L43]
    └─ automapper.registration CirrusMappingProfile (CashflowJournal->CashflowJournalDto) [L508]
  └─ calls CashflowJournalRepository.ReadQuery [L43]
  └─ query CashflowJournal [L43]
    └─ reads_from CashflowJournals
  └─ uses_service IControlledRepository<CashflowJournal>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L46]
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

