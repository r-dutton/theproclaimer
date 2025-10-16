[web] PUT /api/accounting/ledger/accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Update)  [L355–L371] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L358]
  └─ write Account [L358]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Account writes=1 reads=0

