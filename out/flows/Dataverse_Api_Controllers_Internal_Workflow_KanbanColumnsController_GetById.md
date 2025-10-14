[web] GET /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetById)  [L49–L57] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to KanbanColumnDto [L52]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L378]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L52]
  └─ queries KanbanColumn [L52]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L52]

