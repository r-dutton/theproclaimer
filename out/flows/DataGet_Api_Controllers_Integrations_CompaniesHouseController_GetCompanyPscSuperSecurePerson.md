[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/super-secure/{superSecureId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscSuperSecurePerson)  [L364–L372] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscSuperSecurePersonQuery -> GetCompanyPscSuperSecurePersonQueryHandler [L369]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscSuperSecurePersonQueryHandler.Handle [L19–L29]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscSuperSecurePersonQuery
    └─ handlers 1
      └─ GetCompanyPscSuperSecurePersonQueryHandler

