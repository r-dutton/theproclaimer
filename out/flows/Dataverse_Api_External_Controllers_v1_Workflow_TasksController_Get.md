[web] GET /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Get)  [L49–L55] status=200
  └─ maps_to WorkflowTaskDto [L52]
    └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L371]
    └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
  └─ calls TaskRepository.ReadQuery [L52]
  └─ query WorkflowTask [L52]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkflowTask writes=0 reads=1
    └─ mappings 1
      └─ WorkflowTaskDto

