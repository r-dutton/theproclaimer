[web] GET /api/companies-house/disqualified-officers/natural/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetNaturalDisqualification)  [L223–L231] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetNaturalDisqualificationQuery [L228]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetNaturalDisqualificationQueryHandler.Handle [L15–L24]

