[web] GET /workflow/v1/job-statuses/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditQuery)  [L116–L121] status=200
  └─ maps_to EntityAuditDto [L119]
  └─ calls JobStatusRepository.ReadQuery [L119]
  └─ query JobStatus [L119]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L119]
      └─ ... (no implementation details available)

