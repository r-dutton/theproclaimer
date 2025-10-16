[web] POST /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Create)  [L59–L72] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.Add [L69]
  └─ calls KanbanColumnRepository.WriteQuery [L62]
  └─ insert KanbanColumn [L69]
    └─ reads_from KanbanColumns
  └─ write KanbanColumn [L62]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L62]
      └─ ... (no implementation details available)

