[web] DELETE /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Delete)  [L99–L109] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.Remove [L106]
  └─ calls KanbanColumnRepository.WriteQuery [L102]
  └─ writes_to KanbanColumn [L106]
    └─ reads_from KanbanColumns
  └─ writes_to KanbanColumn [L102]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L102]

