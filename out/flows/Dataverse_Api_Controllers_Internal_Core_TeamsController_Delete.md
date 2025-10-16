[web] DELETE /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Delete)  [L86–L93] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.WriteQuery [L89]
  └─ write Team [L89]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L89]
      └─ ... (no implementation details available)

