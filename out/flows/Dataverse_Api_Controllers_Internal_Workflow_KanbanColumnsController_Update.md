[web] PUT /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Update)  [L74–L84] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.WriteQuery [L77]
  └─ write KanbanColumn [L77]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ KanbanColumn writes=1 reads=0

