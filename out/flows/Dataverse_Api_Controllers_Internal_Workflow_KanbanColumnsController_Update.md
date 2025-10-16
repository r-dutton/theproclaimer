[web] PUT /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.Update)  [L74–L84] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls KanbanColumnRepository.WriteQuery [L77]
  └─ write KanbanColumn [L77]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method WriteQuery [L77]
      └─ ... (no implementation details available)

