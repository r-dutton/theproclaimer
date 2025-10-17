[web] PUT /api/internal/job-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Reorder)  [L102–L113] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobTypeRepository.WriteQuery [L105]
  └─ write JobType [L105]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobType writes=1 reads=0

