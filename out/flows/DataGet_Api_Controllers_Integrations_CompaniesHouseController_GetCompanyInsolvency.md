[web] GET /api/companies-house/company/{companyNumber}/insolvency  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyInsolvency)  [L203–L211] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyInsolvencyQuery [L208]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyInsolvencyQueryHandler.Handle [L15–L24]

