[web] GET /api/ui/workflow/job-statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.GetById)  [L57–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobStatusDto [L60]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
    └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
  └─ calls JobStatusRepository.ReadQuery [L60]
  └─ query JobStatus [L60]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L60]
      └─ ... (no implementation details available)

