[web] GET /workflow/v1/job-statuses/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.Get)  [L52–L57] status=200
  └─ maps_to JobStatusDto [L55]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
    └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
  └─ calls JobStatusRepository.ReadQuery [L55]
  └─ query JobStatus [L55]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)

