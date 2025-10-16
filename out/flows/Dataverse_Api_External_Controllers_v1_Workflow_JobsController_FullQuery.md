[web] GET /workflow/v1/jobs/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.FullQuery)  [L89–L97] status=200
  └─ calls JobRepository.ReadQuery [L93]
  └─ query Job [L93]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1

