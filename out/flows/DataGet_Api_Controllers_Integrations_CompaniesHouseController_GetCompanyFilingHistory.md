[web] GET /api/companies-house/company/{companyNumber}/filing-history  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistory)  [L181–L191] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyFilingHistoryQuery -> GetCompanyFilingHistoryQueryHandler [L188]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryQueryHandler.Handle [L23–L35]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyFilingHistoryQuery
    └─ handlers 1
      └─ GetCompanyFilingHistoryQueryHandler

