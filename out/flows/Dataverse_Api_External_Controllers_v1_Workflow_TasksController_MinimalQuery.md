[web] GET /workflow/v1/tasks/  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.MinimalQuery)  [L67–L77] status=200
  └─ calls TaskRepository.ReadQuery [L71]
  └─ query WorkflowTask [L71]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkflowTask writes=0 reads=1

