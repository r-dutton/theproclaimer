[web] GET /workflow/v1/job-statuses/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditQuery)  [L116–L121]
  └─ maps_to EntityAuditDto [L119]
  └─ calls JobStatusRepository.ReadQuery [L119]
  └─ queries JobStatus [L119]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L119]

