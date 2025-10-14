[web] GET /api/companies-house/search/disqualified-officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchDisqualifiedOfficers)  [L60–L68] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchDisqualifiedOfficersQuery [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchDisqualifiedOfficersQueryHandler.Handle [L21–L32]

