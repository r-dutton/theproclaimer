[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPerson)  [L334–L342] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscLegalPersonQuery [L339]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonQueryHandler.Handle [L19–L31]

