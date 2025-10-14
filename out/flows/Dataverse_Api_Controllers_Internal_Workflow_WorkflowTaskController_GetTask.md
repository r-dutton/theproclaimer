[web] GET /api/internal/tasks/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTask)  [L40–L46] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WorkflowTaskDto [L43]
    └─ automapper.registration DataverseMappingProfile (WorkflowTask->WorkflowTaskDto) [L370]
    └─ automapper.registration ExternalApiMappingProfile (WorkflowTask->WorkflowTaskDto) [L143]
  └─ queries WorkflowTask [L43]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method ReadQuery [L43]

