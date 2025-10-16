[web] GET /api/accounting/ledger/standard-charts/  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetAll)  [L63–L72] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardChartDto [L66]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L66]
  └─ query StandardChart [L66]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardChart writes=0 reads=1
    └─ mappings 1
      └─ StandardChartDto

