[web] GET /api/ui/workflow/job-filter-sets/{id:Guid}/can-i-save  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.CanISave)  [L75–L83] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.WriteQuery [L78]
  └─ write JobFilterSet [L78]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L78]
      └─ ... (no implementation details available)
  └─ sends_request CanIEditJobFilterSet [L82]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

