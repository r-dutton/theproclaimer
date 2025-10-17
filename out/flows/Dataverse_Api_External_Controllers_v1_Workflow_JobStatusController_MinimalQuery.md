[web] GET /workflow/v1/job-statuses/  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.MinimalQuery)  [L69–L76] status=200
  └─ calls JobStatusRepository.ReadQuery [L73]
  └─ query JobStatus [L73]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1

