[web] PUT /api/accounting/ledger/accounts/movement-category/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateMovementCategory)  [L460–L466] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L463]
  └─ write Account [L463]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L463]
      └─ ... (no implementation details available)

