[web] GET /api/accounting/tradingAccounts/{id}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.Get)  [L41–L47] [auth=user]
  └─ maps_to TradingAccountDto [L44]
    └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountDto) [L227]
  └─ calls TradingAccountRepository.ReadQuery [L44]
  └─ queries TradingAccount [L44]
    └─ reads_from TradingAccounts
  └─ uses_service IControlledRepository<TradingAccount>
    └─ method ReadQuery [L44]

