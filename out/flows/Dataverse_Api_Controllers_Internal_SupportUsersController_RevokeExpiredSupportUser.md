[web] PUT /api/internal/support-users/revoke  (Dataverse.Api.Controllers.Internal.SupportUsersController.RevokeExpiredSupportUser)  [L38–L44] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeExpiredSupportUserCommand -> RevokeExpiredSupportUserCommandHandler [L41]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeExpiredSupportUserCommandHandler.Handle [L19–L42]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L33]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ RevokeExpiredSupportUserCommand
    └─ handlers 1
      └─ RevokeExpiredSupportUserCommandHandler

