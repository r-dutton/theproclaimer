[web] DELETE /api/ui/workflow/job-statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Delete)  [L116–L126] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobStatusRepository.WriteQuery [L119]
  └─ write JobStatus [L119]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobStatus writes=1 reads=0

