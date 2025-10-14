[web] POST /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.CreateTenantUser)  [L57–L63] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateCentralUserCommand [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Tenants.Commands.Users.CreateCentralUserCommandHandler.Handle [L25–L61]
      └─ calls TenantRepository.Add [L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]

