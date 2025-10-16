[web] PUT /api/super/users/  (Dataverse.Api.Controllers.Super.Core.UsersController.UpdateDetails)  [L126–L132] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateUserCommand [L130]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateUserCommandHandler.Handle [L42–L111]
      └─ maps_to UserWithIdentityInfoDto [L95]
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L71]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L95]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L100]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

