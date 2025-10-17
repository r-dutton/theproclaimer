[web] GET /workflow/v1/jobs/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditQuery)  [L118–L123] status=200
  └─ maps_to EntityAuditDto [L121]
  └─ calls JobRepository.ReadQuery [L121]
  └─ query Job [L121]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

