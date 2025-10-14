[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/corporate-entity/{pscId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscCorporateEntity)  [L274–L282] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscCorporateEntityQuery [L279]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscCorporateEntityQueryHandler.Handle [L19–L31]

