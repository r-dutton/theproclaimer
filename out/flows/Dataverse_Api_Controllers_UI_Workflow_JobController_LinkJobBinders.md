[web] PUT /api/ui/workflow/jobs/{id:guid}/update/binders  (Dataverse.Api.Controllers.UI.Workflow.JobController.LinkJobBinders)  [L204–L210] [auth=Authentication.UserPolicy]
  └─ sends_request UpdateJobBindersCommand [L207]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.UpdateJobBindersCommandHandler.Handle [L27–L59]
      └─ uses_service IControlledRepository<Job>
        └─ method WriteQuery [L45]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L47]

