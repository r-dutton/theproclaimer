[web] GET /api/ui/users/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUserAudit)  [L194–L219] status=200 [auth=Authentication.UserPolicy]
  └─ calls UserRepository.ReadQuery [L197]
  └─ query User [L197]
  └─ uses_service UserRepository
    └─ method ReadQuery [L197]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

