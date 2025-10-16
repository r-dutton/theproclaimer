[web] GET /api/ui/workflow/job-types/ultra-light  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetUltraLight)  [L54–L64] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeUltraLightDto [L57]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeUltraLightDto) [L319]
  └─ calls JobTypeRepository.ReadQuery [L57]
  └─ query JobType [L57]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeUltraLightDto

