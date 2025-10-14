[web] GET /api/companies-house/search/officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchOfficers)  [L50–L58] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchOfficersQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchOfficersQueryHandler.Handle [L21–L32]

