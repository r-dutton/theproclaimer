[web] GET /workflow/v1/job-statuses/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditsQuery)  [L102–L108]
  └─ maps_to EntityAuditDto [L105]
  └─ calls JobStatusRepository.ReadQuery [L105]
  └─ queries JobStatus [L105]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L105]

