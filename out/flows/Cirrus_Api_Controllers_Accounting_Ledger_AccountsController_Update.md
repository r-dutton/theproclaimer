[web] PUT /api/accounting/ledger/accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Update)  [L355–L371] [auth=user]
  └─ calls AccountRepository.WriteQuery [L358]
  └─ writes_to Account [L358]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L358]

