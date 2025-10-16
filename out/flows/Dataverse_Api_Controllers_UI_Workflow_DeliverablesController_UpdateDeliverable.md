[web] PUT /api/ui/workflow/deliverables/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.UpdateDeliverable)  [L110–L118] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L115]
  └─ uses_service IMapper
    └─ method Map [L115]
      └─ ... (no implementation details available)
  └─ sends_request UpdateDeliverableCommand [L113]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.UpdateDeliverableCommandHandler.Handle [L25–L51]
      └─ uses_service JobParamsService
        └─ method GetDeliverableParams [L46]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetDeliverableParams [L24-L215]
      └─ uses_service IControlledRepository<Deliverable>
        └─ method WriteQuery [L40]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L44]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

