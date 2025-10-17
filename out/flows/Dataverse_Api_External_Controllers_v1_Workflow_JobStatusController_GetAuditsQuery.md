[web] GET /workflow/v1/job-statuses/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.GetAuditsQuery)  [L102–L108] status=200
  └─ maps_to EntityAuditDto [L105]
  └─ calls JobStatusRepository.ReadQuery [L105]
  └─ query JobStatus [L105]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

