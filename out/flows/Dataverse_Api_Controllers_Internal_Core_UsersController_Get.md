[web] GET /api/internal/users/{id}  (Dataverse.Api.Controllers.Internal.Core.UsersController.Get)  [L92–L98] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserDto [L95]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
  └─ calls UserRepository.ReadQuery [L95]
  └─ query User [L95]
  └─ uses_service UserRepository
    └─ method ReadQuery [L95]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserDto

