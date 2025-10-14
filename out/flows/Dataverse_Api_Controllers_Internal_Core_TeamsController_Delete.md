[web] DELETE /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Delete)  [L86–L93] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.WriteQuery [L89]
  └─ writes_to Team [L89]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L89]

