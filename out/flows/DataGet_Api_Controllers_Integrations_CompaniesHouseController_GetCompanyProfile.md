[web] GET /api/companies-house/company/{companyNumber}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyProfile)  [L119–L127] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyProfileQuery -> GetCompanyProfileQueryHandler [L124]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyProfileQueryHandler.Handle [L15–L25]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyProfileQuery
    └─ handlers 1
      └─ GetCompanyProfileQueryHandler

