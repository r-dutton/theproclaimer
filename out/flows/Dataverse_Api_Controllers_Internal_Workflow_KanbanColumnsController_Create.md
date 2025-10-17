[web] POST /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Create)  [L59–L72] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository (methods: Add,WriteQuery) [L69]
  └─ insert KanbanColumn [L69]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L62]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ KanbanColumn writes=2 reads=0

