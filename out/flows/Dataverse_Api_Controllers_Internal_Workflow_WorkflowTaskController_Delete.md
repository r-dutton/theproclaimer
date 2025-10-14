[web] DELETE /api/internal/tasks/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.Delete)  [L120–L130] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ writes_to WorkflowTask [L127]
    └─ reads_from DVS_Tasks
  └─ writes_to WorkflowTask [L123]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method WriteQuery [L123]

