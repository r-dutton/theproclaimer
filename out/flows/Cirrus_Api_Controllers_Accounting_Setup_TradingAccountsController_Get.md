[web] GET /api/accounting/tradingAccounts/{id}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to TradingAccountDto [L44]
    └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountDto) [L227]
  └─ calls TradingAccountRepository.ReadQuery [L44]
  └─ query TradingAccount [L44]
    └─ reads_from TradingAccounts
  └─ uses_service IControlledRepository<TradingAccount>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

