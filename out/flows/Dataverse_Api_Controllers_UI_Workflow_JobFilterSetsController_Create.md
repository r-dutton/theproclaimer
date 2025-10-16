[web] POST /api/ui/workflow/job-filter-sets  (Dataverse.Api.Controllers.UI.Workflow.JobFilterSetsController.Create)  [L108–L116] status=201 [auth=Authentication.UserPolicy]
  └─ calls JobFilterSetRepository.Add [L114]
  └─ insert JobFilterSet [L114]
    └─ reads_from JobFilterSets
  └─ uses_service IControlledRepository<JobFilterSet>
    └─ method Add [L114]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L111]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
  └─ sends_request CanIEditJobFilterSet [L112]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIEditJobFilterSetHandler.Handle [L24–L56]
      └─ uses_service UserService
        └─ method GetUserAsync [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

