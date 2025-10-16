[web] GET /core/v1/users/  (Dataverse.Api.External.Controllers.v1.Core.UsersController.MinimalQuery)  [L84–L90] status=200
  └─ calls UserRepository.ReadQuery [L87]
  └─ query User [L87]
  └─ uses_service UserRepository
    └─ method ReadQuery [L87]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

