[web] GET /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to KanbanColumnDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
  └─ calls KanbanColumnRepository.ReadQuery [L48]
  └─ query KanbanColumn [L48]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ KanbanColumnDto

