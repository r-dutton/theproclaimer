[web] GET /api/ui/workflow/job-filter-sets/{id:Guid}/can-i-save  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.CanISave)  [L75–L83] [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.WriteQuery [L78]
  └─ writes_to JobFilterSet [L78]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L78]
  └─ sends_request CanIEditJobFilterSet [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]

