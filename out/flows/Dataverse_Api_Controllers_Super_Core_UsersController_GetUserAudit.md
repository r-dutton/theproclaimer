[web] GET /api/super/users/{id:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.UsersController.GetUserAudit)  [L184–L210] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L188]
  └─ query User [L188]
  └─ uses_service UserRepository
    └─ method ReadQuery [L188]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

