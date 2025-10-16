[web] POST /api/ui/users/bulk/invite  (Dataverse.Api.Controllers.UI.Core.UsersController.BulkResendInvite)  [L292–L305] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request ProcessInviteCommand [L300]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ProcessInviteCommandHandler.Handle [L29–L65]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

