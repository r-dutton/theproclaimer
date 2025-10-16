[web] GET /api/internal/tasks/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTask)  [L40–L46] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WorkflowTaskDto [L43]
    └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L371]
    └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
  └─ calls TaskRepository.ReadQuery [L43]
  └─ query WorkflowTask [L43]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkflowTask writes=0 reads=1
    └─ mappings 1
      └─ WorkflowTaskDto

