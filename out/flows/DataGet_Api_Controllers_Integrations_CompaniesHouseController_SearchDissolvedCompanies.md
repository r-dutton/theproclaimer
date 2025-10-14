[web] GET /api/companies-house/search/dissolved-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDissolvedCompanies)  [L81–L91] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchDissolvedCompaniesQuery [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDissolvedCompaniesQueryHandler.Handle [L27–L40]

