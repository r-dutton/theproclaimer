[web] GET /workflow/v1/kanban-columns/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.FullQuery)  [L83–L89] status=200
  └─ calls KanbanColumnRepository.ReadQuery [L87]
  └─ query KanbanColumn [L87]
    └─ reads_from KanbanColumns
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ KanbanColumn writes=0 reads=1

