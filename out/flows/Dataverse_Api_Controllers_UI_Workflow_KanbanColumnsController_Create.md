[web] POST /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Create)  [L55–L68] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository (methods: Add,WriteQuery) [L65]
  └─ insert KanbanColumn [L65]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L58]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ KanbanColumn writes=2 reads=0

