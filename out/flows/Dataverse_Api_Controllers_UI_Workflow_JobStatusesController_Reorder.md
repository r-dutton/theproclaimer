[web] PUT /api/ui/workflow/job-statuses/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Reorder)  [L103–L114] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobStatusRepository.WriteQuery [L106]
  └─ write JobStatus [L106]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobStatus writes=1 reads=0

