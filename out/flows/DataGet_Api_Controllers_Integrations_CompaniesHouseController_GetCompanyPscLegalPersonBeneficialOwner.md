[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPersonBeneficialOwner)  [L324–L332] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscLegalPersonBeneficialOwnerQuery [L329]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonBeneficialOwnerQueryHandler.Handle [L19–L31]

