[web] POST /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Create)  [L75–L88] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L87]
  └─ calls JobTypeRepository (methods: Add,WriteQuery) [L85]
  └─ insert JobType [L85]
    └─ reads_from JobTypes
  └─ write JobType [L78]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ JobType writes=2 reads=0
    └─ mappings 1
      └─ JobTypeDto

