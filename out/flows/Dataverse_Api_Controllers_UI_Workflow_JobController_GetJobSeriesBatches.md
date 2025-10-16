[web] GET /api/ui/workflow/jobs/series/batches  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobSeriesBatches)  [L145–L162] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobRepository.ReadQuery [L151]
  └─ query Job [L151]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1

