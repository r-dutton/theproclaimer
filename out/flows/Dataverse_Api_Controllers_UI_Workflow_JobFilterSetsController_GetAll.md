[web] GET /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetAll)  [L48–L63] [auth=Authentication.UserPolicy]
  └─ maps_to JobFilterSetLightDto [L53]
    └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetLightDto) [L342]
  └─ calls JobFilterSetRepository.ReadQuery [L53]
  └─ queries JobFilterSet [L53]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method ReadQuery [L53]
  └─ uses_service UserService
    └─ method GetUserId [L51]

