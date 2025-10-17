[web] DELETE /api/accounting/ledger/standard-accounts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.Delete)  [L194–L199] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository (methods: Remove,WriteQuery) [L198]
  └─ delete StandardAccount [L198]
    └─ reads_from StandardAccounts
  └─ write StandardAccount [L197]
    └─ reads_from StandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ StandardAccount writes=2 reads=0

