[web] GET /api/ui/workflow/tasks/find  (Dataverse.Api.Controllers.UI.Workflow.TasksController.FindTasks)  [L44–L57] [auth=Authentication.UserPolicy]
  └─ sends_request FindTasksQuery [L54]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindTasksQueryHandler.Handle [L42–L116]
      └─ calls TaskRepository.ReadQuery [L59]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L68]
      └─ uses_service UserService
        └─ method GetUserId [L68]

