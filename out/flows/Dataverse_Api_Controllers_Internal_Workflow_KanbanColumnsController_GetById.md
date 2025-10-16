[web] GET /api/internal/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetById)  [L49–L57] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to KanbanColumnDto [L52]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
  └─ calls KanbanColumnRepository.ReadQuery [L52]
  └─ query KanbanColumn [L52]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ KanbanColumnDto

