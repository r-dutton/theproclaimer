[web] GET /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.GetAll)  [L46–L55] [auth=Authentication.UserPolicy]
  └─ maps_to JobStatusLightDto [L49]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusLightDto) [L315]
  └─ calls JobStatusRepository.ReadQuery [L49]
  └─ queries JobStatus [L49]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method ReadQuery [L49]

