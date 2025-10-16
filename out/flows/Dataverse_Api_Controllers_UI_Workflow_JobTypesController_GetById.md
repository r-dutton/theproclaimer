[web] GET /api/ui/workflow/job-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetById)  [L66–L74] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeDto [L69]
    └─ automapper.registration WorkpapersMappingProfile (JobType->JobTypeDto) [L867]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
  └─ calls JobTypeRepository.ReadQuery [L69]
  └─ query JobType [L69]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L69]
      └─ ... (no implementation details available)

