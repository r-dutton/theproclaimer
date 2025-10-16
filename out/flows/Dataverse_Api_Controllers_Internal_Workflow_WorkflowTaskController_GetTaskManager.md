[web] GET /api/internal/tasks  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.GetTaskManager)  [L151–L162] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskRepository.WriteQuery [L153]
  └─ write WorkflowTask [L153]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkflowTask writes=1 reads=0

