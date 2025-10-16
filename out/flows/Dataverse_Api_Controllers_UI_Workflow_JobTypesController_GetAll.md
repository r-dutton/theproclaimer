[web] GET /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
  └─ calls JobTypeRepository.ReadQuery [L45]
  └─ query JobType [L45]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

