[web] GET /workflow/v1/job-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.FullQuery)  [L88–L95] status=200
  └─ calls JobTypeRepository.ReadQuery [L92]
  └─ query JobType [L92]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1

