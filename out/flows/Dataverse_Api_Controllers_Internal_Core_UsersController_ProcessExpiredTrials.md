[web] POST /api/internal/users/process-expired-trials  (Dataverse.Api.Controllers.Internal.Core.UsersController.ProcessExpiredTrials)  [L199–L203] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ProcessExpiredTrialsCommand [L202]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ProcessExpiredTrialsCommandHandler.Handle [L22–L47]
      └─ calls TenantRepository.WriteTable [L33]

