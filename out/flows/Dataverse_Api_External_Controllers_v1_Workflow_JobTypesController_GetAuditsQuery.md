[web] GET /workflow/v1/job-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditsQuery)  [L102–L108] status=200
  └─ maps_to EntityAuditDto [L105]
  └─ calls JobTypeRepository.ReadQuery [L105]
  └─ query JobType [L105]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

