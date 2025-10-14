[web] GET /workflow/v1/jobs/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Get)  [L52–L57]
  └─ maps_to JobDto [L55]
    └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L306]
    └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
    └─ converts_to JobExportDto [L311]
  └─ calls JobRepository.ReadQuery [L55]
  └─ queries Job [L55]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L55]

