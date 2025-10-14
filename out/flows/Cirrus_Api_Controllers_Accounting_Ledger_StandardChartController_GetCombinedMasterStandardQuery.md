[web] GET /api/accounting/ledger/standard-charts  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetCombinedMasterStandardQuery)  [L180–L204] [auth=Authentication.UserPolicy]
  └─ maps_to StandardChartDto [L188]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L188]
  └─ queries StandardChart [L188]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method ReadQuery [L188]

