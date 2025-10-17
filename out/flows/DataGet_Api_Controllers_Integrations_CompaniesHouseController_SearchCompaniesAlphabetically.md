[web] GET /api/companies-house/search/alphabetical-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompaniesAlphabetically)  [L70–L79] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchCompaniesAlphabeticallyQuery -> SearchCompaniesAlphabeticallyQueryHandler [L76]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesAlphabeticallyQueryHandler.Handle [L23–L34]
  └─ impact_summary
    └─ requests 1
      └─ SearchCompaniesAlphabeticallyQuery
    └─ handlers 1
      └─ SearchCompaniesAlphabeticallyQueryHandler

