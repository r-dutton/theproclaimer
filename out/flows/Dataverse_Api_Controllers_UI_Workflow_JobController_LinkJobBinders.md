[web] PUT /api/ui/workflow/jobs/{id:guid}/update/binders  (Dataverse.Api.Controllers.UI.Workflow.JobController.LinkJobBinders)  [L204–L210] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request UpdateJobBindersCommand [L207]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.UpdateJobBindersCommandHandler.Handle [L27–L59]
      └─ uses_service IControlledRepository<Job>
        └─ method WriteQuery [L45]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L47]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

