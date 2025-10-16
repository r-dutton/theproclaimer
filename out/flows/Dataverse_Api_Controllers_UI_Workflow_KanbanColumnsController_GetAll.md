[web] GET /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetAll)  [L33–L43] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to KanbanColumnDto [L36]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
  └─ calls KanbanColumnRepository.ReadQuery [L36]
  └─ query KanbanColumn [L36]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ KanbanColumnDto

