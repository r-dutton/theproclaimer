[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/super-secure-beneficial-owner/{superSecureId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscSuperSecureBeneficialOwner)  [L354–L362] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscSuperSecureBeneficialOwnerQuery [L359]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscSuperSecureBeneficialOwnerQueryHandler.Handle [L19–L29]

