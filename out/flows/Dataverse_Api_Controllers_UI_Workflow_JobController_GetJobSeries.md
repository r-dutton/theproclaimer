[web] GET /api/ui/workflow/jobs/series  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeries)  [L112–L136] [auth=Authentication.UserPolicy]
  └─ calls JobRepository.ReadQuery [L121]
  └─ queries Job [L121]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L121]

