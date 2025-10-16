[web] GET /api/internal/teams/audit  (Dataverse.Api.Controllers.Internal.Core.TeamsController.GetAll)  [L42–L46] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.ReadQuery [L45]
  └─ query Team [L45]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L45]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository

