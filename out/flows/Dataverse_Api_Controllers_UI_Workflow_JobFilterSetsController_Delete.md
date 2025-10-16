[web] DELETE /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Delete)  [L130–L140] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.Remove [L139]
  └─ calls JobFilterSetRepository.WriteQuery [L133]
  └─ delete JobFilterSet [L139]
    └─ reads_from JobFilterSets
  └─ write JobFilterSet [L133]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L133]
      └─ ... (no implementation details available)
  └─ sends_request CanIEditJobFilterSet [L137]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

