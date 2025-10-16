[web] GET /api/companies-house/disqualified-officers/natural/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetNaturalDisqualification)  [L223–L231] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetNaturalDisqualificationQuery -> GetNaturalDisqualificationQueryHandler [L228]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetNaturalDisqualificationQueryHandler.Handle [L15–L24]
  └─ impact_summary
    └─ requests 1
      └─ GetNaturalDisqualificationQuery
    └─ handlers 1
      └─ GetNaturalDisqualificationQueryHandler

