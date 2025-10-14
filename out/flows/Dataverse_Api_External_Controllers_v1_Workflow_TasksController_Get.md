[web] GET /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Get)  [L49–L55]
  └─ maps_to WorkflowTaskDto [L52]
    └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L370]
    └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
  └─ queries WorkflowTask [L52]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method ReadQuery [L52]

