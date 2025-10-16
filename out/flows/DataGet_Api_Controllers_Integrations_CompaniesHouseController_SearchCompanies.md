[web] GET /api/companies-house/search/companies  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchCompanies)  [L39–L48] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchCompaniesQuery -> SearchCompaniesQueryHandler [L45]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchCompaniesQueryHandler.Handle [L23–L34]
  └─ impact_summary
    └─ requests 1
      └─ SearchCompaniesQuery
    └─ handlers 1
      └─ SearchCompaniesQueryHandler

