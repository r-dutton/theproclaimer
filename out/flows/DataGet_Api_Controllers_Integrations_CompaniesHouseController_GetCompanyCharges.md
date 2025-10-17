[web] GET /api/companies-house/company/{companyNumber}/charges  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharges)  [L161–L169] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyChargesQuery -> GetCompanyChargesQueryHandler [L166]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargesQueryHandler.Handle [L21–L33]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyChargesQuery
    └─ handlers 1
      └─ GetCompanyChargesQueryHandler

