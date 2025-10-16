[web] GET /api/companies-house/disqualified-officers/corporate/{officerId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCorporateDisqualification)  [L233–L241] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCorporateDisqualificationQuery -> GetCorporateDisqualificationQueryHandler [L238]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCorporateDisqualificationQueryHandler.Handle [L15–L24]
  └─ impact_summary
    └─ requests 1
      └─ GetCorporateDisqualificationQuery
    └─ handlers 1
      └─ GetCorporateDisqualificationQueryHandler

