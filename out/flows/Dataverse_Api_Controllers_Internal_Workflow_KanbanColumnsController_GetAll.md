[web] GET /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetAll)  [L37–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to KanbanColumnDto [L40]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L40]
  └─ query KanbanColumn [L40]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L40]
      └─ ... (no implementation details available)

