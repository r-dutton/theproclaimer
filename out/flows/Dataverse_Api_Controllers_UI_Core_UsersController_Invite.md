[web] POST /api/ui/users/{userId:guid}/request-identity-account  (Dataverse.Api.Controllers.UI.Core.UsersController.Invite)  [L326–L334] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.WriteQuery [L330]
  └─ writes_to User [L330]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L330]

