[web] DELETE /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Delete)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobTypeRepository.WriteQuery [L118]
  └─ write JobType [L118]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobType writes=1 reads=0

