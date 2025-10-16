[web] GET /workflow/v1/jobs/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Get)  [L52–L57] status=200
  └─ maps_to JobDto [L55]
    └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L307]
    └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
    └─ converts_to JobExportDto [L312]
  └─ calls JobRepository.ReadQuery [L55]
  └─ query Job [L55]
    └─ reads_from Jobs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1
    └─ mappings 1
      └─ JobDto

