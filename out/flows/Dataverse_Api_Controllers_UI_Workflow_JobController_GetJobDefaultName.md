[web] GET /api/ui/workflow/jobs/default-name  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobDefaultName)  [L95–L100] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request DefaultJobNameQuery [L98]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultJobNameQueryHandler.Handle [L32–L73]
      └─ maps_to JobTypeDto [L51]
      └─ uses_service JobParamsService
        └─ method GetClient [L50]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient [L24-L215]
      └─ uses_service IMapper
        └─ method Map [L51]
          └─ ... (no implementation details available)

