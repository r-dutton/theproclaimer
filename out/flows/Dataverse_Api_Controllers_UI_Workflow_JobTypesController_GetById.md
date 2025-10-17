[web] GET /api/ui/workflow/job-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.GetById)  [L66–L74] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobTypeDto [L69]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
  └─ calls JobTypeRepository.ReadQuery [L69]
  └─ query JobType [L69]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeDto

