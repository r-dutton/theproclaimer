[web] PUT /api/accounting/ledger/accounts/mark-consolidated/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.MarkAccountConsolidated)  [L377–L384] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L380]
  └─ write Account [L380]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L380]
      └─ ... (no implementation details available)

