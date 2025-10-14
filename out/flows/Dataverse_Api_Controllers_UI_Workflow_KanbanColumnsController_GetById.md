[web] GET /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetById)  [L45–L53] [auth=Authentication.UserPolicy]
  └─ maps_to KanbanColumnDto [L48]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L378]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L48]
  └─ queries KanbanColumn [L48]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L48]

