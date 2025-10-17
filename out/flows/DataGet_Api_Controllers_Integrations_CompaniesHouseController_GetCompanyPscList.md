[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscList)  [L374–L384] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscListQuery -> GetCompanyPscListQueryHandler [L381]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscListQueryHandler.Handle [L26–L38]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyPscListQuery
    └─ handlers 1
      └─ GetCompanyPscListQueryHandler

