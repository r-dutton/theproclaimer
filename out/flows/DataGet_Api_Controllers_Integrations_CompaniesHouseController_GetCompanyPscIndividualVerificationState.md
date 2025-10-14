[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}/verification-state  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualVerificationState)  [L304–L312] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualVerificationStateQuery [L309]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualVerificationStateQueryHandler.Handle [L19–L31]

