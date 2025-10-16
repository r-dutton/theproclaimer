[web] GET /workflow/v1/job-statuses/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.FullQuery)  [L88–L95] status=200
  └─ calls JobStatusRepository.ReadQuery [L92]
  └─ query JobStatus [L92]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L92]
      └─ ... (no implementation details available)

