[web] GET /ledger/v1/standard-charts/{standardChartId:int}  (Cirrus.Api.External.Controllers.v1.Ledger.StandardChartsController.Get)  [L45–L51] status=200
  └─ maps_to StandardChartDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L48]
  └─ query StandardChart [L48]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardChart writes=0 reads=1
    └─ mappings 1
      └─ StandardChartDto

