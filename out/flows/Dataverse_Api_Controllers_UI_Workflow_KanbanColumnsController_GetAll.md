[web] GET /api/ui/workflow/kanban-columns  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetAll)  [L33–L43] [auth=Authentication.UserPolicy]
  └─ maps_to KanbanColumnDto [L36]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L378]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L36]
  └─ queries KanbanColumn [L36]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L36]

