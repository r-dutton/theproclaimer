[web] PUT /api/internal/job-statuses/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.UpdateOrder)  [L78–L89] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobStatusRepository.WriteQuery [L81]
  └─ writes_to JobStatus [L81]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method WriteQuery [L81]
  └─ uses_service IMapper
    └─ method Map [L88]

