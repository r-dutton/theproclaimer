[web] POST /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Create)  [L108–L116] [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.Add [L114]
  └─ writes_to JobFilterSet [L114]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method Add [L114]
  └─ uses_service UserService
    └─ method GetUserId [L111]
  └─ sends_request CanIEditJobFilterSet [L112]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]

