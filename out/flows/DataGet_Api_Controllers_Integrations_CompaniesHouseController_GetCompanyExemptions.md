[web] GET /api/companies-house/company/{companyNumber}/exemptions  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyExemptions)  [L213–L221] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyExemptionsQuery [L218]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyExemptionsQueryHandler.Handle [L15–L24]

