[web] GET /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetById)  [L65–L73] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L68]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
  └─ calls JobTypeRepository.ReadQuery [L68]
  └─ query JobType [L68]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeDto

