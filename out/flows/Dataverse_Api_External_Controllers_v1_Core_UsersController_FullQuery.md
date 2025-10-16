[web] GET /core/v1/users/detailed  (Dataverse.Api.External.Controllers.v1.Core.UsersController.FullQuery)  [L102–L108] status=200
  └─ calls UserRepository.ReadQuery [L105]
  └─ query User [L105]
  └─ uses_service UserRepository
    └─ method ReadQuery [L105]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

