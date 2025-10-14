[web] GET /api/companies-house/company/{companyNumber}/persons-with-significant-control/individual/{pscId}/full-record  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyPscIndividualFullRecord)  [L314–L322] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyPscIndividualFullRecordQuery [L319]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyPscIndividualFullRecordQueryHandler.Handle [L19–L31]

