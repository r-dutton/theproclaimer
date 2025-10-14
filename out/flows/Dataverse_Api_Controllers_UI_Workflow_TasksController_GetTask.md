[web] GET /api/ui/workflow/tasks/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetTask)  [L88–L95] [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskDto [L91]
  └─ calls TaskRepository.ReadQuery [L91]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L93]
  └─ uses_service TaskRepository
    └─ method ReadQuery [L91]
  └─ uses_service UserService
    └─ method GetUserId [L93]

