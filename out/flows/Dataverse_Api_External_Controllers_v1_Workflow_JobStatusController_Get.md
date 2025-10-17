[web] GET /workflow/v1/job-statuses/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.Get)  [L52–L57] status=200
  └─ maps_to JobStatusDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
  └─ calls JobStatusRepository.ReadQuery [L55]
  └─ query JobStatus [L55]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1
    └─ mappings 1
      └─ JobStatusDto

