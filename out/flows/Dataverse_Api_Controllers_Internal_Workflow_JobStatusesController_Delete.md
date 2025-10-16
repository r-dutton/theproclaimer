[web] DELETE /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.Delete)  [L91–L99] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.WriteQuery [L94]
  └─ write JobStatus [L94]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method WriteQuery [L94]
      └─ ... (no implementation details available)

