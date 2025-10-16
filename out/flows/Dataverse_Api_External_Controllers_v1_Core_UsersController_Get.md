[web] GET /core/v1/users/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Get)  [L67–L72] status=200
  └─ maps_to UserDto [L70]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
  └─ calls UserRepository.ReadQuery [L70]
  └─ query User [L70]
  └─ uses_service UserRepository
    └─ method ReadQuery [L70]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserDto

