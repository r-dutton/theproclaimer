[web] PUT /api/super/support-users/revoke  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.RevokeSupportUserAccess)  [L43–L48] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeSupportUserCommand [L46]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeSupportUserCommandHanlder.Handle [L34–L85]
      └─ calls TenantRepository.WriteTable [L55]
      └─ calls TenantRepository.WriteTable [L53]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L81]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

