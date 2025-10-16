[web] PUT /api/ui/workflow/job-filter-sets/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Update)  [L118–L128] status=200 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.WriteQuery [L121]
  └─ write JobFilterSet [L121]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method WriteQuery [L121]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L127]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
  └─ sends_request CanIEditJobFilterSet [L125]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

