[web] GET /api/accounting/ledger/accounts/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Get)  [L68–L74] status=200 [auth=user]
  └─ maps_to AccountDto [L71]
    └─ automapper.registration CirrusMappingProfile (Account->AccountDto) [L327]
  └─ calls AccountRepository.ReadQuery [L71]
  └─ query Account [L71]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L71]
      └─ ... (no implementation details available)

