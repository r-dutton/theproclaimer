[web] GET /api/companies-house/search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompanies)  [L39–L48] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchCompaniesQuery [L45]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesQueryHandler.Handle [L23–L34]

