[web] GET /api/ui/users/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUserAudit)  [L194–L219] [auth=Authentication.UserPolicy]
  └─ calls UserRepository.ReadQuery [L197]
  └─ queries User [L197]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L197]

