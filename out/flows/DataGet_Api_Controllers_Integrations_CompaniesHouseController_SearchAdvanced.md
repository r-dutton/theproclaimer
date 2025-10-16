[web] GET /api/companies-house/search/advanced-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAdvanced)  [L93–L107] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchAdvancedQuery -> SearchAdvancedQueryHandler [L104]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAdvancedQueryHandler.Handle [L40–L50]
  └─ impact_summary
    └─ requests 1
      └─ SearchAdvancedQuery
    └─ handlers 1
      └─ SearchAdvancedQueryHandler

