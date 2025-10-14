[web] GET /workflow/v1/jobs/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.FullQuery)  [L89–L97]
  └─ calls JobRepository.ReadQuery [L93]
  └─ queries Job [L93]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L93]

