[web] POST /api/internal/tasks/  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.CreateTask)  [L48–L58] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WorkflowTaskDto [L57]
  └─ insert WorkflowTask [L55]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method Add [L55]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L57]
      └─ ... (no implementation details available)

