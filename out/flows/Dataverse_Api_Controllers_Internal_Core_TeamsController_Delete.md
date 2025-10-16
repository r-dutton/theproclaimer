[web] DELETE /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Delete)  [L86–L93] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TeamRepository.WriteQuery [L89]
  └─ write Team [L89]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method WriteQuery [L89]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.WriteQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Team writes=1 reads=0
    └─ services 1
      └─ TeamRepository

