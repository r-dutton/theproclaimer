[web] POST /api/super/users/{userId:Guid}/request-identity-account  (Dataverse.Api.Controllers.Super.Core.UsersController.Invite)  [L162–L170] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.WriteQuery [L166]
  └─ write User [L166]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L166]
      └─ ... (no implementation details available)

