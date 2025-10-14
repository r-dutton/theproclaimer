[web] PUT /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Update)  [L118–L128] [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.WriteQuery [L121]
  └─ writes_to JobFilterSet [L121]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L121]
  └─ uses_service UserService
    └─ method GetUserId [L127]
  └─ sends_request CanIEditJobFilterSet [L125]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]

