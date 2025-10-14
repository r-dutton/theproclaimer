[web] PUT /api/accounting/ledger/accounts/primary-production/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdatePrimaryProductionCostType)  [L417–L425] [auth=user]
  └─ calls AccountRepository.WriteQuery [L423]
  └─ writes_to Account [L423]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L423]

