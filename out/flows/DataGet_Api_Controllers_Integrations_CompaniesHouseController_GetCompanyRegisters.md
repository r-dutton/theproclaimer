[web] GET /api/companies-house/company/{companyNumber}/registers  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisters)  [L151–L159] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyRegistersQuery -> GetCompanyRegistersQueryHandler [L156]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegistersQueryHandler.Handle [L15–L24]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyRegistersQuery
    └─ handlers 1
      └─ GetCompanyRegistersQueryHandler

