[web] GET /api/companies-house/company/{companyNumber}/filing-history/{transactionId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyFilingHistoryItem)  [L193–L201] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyFilingHistoryItemQuery [L198]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyFilingHistoryItemQueryHandler.Handle [L19–L30]

