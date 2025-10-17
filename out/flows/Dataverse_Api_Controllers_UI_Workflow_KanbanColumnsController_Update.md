[web] PUT /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Update)  [L70–L80] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.WriteQuery [L73]
  └─ write KanbanColumn [L73]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ KanbanColumn writes=1 reads=0

