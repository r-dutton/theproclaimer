[web] PUT /api/accounting/ledger/master-accounts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.MasterAccountController.PutHeader)  [L42–L47] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MasterAccountRepository.WriteQuery [L45]
  └─ write MasterAccount [L45]
    └─ reads_from MasterAccounts
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MasterAccount writes=1 reads=0

