[web] PUT /api/internal/deliverables/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverablesController.Update)  [L124–L132] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableDto [L129]
  └─ uses_service IMapper
    └─ method Map [L129]
  └─ sends_request UpdateDeliverableCommand [L127]
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

