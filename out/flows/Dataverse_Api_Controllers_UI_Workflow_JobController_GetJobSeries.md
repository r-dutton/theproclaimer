[web] GET /api/ui/workflow/jobs/series  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeries)  [L112–L136] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobRepository.ReadQuery [L121]
  └─ query Job [L121]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L121]
      └─ ... (no implementation details available)

