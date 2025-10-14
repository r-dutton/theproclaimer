[web] GET /api/internal/users/audit  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAudit)  [L77–L81] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L80]
  └─ queries User [L80]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L80]

