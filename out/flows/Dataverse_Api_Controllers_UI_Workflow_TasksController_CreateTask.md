[web] POST /api/ui/workflow/tasks/  (Dataverse.Api.Controllers.UI.Workflow.TasksController.CreateTask)  [L97–L107] [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskDto [L106]
  └─ calls TaskRepository.Add [L104]
  └─ uses_service IMapper
    └─ method Map [L106]
  └─ uses_service TaskRepository
    └─ method Add [L104]

