[web] PUT /api/internal/job-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Reorder)  [L102–L113] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobTypeRepository.WriteQuery [L105]
  └─ writes_to JobType [L105]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L105]

