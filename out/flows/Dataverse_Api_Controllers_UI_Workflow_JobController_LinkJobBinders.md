[web] PUT /api/ui/workflow/jobs/{id:guid}/update/binders  (Dataverse.Api.Controllers.UI.Workflow.JobController.LinkJobBinders)  [L204–L210] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request UpdateJobBindersCommand -> UpdateJobBindersCommandHandler [L207]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.UpdateJobBindersCommandHandler.Handle [L27–L59]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L47]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Job> (Scoped (inferred))
        └─ method WriteQuery [L45]
          └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateJobBindersCommand
    └─ handlers 1
      └─ UpdateJobBindersCommandHandler

