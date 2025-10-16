[web] GET /api/ui/workflow/jobs/series/batches  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeriesBatches)  [L145–L162] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobRepository.ReadQuery [L151]
  └─ query Job [L151]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L151]
      └─ ... (no implementation details available)

