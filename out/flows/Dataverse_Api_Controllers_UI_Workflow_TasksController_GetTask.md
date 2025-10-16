[web] GET /api/ui/workflow/tasks/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetTask)  [L88–L95] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskDto [L91]
  └─ calls TaskRepository.ReadQuery [L91]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L93]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service TaskRepository
    └─ method ReadQuery [L91]
      └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery [L8-L40]
  └─ uses_service UserService
    └─ method GetUserId [L93]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

