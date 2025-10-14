[web] GET /api/companies-house/company/{companyNumber}/charges  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharges)  [L161–L169] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyChargesQuery [L166]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargesQueryHandler.Handle [L21–L33]

