[web] GET /api/companies-house/search/dissolved-search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDissolvedCompanies)  [L81–L91] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchDissolvedCompaniesQuery -> SearchDissolvedCompaniesQueryHandler [L88]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDissolvedCompaniesQueryHandler.Handle [L27–L40]
  └─ impact_summary
    └─ requests 1
      └─ SearchDissolvedCompaniesQuery
    └─ handlers 1
      └─ SearchDissolvedCompaniesQueryHandler

