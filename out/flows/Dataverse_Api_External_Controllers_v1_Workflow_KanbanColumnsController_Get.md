[web] GET /workflow/v1/kanban-columns/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.Get)  [L48–L53] status=200
  └─ maps_to KanbanColumnDto [L51]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
  └─ calls KanbanColumnRepository.ReadQuery [L51]
  └─ query KanbanColumn [L51]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ KanbanColumnDto

