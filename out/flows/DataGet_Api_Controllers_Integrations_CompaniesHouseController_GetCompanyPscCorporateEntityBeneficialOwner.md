[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntityBeneficialOwner)  [L264–L272] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscCorporateEntityBeneficialOwnerQuery -> GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler [L269]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler.Handle [L19–L32]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscCorporateEntityBeneficialOwnerQuery
    └─ handlers 1
      └─ GetCompanyPscCorporateEntityBeneficialOwnerQueryHandler

