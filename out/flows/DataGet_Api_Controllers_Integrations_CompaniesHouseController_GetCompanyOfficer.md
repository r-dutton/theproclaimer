[web] GET /api/companies-house/company/{companyNumber}/appointments/{appointmentId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficer)  [L141–L149] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyOfficerQuery -> GetOfficerQueryHandler [L146]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerQueryHandler.Handle [L16–L28]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyOfficerQuery
    └─ handlers 1
      └─ GetOfficerQueryHandler

