[web] GET /api/companies-house/officers/{officerId}/appointments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetOfficerAppointmentList)  [L243–L252] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetOfficerAppointmentsQuery -> GetOfficerAppointmentsQueryHandler [L249]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetOfficerAppointmentsQueryHandler.Handle [L23–L38]
  └─ impact_summary
    └─ requests 1
      └─ GetOfficerAppointmentsQuery
    └─ handlers 1
      └─ GetOfficerAppointmentsQueryHandler

