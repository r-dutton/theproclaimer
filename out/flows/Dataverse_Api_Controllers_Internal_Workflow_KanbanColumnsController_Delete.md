[web] DELETE /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Delete)  [L99–L109] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository (methods: Remove,WriteQuery) [L106]
  └─ delete KanbanColumn [L106]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L102]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ KanbanColumn writes=2 reads=0

