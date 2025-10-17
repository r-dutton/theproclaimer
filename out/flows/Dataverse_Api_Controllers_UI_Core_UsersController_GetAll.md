[web] GET /api/ui/users/getAll  (Dataverse.Api.Controllers.UI.Core.UsersController.GetAll)  [L169–L177] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to UserLightDto [L172]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
  └─ calls UserRepository.ReadQuery [L172]
  └─ query User [L172]
  └─ uses_service UserRepository
    └─ method ReadQuery [L172]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserLightDto

