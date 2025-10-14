[web] POST /api/internal/tasks/  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.CreateTask)  [L48–L58] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WorkflowTaskDto [L57]
  └─ writes_to WorkflowTask [L55]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method Add [L55]
  └─ uses_service IMapper
    └─ method Map [L57]

