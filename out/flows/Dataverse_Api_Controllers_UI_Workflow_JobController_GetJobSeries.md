[web] GET /api/ui/workflow/jobs/series  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeries)  [L112–L136] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobRepository.ReadQuery [L121]
  └─ query Job [L121]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1

