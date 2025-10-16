[web] PUT /api/accounting/ledger/accounts/movement-title/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateMovementTitle)  [L472–L478] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L475]
  └─ write Account [L475]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L475]
      └─ ... (no implementation details available)

