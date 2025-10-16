[web] GET /api/ui/users/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUserAudit)  [L194–L219] status=200 [auth=Authentication.UserPolicy]
  └─ calls UserRepository.ReadQuery [L197]
  └─ query User [L197]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L197]
      └─ ... (no implementation details available)

