[web] GET /api/internal/users/audit  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAudit)  [L77–L81] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L80]
  └─ query User [L80]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)

