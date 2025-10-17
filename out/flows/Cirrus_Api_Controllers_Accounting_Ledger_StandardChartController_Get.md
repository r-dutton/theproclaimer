[web] GET /api/accounting/ledger/standard-charts/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.Get)  [L52–L58] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardChartDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L55]
  └─ query StandardChart [L55]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardChart writes=0 reads=1
    └─ mappings 1
      └─ StandardChartDto

