[web] GET /ledger/v1/standard-charts/{standardChartId:int}  (Cirrus.Api.External.Controllers.v1.Ledger.StandardChartsController.Get)  [L45–L51]
  └─ maps_to StandardChartDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L48]
  └─ queries StandardChart [L48]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method ReadQuery [L48]

