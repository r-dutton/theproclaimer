[web] GET /workflow/v1/job-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.Get)  [L52–L57] status=200
  └─ maps_to JobTypeDto [L55]
    └─ automapper.registration DataverseMappingProfile (JobType->JobTypeDto) [L318]
    └─ automapper.registration ExternalApiMappingProfile (JobType->JobTypeDto) [L175]
  └─ calls JobTypeRepository.ReadQuery [L55]
  └─ query JobType [L55]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobType writes=0 reads=1
    └─ mappings 1
      └─ JobTypeDto

