[web] GET /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetAll)  [L53–L63] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L56]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
  └─ calls JobTypeRepository.ReadQuery [L56]
  └─ query JobType [L56]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L56]
      └─ ... (no implementation details available)

