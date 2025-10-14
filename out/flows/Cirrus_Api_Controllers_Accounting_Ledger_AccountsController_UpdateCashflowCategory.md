[web] PUT /api/accounting/ledger/accounts/cashflow-category/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateCashflowCategory)  [L448–L454] [auth=user]
  └─ calls AccountRepository.WriteQuery [L451]
  └─ writes_to Account [L451]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L451]

