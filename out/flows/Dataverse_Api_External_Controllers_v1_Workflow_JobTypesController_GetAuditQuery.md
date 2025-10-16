[web] GET /workflow/v1/job-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditQuery)  [L116–L121] status=200
  └─ maps_to EntityAuditDto [L119]
  └─ calls JobTypeRepository.ReadQuery [L119]
  └─ query JobType [L119]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

