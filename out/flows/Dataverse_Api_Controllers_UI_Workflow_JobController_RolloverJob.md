[web] POST /api/ui/workflow/jobs/{id:guid}/rollover  (Dataverse.Api.Controllers.UI.Workflow.JobController.RolloverJob)  [L171–L175] [auth=Authentication.UserPolicy]
  └─ sends_request RolloverJobCommand [L174]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.RolloverJobCommandHandler.Handle [L58–L257]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L151]
      └─ uses_service JobParamsService
        └─ method GetDefaultStatus [L95]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]

