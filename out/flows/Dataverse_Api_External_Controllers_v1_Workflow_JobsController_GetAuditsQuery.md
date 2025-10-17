[web] GET /workflow/v1/jobs/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditsQuery)  [L104–L110] status=200
  └─ maps_to EntityAuditDto [L107]
  └─ calls JobRepository.ReadQuery [L107]
  └─ query Job [L107]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

