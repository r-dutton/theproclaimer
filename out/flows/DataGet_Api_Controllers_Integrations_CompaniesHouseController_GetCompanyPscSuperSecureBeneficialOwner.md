[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/super-secure-beneficial-owner/{superSecureId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscSuperSecureBeneficialOwner)  [L354–L362] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscSuperSecureBeneficialOwnerQuery -> GetCompanyPscSuperSecureBeneficialOwnerQueryHandler [L359]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscSuperSecureBeneficialOwnerQueryHandler.Handle [L19–L29]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscSuperSecureBeneficialOwnerQuery
    └─ handlers 1
      └─ GetCompanyPscSuperSecureBeneficialOwnerQueryHandler

