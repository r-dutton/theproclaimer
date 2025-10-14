[web] GET /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetById)  [L65–L73] [auth=Authentication.UserPolicy]
  └─ maps_to JobFilterSetDto [L68]
    └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetDto) [L343]
  └─ calls JobFilterSetRepository.ReadQuery [L68]
  └─ queries JobFilterSet [L68]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method ReadQuery [L68]

