[web] GET /api/companies-house/company/{companyNumber}/officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficers)  [L129–L139] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyOfficersQuery [L136]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficersQueryHandler.Handle [L27–L38]

