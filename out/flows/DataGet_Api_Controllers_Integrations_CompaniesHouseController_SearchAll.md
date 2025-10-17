[web] GET /api/companies-house/search/all  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAll)  [L29–L37] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchAllQuery -> SearchAllQueryHandler [L34]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAllQueryHandler.Handle [L17–L28]
  └─ impact_summary
    └─ requests 1
      └─ SearchAllQuery
    └─ handlers 1
      └─ SearchAllQueryHandler

