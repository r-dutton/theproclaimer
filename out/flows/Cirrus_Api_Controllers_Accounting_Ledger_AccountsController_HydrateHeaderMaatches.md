[web] POST /api/accounting/ledger/accounts/header-matches/hydrate  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.HydrateHeaderMaatches)  [L253–L257] status=201 [auth=user]
  └─ sends_request GetHeaderMatchQuery [L256]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetHeaderMatchQueryHandler.Handle [L24–L108]
      └─ maps_to AccountUltraLightDto [L100]
        └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
      └─ maps_to MasterAccountDto [L58]
        └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountDto) [L301]
        └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
      └─ maps_to StandardAccountUltraLightDto [L74]
        └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountUltraLightDto) [L446]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L100]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L60]
          └─ ... (no implementation details available)

