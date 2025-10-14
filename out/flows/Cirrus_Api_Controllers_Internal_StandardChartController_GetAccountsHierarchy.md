[web] GET /api/internal/ledger/standard-charts/{standardChartId:int}/accounts/hierarchy  (Cirrus.Api.Controllers.Internal.StandardChartController.GetAccountsHierarchy)  [L46–L71] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to StandardChartDto [L54]
    └─ automapper.registration ExternalApiMappingProfile (StandardChart->StandardChartDto) [L78]
    └─ automapper.registration CirrusMappingProfile (StandardChart->StandardChartDto) [L240]
  └─ calls StandardChartRepository.ReadQuery [L54]
  └─ queries StandardChart [L54]
    └─ reads_from StandardCharts
  └─ uses_service IControlledRepository<StandardChart>
    └─ method ReadQuery [L54]

