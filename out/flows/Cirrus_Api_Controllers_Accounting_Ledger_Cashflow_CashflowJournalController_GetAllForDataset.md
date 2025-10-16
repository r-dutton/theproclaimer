[web] GET /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetAllForDataset)  [L55–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CashflowJournalLightDto [L59]
    └─ automapper.registration CirrusMappingProfile (CashflowJournal->CashflowJournalLightDto) [L509]
  └─ calls CashflowJournalRepository.ReadQuery [L59]
  └─ query CashflowJournal [L59]
    └─ reads_from CashflowJournals
  └─ uses_service IControlledRepository<CashflowJournal>
    └─ method ReadQuery [L59]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L58]
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

