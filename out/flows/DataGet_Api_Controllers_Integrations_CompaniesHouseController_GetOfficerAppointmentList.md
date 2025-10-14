[web] GET /api/companies-house/officers/{officerId}/appointments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetOfficerAppointmentList)  [L243–L252] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetOfficerAppointmentsQuery [L249]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerAppointmentsQueryHandler.Handle [L23–L38]

