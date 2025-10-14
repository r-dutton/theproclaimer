[web] GET /api/companies-house/company/{companyNumber}/charges/{chargeId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharge)  [L171–L179] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyChargeQuery [L176]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargeQueryHandler.Handle [L16–L26]

