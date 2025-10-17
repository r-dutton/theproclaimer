[web] GET /api/accounting/ledger/standard-charts  (Cirrus.Api.Controllers.Accounting.Ledger.StandardChartController.GetCombinedMasterStandardQuery)  [L180–L204] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardChartDto [L188]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L188]
  └─ query StandardChart [L188]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardChart writes=0 reads=1
    └─ mappings 1
      └─ StandardChartDto

