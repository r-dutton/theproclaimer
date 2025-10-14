[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscList)  [L374–L384] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscListQuery [L381]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscListQueryHandler.Handle [L26–L38]

