[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}/verification-state  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualVerificationState)  [L304–L312] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualVerificationStateQuery -> GetCompanyPscIndividualVerificationStateQueryHandler [L309]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualVerificationStateQueryHandler.Handle [L19–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscIndividualVerificationStateQuery
    └─ handlers 1
      └─ GetCompanyPscIndividualVerificationStateQueryHandler

