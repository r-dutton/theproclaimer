[web] GET /api/companies-house/company/{companyNumber}/appointments/{appointmentId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyOfficer)  [L141–L149] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyOfficerQuery [L146]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerQueryHandler.Handle [L16–L28]

