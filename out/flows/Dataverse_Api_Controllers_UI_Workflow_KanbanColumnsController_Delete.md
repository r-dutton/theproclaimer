[web] DELETE /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Delete)  [L95–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository (methods: Remove,WriteQuery) [L102]
  └─ delete KanbanColumn [L102]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L98]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ KanbanColumn writes=2 reads=0

