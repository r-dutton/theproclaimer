[web] POST /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.CreateTenantUser)  [L57–L63] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateCentralUserCommand [L60]
    └─ handled_by Dataverse.Tenants.Commands.Users.CreateCentralUserCommandHandler.Handle [L25–L61]
      └─ calls TenantRepository.Add [L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

