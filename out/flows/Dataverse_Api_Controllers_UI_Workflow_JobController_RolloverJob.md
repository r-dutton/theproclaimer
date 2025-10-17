[web] POST /api/ui/workflow/jobs/{id:guid}/rollover  (Dataverse.Api.Controllers.UI.Workflow.JobController.RolloverJob)  [L171–L175] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request RolloverJobCommand -> RolloverJobCommandHandler [L174]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.RolloverJobCommandHandler.Handle [L58–L257]
      └─ uses_service IControlledRepository<Job> (Scoped (inferred))
        └─ method ReadQuery [L151]
          └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
      └─ uses_service JobParamsService
        └─ method GetDefaultStatus [L95]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetDefaultStatus [L24-L215]
            └─ uses_client ClientRepository [L137]
            └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
              └─ method GetDefaultStatus [L95]
                └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.GetDefaultStatus
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ RolloverJobCommand
    └─ handlers 1
      └─ RolloverJobCommandHandler

