[web] GET /workflow/v1/tasks/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.FullQuery)  [L89–L99] status=200
  └─ calls TaskRepository.ReadQuery [L93]
  └─ query WorkflowTask [L93]
    └─ reads_from DVS_Tasks
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkflowTask writes=0 reads=1

