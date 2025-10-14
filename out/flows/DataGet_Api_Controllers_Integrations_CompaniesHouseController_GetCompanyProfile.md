[web] GET /api/companies-house/company/{companyNumber}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyProfile)  [L119–L127] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyProfileQuery [L124]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyProfileQueryHandler.Handle [L15–L25]

