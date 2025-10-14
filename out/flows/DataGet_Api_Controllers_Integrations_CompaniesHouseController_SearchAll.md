[web] GET /api/companies-house/search/all  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.SearchAll)  [L29–L37] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SearchAllQuery [L34]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.SearchAllQueryHandler.Handle [L17–L28]

