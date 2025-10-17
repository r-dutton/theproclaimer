[web] PUT /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Update)  [L163–L168] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository.WriteQuery [L166]
  └─ write StandardChart [L166]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ StandardChart writes=1 reads=0

