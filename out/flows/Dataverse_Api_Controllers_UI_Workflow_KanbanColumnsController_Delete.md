[web] DELETE /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Delete)  [L95–L105] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.Remove [L102]
  └─ calls KanbanColumnRepository.WriteQuery [L98]
  └─ writes_to KanbanColumn [L102]
    └─ reads_from KanbanColumns
  └─ writes_to KanbanColumn [L98]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L98]

