[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividual)  [L294–L302] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualQuery [L299]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualQueryHandler.Handle [L19–L31]

