[web] GET /api/internal/job-statuses/audit  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.GetAll)  [L45–L49] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.ReadQuery [L48]
  └─ queries JobStatus [L48]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L48]

