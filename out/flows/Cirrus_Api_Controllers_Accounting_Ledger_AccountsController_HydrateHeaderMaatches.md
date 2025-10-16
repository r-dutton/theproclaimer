[web] POST /api/accounting/ledger/accounts/header-matches/hydrate  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.HydrateHeaderMaatches)  [L253–L257] status=201 [auth=user]
  └─ sends_request GetHeaderMatchQuery -> GetHeaderMatchQueryHandler [L256]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetHeaderMatchQueryHandler.Handle [L24–L108]
      └─ maps_to AccountUltraLightDto [L100]
        └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
      └─ maps_to StandardAccountUltraLightDto [L74]
        └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountUltraLightDto) [L446]
      └─ maps_to MasterAccountDto [L58]
        └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountDto) [L301]
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L100]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L74]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
        └─ method ReadQuery [L58]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetHeaderMatchQuery
    └─ handlers 1
      └─ GetHeaderMatchQueryHandler
    └─ mappings 3
      └─ AccountUltraLightDto
      └─ MasterAccountDto
      └─ StandardAccountUltraLightDto

