[web] PUT /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Update)  [L224–L229] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateOfficeWithUsersCommand [L227]
    └─ handled_by Dataverse.ApplicationService.Commands.Offices.UpdateOfficeWithUsersCommandHandler.Handle [L30–L127]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L51]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

