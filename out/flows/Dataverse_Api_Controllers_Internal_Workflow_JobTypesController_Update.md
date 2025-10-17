[web] PUT /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Update)  [L90–L100] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobTypeRepository.WriteQuery [L93]
  └─ write JobType [L93]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobType writes=1 reads=0

