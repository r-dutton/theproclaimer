[web] POST /api/ui/workflow/jobs/{id:guid}/rollover  (Dataverse.Api.Controllers.UI.Workflow.JobController.RolloverJob)  [L171–L175] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request RolloverJobCommand [L174]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.RolloverJobCommandHandler.Handle [L58–L257]
      └─ uses_service JobParamsService
        └─ method GetDefaultStatus [L95]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetDefaultStatus [L24-L215]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L151]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

