[web] GET /workflow/v1/job-statuses/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditQuery)  [L116–L121] status=200
  └─ maps_to EntityAuditDto [L119]
  └─ calls JobStatusRepository.ReadQuery [L119]
  └─ query JobStatus [L119]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

