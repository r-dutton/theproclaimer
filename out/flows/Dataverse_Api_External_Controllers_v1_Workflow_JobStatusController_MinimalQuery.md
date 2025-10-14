[web] GET /workflow/v1/job-statuses/  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.MinimalQuery)  [L69–L76]
  └─ calls JobStatusRepository.ReadQuery [L73]
  └─ queries JobStatus [L73]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L73]

