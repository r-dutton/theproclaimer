[web] POST /api/accounting/ledger/accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.Create)  [L273–L282] status=201 [auth=user]
  └─ calls AccountRepository (methods: Add,WriteQuery) [L280]
  └─ insert Account [L280]
  └─ write Account [L276]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Account writes=2 reads=0

