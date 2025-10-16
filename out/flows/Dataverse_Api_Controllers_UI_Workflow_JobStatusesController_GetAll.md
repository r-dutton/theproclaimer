[web] GET /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.GetAll)  [L46–L55] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobStatusLightDto [L49]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusLightDto) [L316]
  └─ calls JobStatusRepository.ReadQuery [L49]
  └─ query JobStatus [L49]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1
    └─ mappings 1
      └─ JobStatusLightDto

