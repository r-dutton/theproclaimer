[web] GET /api/internal/tasks  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTaskManager)  [L151–L162] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ write WorkflowTask [L153]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method WriteQuery [L153]
      └─ ... (no implementation details available)

