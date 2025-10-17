[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control-statements/{statementId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscStatement)  [L344–L352] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscStatementQuery -> GetCompanyPscStatementQueryHandler [L349]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscStatementQueryHandler.Handle [L19–L29]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscStatementQuery
    └─ handlers 1
      └─ GetCompanyPscStatementQueryHandler

