[web] POST /api/internal/tasks/  (Dataverse.Api.Controllers.Internal.Workflow.WorkflowTaskController.CreateTask)  [L48–L58] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WorkflowTaskDto [L57]
  └─ calls TaskRepository.Add [L55]
  └─ insert WorkflowTask [L55]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkflowTask writes=1 reads=0
    └─ mappings 1
      └─ WorkflowTaskDto

