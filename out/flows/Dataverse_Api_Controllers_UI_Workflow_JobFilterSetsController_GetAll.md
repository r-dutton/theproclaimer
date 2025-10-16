[web] GET /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetAll)  [L48–L63] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobFilterSetLightDto [L53]
    └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetLightDto) [L343]
  └─ calls JobFilterSetRepository.ReadQuery [L53]
  └─ query JobFilterSet [L53]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method ReadQuery [L53]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L51]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

