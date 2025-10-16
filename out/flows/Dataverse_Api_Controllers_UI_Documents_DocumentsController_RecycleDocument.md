[web] DELETE /api/ui/documents/documents/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.RecycleDocument)  [L325–L341] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentRepository.WriteQuery [L329]
  └─ write Document [L329]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method WriteQuery [L329]
      └─ ... (no implementation details available)
  └─ sends_request RecycleDocumentCommand [L340]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RecycleDocumentCommandHandler.Handle [L28–L66]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L51]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L60]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanIAccessDocumentQuery [L339]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)

