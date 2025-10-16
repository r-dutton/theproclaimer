[web] GET /workflow/v1/jobs/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditsQuery)  [L104–L110] status=200
  └─ maps_to EntityAuditDto [L107]
  └─ calls JobRepository.ReadQuery [L107]
  └─ query Job [L107]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L107]
      └─ ... (no implementation details available)

