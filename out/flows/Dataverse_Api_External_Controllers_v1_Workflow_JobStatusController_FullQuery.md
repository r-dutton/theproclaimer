[web] GET /workflow/v1/job-statuses/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.FullQuery)  [L88–L95] status=200
  └─ calls JobStatusRepository.ReadQuery [L92]
  └─ query JobStatus [L92]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1

