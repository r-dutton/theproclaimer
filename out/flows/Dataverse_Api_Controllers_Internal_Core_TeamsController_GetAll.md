[web] GET /api/internal/teams/audit  (Dataverse.Api.Controllers.Internal.Core.TeamsController.GetAll)  [L42–L46] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.ReadQuery [L45]
  └─ queries Team [L45]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L45]

