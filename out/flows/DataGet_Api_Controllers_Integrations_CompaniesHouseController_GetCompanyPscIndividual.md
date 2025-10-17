[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividual)  [L294–L302] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualQuery -> GetCompanyPscIndividualQueryHandler [L299]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualQueryHandler.Handle [L19–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscIndividualQuery
    └─ handlers 1
      └─ GetCompanyPscIndividualQueryHandler

