[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control-statements  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscStatementsList)  [L386–L396] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscStatementsListQuery [L393]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscStatementsListQueryHandler.Handle [L26–L38]

