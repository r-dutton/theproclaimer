[web] GET /workflow/v1/jobs/  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.MinimalQuery)  [L69–L77] status=200
  └─ calls JobRepository.ReadQuery [L73]
  └─ query Job [L73]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)

