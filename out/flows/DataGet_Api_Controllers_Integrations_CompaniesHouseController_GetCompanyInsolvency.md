[web] GET /api/companies-house/company/{companyNumber}/insolvency  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyInsolvency)  [L203–L211] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyInsolvencyQuery -> GetCompanyInsolvencyQueryHandler [L208]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyInsolvencyQueryHandler.Handle [L15–L24]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyInsolvencyQuery
    └─ handlers 1
      └─ GetCompanyInsolvencyQueryHandler

