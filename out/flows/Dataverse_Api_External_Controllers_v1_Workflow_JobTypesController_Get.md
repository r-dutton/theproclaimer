[web] GET /workflow/v1/job-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.Get)  [L52–L57]
  └─ maps_to JobTypeDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L317]
  └─ calls JobTypeRepository.ReadQuery [L55]
  └─ queries JobType [L55]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L55]

