[web] GET /api/companies-house/company/{companyNumber}/filing-history/{transactionId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistoryItem)  [L193–L201] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyFilingHistoryItemQuery -> GetCompanyFilingHistoryItemQueryHandler [L198]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryItemQueryHandler.Handle [L19–L30]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyFilingHistoryItemQuery
    └─ handlers 1
      └─ GetCompanyFilingHistoryItemQueryHandler

