[web] PUT /api/accounting/ledger/accounts/primary-production/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdatePrimaryProductionCostType)  [L417–L425] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L423]
  └─ write Account [L423]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L423]
      └─ ... (no implementation details available)

