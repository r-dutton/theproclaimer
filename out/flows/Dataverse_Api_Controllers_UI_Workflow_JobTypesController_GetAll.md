[web] GET /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetAll)  [L42–L52] [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L317]
  └─ calls JobTypeRepository.ReadQuery [L45]
  └─ queries JobType [L45]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L45]

