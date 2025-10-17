[web] PUT /api/internal/kanban-columns/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Reorder)  [L86–L97] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.WriteQuery [L89]
  └─ write KanbanColumn [L89]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ KanbanColumn writes=1 reads=0

