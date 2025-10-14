[web] DELETE /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Delete)  [L130–L140] [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.Remove [L139]
  └─ calls JobFilterSetRepository.WriteQuery [L133]
  └─ writes_to JobFilterSet [L139]
    └─ reads_from JobFilterSets
  └─ writes_to JobFilterSet [L133]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L133]
  └─ sends_request CanIEditJobFilterSet [L137]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]

