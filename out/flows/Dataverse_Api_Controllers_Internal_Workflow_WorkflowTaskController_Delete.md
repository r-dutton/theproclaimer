[web] DELETE /api/internal/tasks/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.Delete)  [L120–L130] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskRepository (methods: Remove,WriteQuery) [L127]
  └─ delete WorkflowTask [L127]
    └─ reads_from DVS_Tasks
  └─ write WorkflowTask [L123]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ WorkflowTask writes=2 reads=0

