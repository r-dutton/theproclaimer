[web] GET /api/companies-house/company/{companyNumber}/charges/{chargeId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyCharge)  [L171–L179] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyChargeQuery -> GetCompanyChargeQueryHandler [L176]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyChargeQueryHandler.Handle [L16–L26]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyChargeQuery
    └─ handlers 1
      └─ GetCompanyChargeQueryHandler

