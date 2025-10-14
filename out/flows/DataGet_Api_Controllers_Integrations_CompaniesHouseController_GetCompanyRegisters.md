[web] GET /api/companies-house/company/{companyNumber}/registers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisters)  [L151–L159] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyRegistersQuery [L156]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegistersQueryHandler.Handle [L15–L24]

