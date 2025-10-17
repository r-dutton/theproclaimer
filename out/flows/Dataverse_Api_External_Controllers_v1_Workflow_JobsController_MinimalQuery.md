[web] GET /workflow/v1/jobs/  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.MinimalQuery)  [L69–L77] status=200
  └─ calls JobRepository.ReadQuery [L73]
  └─ query Job [L73]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1

