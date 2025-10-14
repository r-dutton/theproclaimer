[web] PUT /api/ui/workflow/deliverables/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.UpdateDeliverable)  [L110–L118] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L115]
  └─ uses_service IMapper
    └─ method Map [L115]
  └─ sends_request UpdateDeliverableCommand [L113]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.UpdateDeliverableCommandHandler.Handle [L25–L51]
      └─ uses_service IControlledRepository<Deliverable>
        └─ method WriteQuery [L40]
      └─ uses_service JobParamsService
        └─ method GetDeliverableParams [L46]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L44]

