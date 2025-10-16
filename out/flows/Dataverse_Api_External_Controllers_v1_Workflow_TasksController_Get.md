[web] GET /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Get)  [L49–L55] status=200
  └─ maps_to WorkflowTaskDto [L52]
    └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L371]
    └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
  └─ query WorkflowTask [L52]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method ReadQuery [L52]
      └─ ... (no implementation details available)

