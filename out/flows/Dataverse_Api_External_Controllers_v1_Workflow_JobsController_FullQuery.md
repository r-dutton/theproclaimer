[web] GET /workflow/v1/jobs/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.FullQuery)  [L89–L97] status=200
  └─ calls JobRepository.ReadQuery [L93]
  └─ query Job [L93]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L93]
      └─ ... (no implementation details available)

