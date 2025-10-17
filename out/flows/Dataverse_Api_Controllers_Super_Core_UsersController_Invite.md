[web] POST /api/super/users/{userId:Guid}/request-identity-account  (Dataverse.Api.Controllers.Super.Core.UsersController.Invite)  [L162–L170] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.WriteQuery [L166]
  └─ write User [L166]
  └─ uses_service UserRepository
    └─ method WriteQuery [L166]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.WriteQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository

