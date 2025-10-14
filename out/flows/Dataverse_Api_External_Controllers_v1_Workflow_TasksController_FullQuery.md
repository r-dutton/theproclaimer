[web] GET /workflow/v1/tasks/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.FullQuery)  [L89–L99]
  └─ queries WorkflowTask [L93]
    └─ reads_from DVS_Tasks
  └─ uses_service IControlledRepository<WorkflowTask>
    └─ method ReadQuery [L93]

