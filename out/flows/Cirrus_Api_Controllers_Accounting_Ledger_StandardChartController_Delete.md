[web] DELETE /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Delete)  [L173–L178] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository (methods: Remove,WriteQuery) [L177]
  └─ delete StandardChart [L177]
    └─ reads_from StandardCharts
  └─ write StandardChart [L176]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ StandardChart writes=2 reads=0

