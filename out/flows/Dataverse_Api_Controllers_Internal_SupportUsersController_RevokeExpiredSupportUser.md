[web] PUT /api/internal/support-users/revoke  (Dataverse.Api.Controllers.Internal.SupportUsersController.RevokeExpiredSupportUser)  [L38–L44] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeExpiredSupportUserCommand [L41]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeExpiredSupportUserCommandHandler.Handle [L19–L42]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L33]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

