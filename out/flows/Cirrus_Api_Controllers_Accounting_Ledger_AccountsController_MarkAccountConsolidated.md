[web] PUT /api/accounting/ledger/accounts/mark-consolidated/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.MarkAccountConsolidated)  [L377–L384] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L380]
  └─ write Account [L380]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Account writes=1 reads=0

