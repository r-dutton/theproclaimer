[web] PUT /api/internal/support-users/revoke  (Dataverse.Api.Controllers.Internal.SupportUsersController.RevokeExpiredSupportUser)  [L38–L44] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeExpiredSupportUserCommand [L41]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeExpiredSupportUserCommandHandler.Handle [L19–L42]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L33]

