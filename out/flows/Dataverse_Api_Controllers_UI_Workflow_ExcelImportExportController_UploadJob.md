[web] POST /api/ui/workflow/excel/upload-job  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.UploadJob)  [L92–L100] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request ImportJobsFromDtoCommand -> ImportJobsFromDtoCommandHandler [L95]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ImportJobsFromDtoCommandHandler.Handle [L31–L263]
      └─ calls UserRepository.ReadQuery [L216]
      └─ calls ClientRepository.ReadQuery [L184]
      └─ uses_client ClientRepository [L184]
      └─ uses_service IControlledRepository<JobType> (Scoped (inferred))
        └─ method ReadQuery [L253]
          └─ implementation Dataverse.Data.Repository.Workflow.JobTypeRepository.ReadQuery
      └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
        └─ method ReadQuery [L231]
          └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.ReadQuery
      └─ uses_service IControlledRepository<DeliverableType> (Scoped (inferred))
        └─ method ReadQuery [L200]
          └─ implementation Dataverse.Data.Repository.Workflow.DeliverableTypeRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ ImportJobsFromDtoCommand
    └─ handlers 1
      └─ ImportJobsFromDtoCommandHandler

