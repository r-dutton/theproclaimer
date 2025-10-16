[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPerson)  [L334–L342] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscLegalPersonQuery -> GetCompanyPscLegalPersonQueryHandler [L339]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonQueryHandler.Handle [L19–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscLegalPersonQuery
    └─ handlers 1
      └─ GetCompanyPscLegalPersonQueryHandler

