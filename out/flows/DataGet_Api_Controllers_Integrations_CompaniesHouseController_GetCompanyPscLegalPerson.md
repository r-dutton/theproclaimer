[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/legal-person/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscLegalPerson)  [L334–L342] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscLegalPersonQuery [L339]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscLegalPersonQueryHandler.Handle [L19–L31]

