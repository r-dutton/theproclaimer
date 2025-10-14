[web] POST /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Create)  [L59–L72] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.Add [L69]
  └─ calls KanbanColumnRepository.WriteQuery [L62]
  └─ writes_to KanbanColumn [L69]
    └─ reads_from KanbanColumns
  └─ writes_to KanbanColumn [L62]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L62]

