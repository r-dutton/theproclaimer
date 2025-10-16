[web] GET /api/ui/workflow/kanban-columns/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.KanbanColumnsController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to KanbanColumnDto [L48]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L48]
  └─ query KanbanColumn [L48]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L48]
      └─ ... (no implementation details available)

