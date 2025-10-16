[web] DELETE /api/internal/tasks/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.Delete)  [L120–L130] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ delete WorkflowTask [L127]
    └─ reads_from DVS_Tasks
  └─ write WorkflowTask [L123]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method WriteQuery [L123]
      └─ ... (no implementation details available)

