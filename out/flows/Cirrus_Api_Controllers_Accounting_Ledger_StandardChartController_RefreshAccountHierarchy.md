[web] GET /api/accounting/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.RefreshAccountHierarchy)  [L120–L148] status=200 [auth=Authentication.UserPolicy]
  └─ calls StandardAccountRepository.ReadQuery [L138]
  └─ calls MasterAccountRepository.ReadQuery [L135]
  └─ query StandardAccount [L138]
    └─ reads_from StandardAccounts
  └─ query MasterAccount [L135]
    └─ reads_from MasterAccounts
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ MasterAccount writes=0 reads=1
      └─ StandardAccount writes=0 reads=1

