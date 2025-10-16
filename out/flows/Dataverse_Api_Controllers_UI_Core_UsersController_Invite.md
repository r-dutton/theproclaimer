[web] POST /api/ui/users/{userId:guid}/request-identity-account  (Dataverse.Api.Controllers.UI.Core.UsersController.Invite)  [L326–L334] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.WriteQuery [L330]
  └─ write User [L330]
  └─ uses_service UserRepository
    └─ method WriteQuery [L330]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.WriteQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository

