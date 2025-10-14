[web] GET /api/companies-house/company/{companyNumber}/filing-history  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistory)  [L181–L191] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyFilingHistoryQuery [L188]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryQueryHandler.Handle [L23–L35]

