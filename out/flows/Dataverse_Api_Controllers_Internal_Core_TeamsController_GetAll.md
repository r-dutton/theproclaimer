[web] GET /api/internal/teams/audit  (Dataverse.Api.Controllers.Internal.Core.TeamsController.GetAll)  [L42–L46] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.ReadQuery [L45]
  └─ query Team [L45]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

