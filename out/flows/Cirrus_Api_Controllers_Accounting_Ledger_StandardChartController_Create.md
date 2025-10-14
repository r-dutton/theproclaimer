[web] POST /api/accounting/ledger/standard-charts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Create)  [L153–L158] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository.Add [L157]
  └─ writes_to StandardChart [L157]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method Add [L157]

