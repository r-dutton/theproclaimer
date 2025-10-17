[web] GET /api/internal/kanban-columns  (Dataverse.Api.Controllers.Internal.Workflow.KanbanColumnsController.GetAll)  [L37–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to KanbanColumnDto [L40]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
  └─ calls KanbanColumnRepository.ReadQuery [L40]
  └─ query KanbanColumn [L40]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ KanbanColumnDto

