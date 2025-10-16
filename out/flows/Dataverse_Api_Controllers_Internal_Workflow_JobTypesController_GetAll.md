[web] GET /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetAll)  [L53–L63] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L56]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
  └─ calls JobTypeRepository.ReadQuery [L56]
  └─ query JobType [L56]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeDto

