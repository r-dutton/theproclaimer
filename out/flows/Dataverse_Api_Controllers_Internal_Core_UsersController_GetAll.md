[web] GET /api/internal/users  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAll)  [L83–L90] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserLightDto [L86]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
  └─ calls UserRepository.ReadQuery [L86]
  └─ query User [L86]
  └─ uses_service UserRepository
    └─ method ReadQuery [L86]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserLightDto

