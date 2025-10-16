[web] GET /api/ui/workflow/jobs/{id:guid}/cirrus-links  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobCirrusLinks)  [L83–L93] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableCirrusLinkDto [L86]
  └─ calls JobRepository.ReadQuery [L86]
  └─ query Job [L86]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L86]
      └─ ... (no implementation details available)

