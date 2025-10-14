[web] GET /workflow/v1/jobs/  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.MinimalQuery)  [L69–L77]
  └─ calls JobRepository.ReadQuery [L73]
  └─ queries Job [L73]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L73]

