[web] GET /api/internal/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy  (Cirrus.Api.Controllers.Internal.StandardChartController.GetAccountsHierarchy)  [L46–L71] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to StandardChartDto [L54]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L54]
  └─ query StandardChart [L54]
    └─ reads_from StandardCharts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardChart writes=0 reads=1
    └─ mappings 1
      └─ StandardChartDto

