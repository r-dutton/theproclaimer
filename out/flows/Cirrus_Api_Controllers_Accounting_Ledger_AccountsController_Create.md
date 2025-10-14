[web] POST /api/accounting/ledger/accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Create)  [L273–L282] [auth=user]
  └─ calls AccountRepository.Add [L280]
  └─ calls AccountRepository.WriteQuery [L276]
  └─ writes_to Account [L280]
  └─ writes_to Account [L276]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L276]

