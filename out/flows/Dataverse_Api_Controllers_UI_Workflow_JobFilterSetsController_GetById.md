[web] GET /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.GetById)  [L65–L73] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to JobFilterSetDto [L68]
    └─ automapper.registration DataverseMappingProfile (JobFilterSet->JobFilterSetDto) [L344]
  └─ calls JobFilterSetRepository.ReadQuery [L68]
  └─ query JobFilterSet [L68]
    └─ reads_from JobFilterSets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobFilterSet writes=0 reads=1
    └─ mappings 1
      └─ JobFilterSetDto

