[web] GET /api/internal/job-statuses/audit  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.GetAll)  [L45–L49] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.ReadQuery [L48]
  └─ query JobStatus [L48]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L48]
      └─ ... (no implementation details available)

