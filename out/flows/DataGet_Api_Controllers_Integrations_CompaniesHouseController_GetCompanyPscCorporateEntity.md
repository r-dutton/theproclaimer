[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntity)  [L274–L282] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscCorporateEntityQuery -> GetCompanyPscCorporateEntityQueryHandler [L279]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityQueryHandler.Handle [L19–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscCorporateEntityQuery
    └─ handlers 1
      └─ GetCompanyPscCorporateEntityQueryHandler

