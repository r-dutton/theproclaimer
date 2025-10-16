[web] GET /workflow/v1/kanban-columns/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.Get)  [L48–L53] status=200
  └─ maps_to KanbanColumnDto [L51]
    └─ automapper.registration DataverseMappingProfile (KanbanColumn->KanbanColumnDto) [L379]
    └─ automapper.registration ExternalApiMappingProfile (KanbanColumn->KanbanColumnDto) [L161]
  └─ calls KanbanColumnRepository.ReadQuery [L51]
  └─ query KanbanColumn [L51]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)

