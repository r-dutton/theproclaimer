[web] DELETE /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.Delete)  [L95–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls KanbanColumnRepository.Remove [L102]
  └─ calls KanbanColumnRepository.WriteQuery [L98]
  └─ delete KanbanColumn [L102]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L98]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L98]
      └─ ... (no implementation details available)

