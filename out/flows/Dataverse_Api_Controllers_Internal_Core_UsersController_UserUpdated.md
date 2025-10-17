[web] PUT /api/internal/users/user-updated  (Dataverse.Api.Controllers.Internal.Core.UsersController.UserUpdated)  [L181–L192] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserWithIdentityInfoDto [L191]
  └─ calls UserRepository.WriteQuery [L186]
  └─ write User [L186]
  └─ uses_service UserRepository
    └─ method WriteQuery [L186]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.WriteQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserWithIdentityInfoDto

