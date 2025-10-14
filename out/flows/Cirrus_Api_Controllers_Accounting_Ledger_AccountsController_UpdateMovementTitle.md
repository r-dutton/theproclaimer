[web] PUT /api/accounting/ledger/accounts/movement-title/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateMovementTitle)  [L472–L478] [auth=user]
  └─ calls AccountRepository.WriteQuery [L475]
  └─ writes_to Account [L475]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L475]

