[web] DELETE /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Delete)  [L173–L178] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository.Remove [L177]
  └─ calls StandardChartRepository.WriteQuery [L176]
  └─ writes_to StandardChart [L177]
    └─ reads_from StandardCharts
  └─ writes_to StandardChart [L176]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method WriteQuery [L176]

