[web] DELETE /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Delete)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobTypeRepository.WriteQuery [L118]
  └─ write JobType [L118]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L118]
      └─ ... (no implementation details available)

