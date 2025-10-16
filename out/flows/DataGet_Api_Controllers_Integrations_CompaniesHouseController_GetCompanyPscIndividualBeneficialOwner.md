[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual-beneficial-owner/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualBeneficialOwner)  [L284–L292] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualBeneficialOwnerQuery -> GetCompanyPscIndividualBeneficialOwnerQueryHandler [L289]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualBeneficialOwnerQueryHandler.Handle [L19–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscIndividualBeneficialOwnerQuery
    └─ handlers 1
      └─ GetCompanyPscIndividualBeneficialOwnerQueryHandler

