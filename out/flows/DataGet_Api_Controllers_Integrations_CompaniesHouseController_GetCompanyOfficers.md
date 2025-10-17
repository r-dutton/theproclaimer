[web] GET /api/companies-house/company/{companyNumber}/officers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficers)  [L129–L139] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyOfficersQuery -> GetOfficersQueryHandler [L136]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficersQueryHandler.Handle [L27–L38]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyOfficersQuery
    └─ handlers 1
      └─ GetOfficersQueryHandler

