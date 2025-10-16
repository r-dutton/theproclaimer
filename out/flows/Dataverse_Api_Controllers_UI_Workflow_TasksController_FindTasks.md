[web] GET /api/ui/workflow/tasks/find  (Dataverse.Api.Controllers.UI.Workflow.TasksController.FindTasks)  [L44–L57] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindTasksQuery [L54]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindTasksQueryHandler.Handle [L42–L116]
      └─ calls TaskRepository.ReadQuery [L59]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L68]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L68]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

