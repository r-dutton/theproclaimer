[web] PUT /api/accounting/ledger/standard-accounts/child/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PutChild)  [L123–L128] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.WriteQuery [L126]
  └─ write StandardAccount [L126]
    └─ reads_from StandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ StandardAccount writes=1 reads=0

