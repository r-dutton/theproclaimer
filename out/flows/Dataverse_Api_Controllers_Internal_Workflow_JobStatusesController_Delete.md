[web] DELETE /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.Delete)  [L91–L99] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.WriteQuery [L94]
  └─ write JobStatus [L94]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobStatus writes=1 reads=0

