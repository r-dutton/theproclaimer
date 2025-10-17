[web] GET /api/internal/users/audit  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAudit)  [L77–L81] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L80]
  └─ query User [L80]
  └─ uses_service UserRepository
    └─ method ReadQuery [L80]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

