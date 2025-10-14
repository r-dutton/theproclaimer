[web] GET /api/companies-house/search/advanced-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAdvanced)  [L93–L107] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchAdvancedQuery [L104]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAdvancedQueryHandler.Handle [L40–L50]

