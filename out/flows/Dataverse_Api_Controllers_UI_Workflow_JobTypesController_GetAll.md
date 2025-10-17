[web] GET /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeDto [L45]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
  └─ calls JobTypeRepository.ReadQuery [L45]
  └─ query JobType [L45]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeDto

