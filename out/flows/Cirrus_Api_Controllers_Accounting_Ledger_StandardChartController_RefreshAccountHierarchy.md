[web] GET /api/accounting/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.RefreshAccountHierarchy)  [L120–L148] [auth=Authentication.UserPolicy]
  └─ calls MasterAccountRepository.ReadQuery [L135]
  └─ calls StandardAccountRepository.ReadQuery [L138]
  └─ queries MasterAccount [L135]
    └─ reads_from MasterAccounts
  └─ queries StandardAccount [L138]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method ReadQuery [L135]
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L138]

