[web] GET /api/companies-house/disqualified-officers/corporate/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCorporateDisqualification)  [L233–L241] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCorporateDisqualificationQuery [L238]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCorporateDisqualificationQueryHandler.Handle [L15–L24]

