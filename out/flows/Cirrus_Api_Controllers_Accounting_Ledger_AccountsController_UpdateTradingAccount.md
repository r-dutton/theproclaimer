[web] PUT /api/accounting/ledger/accounts/trading-account/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateTradingAccount)  [L391–L399] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L394]
  └─ write Account [L394]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L394]
      └─ ... (no implementation details available)

