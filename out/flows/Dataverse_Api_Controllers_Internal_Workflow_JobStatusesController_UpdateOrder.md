[web] PUT /api/internal/job-statuses/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.UpdateOrder)  [L78–L89] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.WriteQuery [L81]
  └─ write JobStatus [L81]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobStatus writes=1 reads=0

