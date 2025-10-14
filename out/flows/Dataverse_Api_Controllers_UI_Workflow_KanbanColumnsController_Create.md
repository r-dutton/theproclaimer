[web] POST /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Create)  [L55–L68] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.Add [L65]
  └─ calls KanbanColumnRepository.WriteQuery [L58]
  └─ writes_to KanbanColumn [L65]
    └─ reads_from KanbanColumns
  └─ writes_to KanbanColumn [L58]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L58]

