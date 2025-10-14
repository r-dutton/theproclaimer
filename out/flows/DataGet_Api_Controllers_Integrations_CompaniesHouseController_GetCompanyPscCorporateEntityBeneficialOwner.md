[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntityBeneficialOwner)  [L264–L272] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscCorporateEntityBeneficialOwnerQuery [L269]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler.Handle [L19–L32]

