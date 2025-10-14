[web] GET /api/companies-house/search/alphabetical-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompaniesAlphabetically)  [L70–L79] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchCompaniesAlphabeticallyQuery [L76]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesAlphabeticallyQueryHandler.Handle [L23–L34]

