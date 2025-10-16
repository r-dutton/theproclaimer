[web] GET /api/super/users/getAll  (Dataverse.Api.Controllers.Super.Core.UsersController.GetAll)  [L104–L113] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserLightDto [L108]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
  └─ calls UserRepository.ReadQuery [L108]
  └─ query User [L108]
  └─ uses_service UserRepository
    └─ method ReadQuery [L108]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserLightDto

