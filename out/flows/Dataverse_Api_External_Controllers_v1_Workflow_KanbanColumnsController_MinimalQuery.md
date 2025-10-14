[web] GET /workflow/v1/kanban-columns/  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.MinimalQuery)  [L65–L71]
  └─ calls KanbanColumnRepository.ReadQuery [L69]
  └─ queries KanbanColumn [L69]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L69]

