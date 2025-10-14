[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control-statements/{statementId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscStatement)  [L344–L352] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscStatementQuery [L349]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscStatementQueryHandler.Handle [L19–L29]

