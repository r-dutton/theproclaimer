[web] GET /api/journals/package  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetPackage)  [L186–L191] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetJournalPackageQuery -> GetJournalPackageQueryHandler [L190]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalPackageQueryHandler.Handle [L56–L244]
      └─ maps_to DivisionLightDto [L166]
        └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
      └─ maps_to StandardAccountLightListForJournalDto [L155]
        └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightListForJournalDto) [L447]
      └─ maps_to CachedSourceAccountDto [L134]
        └─ automapper.registration CirrusMappingProfile (CachedSourceAccount->CachedSourceAccountDto) [L287]
      └─ maps_to SourceAccountLightDto [L124]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
        └─ converts_to LinkedSourceAccount [L72]
        └─ converts_to SourceAccountInJournalModel [L269]
        └─ converts_to MappingSourceAccountModel [L830]
      └─ maps_to AccountLightListForJournalDto [L115]
        └─ automapper.registration CirrusMappingProfile (Account->AccountLightListForJournalDto) [L318]
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method ReadQuery [L177]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.ReadQuery
      └─ uses_service IControlledRepository<Division> (Scoped (inferred))
        └─ method ReadQuery [L166]
          └─ implementation Cirrus.Data.Repository.Accounting.Setup.DivisionRepository.ReadQuery
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L155]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L134]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L124]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L115]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L99]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L95]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ returns JournalPackageDto [L190] [return]
  └─ impact_summary
    └─ requests 1
      └─ GetJournalPackageQuery
    └─ handlers 1
      └─ GetJournalPackageQueryHandler
    └─ mappings 6
      └─ AccountLightListForJournalDto
      └─ CachedSourceAccountDto
      └─ DivisionLightDto
      └─ JournalPackageDto
      └─ SourceAccountLightDto
      └─ +1 more

