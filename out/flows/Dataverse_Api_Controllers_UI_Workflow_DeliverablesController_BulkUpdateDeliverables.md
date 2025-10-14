[web] PUT /api/ui/workflow/deliverables/bulk  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.BulkUpdateDeliverables)  [L158–L167] [auth=Authentication.UserPolicy]
  └─ sends_request UpdateDeliverableCommand [L163]
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

