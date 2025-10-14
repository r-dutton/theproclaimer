[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualBeneficialOwner)  [L284–L292] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualBeneficialOwnerQuery [L289]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualBeneficialOwnerQueryHandler.Handle [L19–L31]

