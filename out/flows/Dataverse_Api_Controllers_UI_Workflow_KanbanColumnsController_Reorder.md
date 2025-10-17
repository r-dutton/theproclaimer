[web] PUT /api/ui/workflow/kanban-columns/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Reorder)  [L82–L93] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.WriteQuery [L85]
  └─ write KanbanColumn [L85]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ KanbanColumn writes=1 reads=0

