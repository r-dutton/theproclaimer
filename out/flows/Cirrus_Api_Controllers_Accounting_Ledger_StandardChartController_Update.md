[web] PUT /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Update)  [L163–L168] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardChartRepository.WriteQuery [L166]
  └─ write StandardChart [L166]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method WriteQuery [L166]
      └─ ... (no implementation details available)

