[web] GET /api/companies-house/search/disqualified-officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDisqualifiedOfficers)  [L60–L68] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchDisqualifiedOfficersQuery -> SearchDisqualifiedOfficersQueryHandler [L65]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDisqualifiedOfficersQueryHandler.Handle [L21–L32]
  └─ impact_summary
    └─ requests 1
      └─ SearchDisqualifiedOfficersQuery
    └─ handlers 1
      └─ SearchDisqualifiedOfficersQueryHandler

