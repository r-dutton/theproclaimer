[web] POST /api/accounting/ledger/standard-charts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Create)  [L153–L158] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository.Add [L157]
  └─ insert StandardChart [L157]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ StandardChart writes=1 reads=0

