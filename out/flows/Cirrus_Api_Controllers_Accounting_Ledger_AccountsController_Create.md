[web] POST /api/accounting/ledger/accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Create)  [L273–L282] status=201 [auth=user]
  └─ calls AccountRepository.Add [L280]
  └─ calls AccountRepository.WriteQuery [L276]
  └─ insert Account [L280]
  └─ write Account [L276]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L276]
      └─ ... (no implementation details available)

