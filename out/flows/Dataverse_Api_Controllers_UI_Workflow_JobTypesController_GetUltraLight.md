[web] GET /api/ui/workflow/job-types/ultra-light  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetUltraLight)  [L54–L64] [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeUltraLightDto [L57]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeUltraLightDto) [L318]
  └─ calls JobTypeRepository.ReadQuery [L57]
  └─ queries JobType [L57]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L57]

