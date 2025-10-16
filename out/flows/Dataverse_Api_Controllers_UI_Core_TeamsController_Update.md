[web] PUT /api/ui/teams/{id}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Update)  [L122–L127] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateTeamWithUsersCommand [L125]
    └─ handled_by Dataverse.ApplicationService.Commands.Teams.UpdateTeamWithUsersCommandHandler.Handle [L30–L127]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L51]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L65]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L77]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

