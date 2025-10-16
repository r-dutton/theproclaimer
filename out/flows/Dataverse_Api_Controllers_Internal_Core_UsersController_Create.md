[web] POST /api/internal/users/  (Dataverse.Api.Controllers.Internal.Core.UsersController.Create)  [L155–L161] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateUserCommand [L159]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.CreateOrLinkWithDataverseCommandHandler.Handle [L57–L149]
      └─ uses_service UserService
        └─ method IsInRole [L103]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]
      └─ uses_service EventPublisher (InstancePerLifetimeScope)
        └─ method PublishAllEventsForEntity [L145]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L111]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L86]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L87]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L141]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

