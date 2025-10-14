[web] GET /api/super/users/{id:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.UsersController.GetUserAudit)  [L184–L210] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L188]
  └─ queries User [L188]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L188]

