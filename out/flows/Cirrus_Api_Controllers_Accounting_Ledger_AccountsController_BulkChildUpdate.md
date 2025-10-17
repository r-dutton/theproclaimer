[web] PUT /api/accounting/ledger/accounts/bulk-child-update  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.BulkChildUpdate)  [L302–L350] status=200 [auth=user]
  └─ calls AccountRepository (methods: ReadQuery,WriteQuery) [L320]
  └─ query Account [L320]
  └─ write Account [L305]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ Account writes=1 reads=1

