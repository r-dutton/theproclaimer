[web] POST /api/internal/users/process-expired-trials  (Dataverse.Api.Controllers.Internal.Core.UsersController.ProcessExpiredTrials)  [L199–L203] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ProcessExpiredTrialsCommand -> ProcessExpiredTrialsCommandHandler [L202]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ProcessExpiredTrialsCommandHandler.Handle [L22–L47]
      └─ calls TenantRepository.WriteTable [L33]
  └─ impact_summary
    └─ requests 1
      └─ ProcessExpiredTrialsCommand
    └─ handlers 1
      └─ ProcessExpiredTrialsCommandHandler

