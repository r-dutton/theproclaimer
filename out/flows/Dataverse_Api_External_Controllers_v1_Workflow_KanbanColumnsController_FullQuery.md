[web] GET /workflow/v1/kanban-columns/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.FullQuery)  [L83–L89]
  └─ calls KanbanColumnRepository.ReadQuery [L87]
  └─ queries KanbanColumn [L87]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L87]

