[web] GET /api/internal/job-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.GetById)  [L65–L73] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L68]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L317]
  └─ calls JobTypeRepository.ReadQuery [L68]
  └─ queries JobType [L68]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L68]

