[web] POST /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Create)  [L55–L68] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.Add [L65]
  └─ calls KanbanColumnRepository.WriteQuery [L58]
  └─ insert KanbanColumn [L65]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L58]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L58]
      └─ ... (no implementation details available)

